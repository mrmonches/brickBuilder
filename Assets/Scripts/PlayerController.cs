using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputAction select;
    private InputAction cameraControl;

    private Vector3 mousePosition;

    // Camera-Grid Settings
    [SerializeField] private Camera SceneCamera;
    private Vector3 lastPosition;

    [SerializeField] private float CastDistance;

    [SerializeField] private LayerMask LevelMask, BrickMask, OutlineMask;

    // Brick Settings
    private BrickController _brickController;
    private bool isHolding;

    // Outline Settings
    private OutlineController _outlineController;

    // Camera Settings
    [SerializeField] private CameraController _cameraController;


    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.currentActionMap.Enable();

        select = _playerInput.currentActionMap.FindAction("Select");
        cameraControl = _playerInput.currentActionMap.FindAction("CameraControl");

        select.started += Select_started;
        select.canceled += Select_canceled;

        cameraControl.started += CameraControl_started;
        cameraControl.canceled += CameraControl_canceled;
    }



    private void Select_started(InputAction.CallbackContext obj)
    {
        if (_brickController != null && !_brickController.IsPlaced)
        {
            _brickController.IsHeld = true;

            isHolding = true;

            _brickController.Rigidbody.useGravity = false;

            _brickController.Rigidbody.excludeLayers = BrickMask;

            _brickController.SetDefaultLayer();
        }
    }
    private void Select_canceled(InputAction.CallbackContext obj)
    {
        if (_brickController != null && _brickController.IsHeld && !_brickController.IsPlaced)
        {
            _brickController.IsHeld = false;

            isHolding = false;

            _brickController.Rigidbody.useGravity = true;

            _brickController.Rigidbody.excludeLayers = default;

            _brickController.SetBrickLayer();

            _outlineController = null;

            if (_brickController.IsPlacing)
            {
                _brickController.IsPlaced = true;

                _brickController.SetDefaultLayer();

                _brickController.Rigidbody.constraints = UnityEngine.RigidbodyConstraints.FreezeAll;
            }

            _brickController = null;
        }
    }

    private void CameraControl_started(InputAction.CallbackContext obj)
    {
        _cameraController.CameraInput = cameraControl.ReadValue<float>();
    }
    private void CameraControl_canceled(InputAction.CallbackContext obj)
    {
        _cameraController.CameraInput = 0f;
    }

    private void OnMouse(InputValue mousePos)
    {
        mousePosition = mousePos.Get<Vector2>();
    }

    public Vector3 GetSelectedMapPosition()
    {
        mousePosition.z = SceneCamera.nearClipPlane;
        
        RaycastHit hit;
        if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, LevelMask))
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }

    private void BrickHighlight()
    {
        RaycastHit hit;
        // If mouse is hovering over a brick
        if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, BrickMask))
        {
            // If the brick has a BrickController component and the script doesn't have a reference for BrickController
            if (hit.rigidbody.gameObject.GetComponent<BrickController>() != null && _brickController == null)
            {
                _brickController = hit.rigidbody.gameObject.GetComponent<BrickController>();
            }
            else if (hit.rigidbody.gameObject.GetComponent<BrickController>() != _brickController && !isHolding)
            {
                _brickController = hit.rigidbody.gameObject.GetComponent<BrickController>();
            }
        }
        // If mouse is hovering over an outline and is holding a brick
        else if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, OutlineMask)
            && isHolding)
        {
            if (hit.rigidbody.gameObject.GetComponent<OutlineController>() != null && _outlineController == null)
            {
                _outlineController = hit.rigidbody.gameObject.GetComponent<OutlineController>();
            }
            else if (hit.rigidbody.gameObject.GetComponent<OutlineController>() != _outlineController && 
                _brickController.IsPlacing)
            {
                _brickController.IsPlacing = false;
            }
            else if (hit.rigidbody.gameObject.GetComponent<OutlineController>() != _outlineController)
            {
                _outlineController = hit.rigidbody.gameObject.GetComponent<OutlineController>();
            }
            else if (_outlineController != null && _outlineController.BrickCheck(_brickController.BrickData))
            {
                _brickController.GoToSelectedSpot(hit.rigidbody.gameObject);
            }
        }
        else if (_brickController != null || _outlineController != null) 
        {
            if (_brickController != null && _brickController.IsPlacing && _brickController.IsHeld)
            {
                _brickController.IsPlacing = false;
            }

            if (_brickController != null && !_brickController.IsHeld)
            {
                _brickController = null;
            }

            _outlineController = null;
        }
    }

    private void Update()
    {
        BrickHighlight();
    }

    private void OnDestroy()
    {
        select.started -= Select_started;
        select.canceled -= Select_canceled;

        cameraControl.started -= CameraControl_started;
        cameraControl.canceled -= CameraControl_canceled;
    }
}
