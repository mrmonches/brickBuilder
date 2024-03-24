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
    [SerializeField] private CameraController _mainCameraController, _instructionCameraController;


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

            if (_brickController.IsPlacing)
            {
                _brickController.IsPlaced = true;

                _brickController.SetDefaultLayer();

                _brickController.Rigidbody.constraints = UnityEngine.RigidbodyConstraints.FreezeAll;

                Destroy(_outlineController.gameObject);

                _outlineController = null;
            }

            _brickController = null;
        }
    }

    private void CameraControl_started(InputAction.CallbackContext obj)
    {
        _mainCameraController.CameraInput = cameraControl.ReadValue<float>();
        _instructionCameraController.CameraInput = cameraControl.ReadValue<float>();
    }
    private void CameraControl_canceled(InputAction.CallbackContext obj)
    {
        _mainCameraController.CameraInput = 0f;
        _instructionCameraController.CameraInput = 0f;
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
        // If mouse is hovering over a brick and the player is not holding a brick
        if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, BrickMask) 
            && !isHolding)
        {
            // If the brick has a BrickController component and the script doesn't have a reference for BrickController
            if (hit.rigidbody.gameObject.GetComponent<BrickController>() != null && _brickController == null)
            {
                _brickController = hit.rigidbody.gameObject.GetComponent<BrickController>();
            }
            // If the current hit's reference doesn't equal the current reference and the player isn't holding a brick
            else if (hit.rigidbody.gameObject.GetComponent<BrickController>() != _brickController && !isHolding)
            {
                _brickController = hit.rigidbody.gameObject.GetComponent<BrickController>();
            }
        }
        // If mouse is hovering over an outline and is holding a brick
        else if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, OutlineMask)
            && isHolding)
        {
            // If the outline has a OutlineController component and there is no current reference for OutlineController
            if (hit.rigidbody.gameObject.GetComponent<OutlineController>() != null && _outlineController == null)
            {
                _outlineController = hit.rigidbody.gameObject.GetComponent<OutlineController>();
            }
            // If the current hit's reference doesn't equal the current reference and the brick was marked as placing
            else if (hit.rigidbody.gameObject.GetComponent<OutlineController>() != _outlineController && 
                _brickController.IsPlacing)
            {
                _brickController.IsPlacing = false;
            }
            // If the current hit's reference doesn't equal the current reference
            else if (hit.rigidbody.gameObject.GetComponent<OutlineController>() != _outlineController)
            {
                _outlineController = hit.rigidbody.gameObject.GetComponent<OutlineController>();
            }
            // If there is an OutlineController reference and BrickCheck is true
            else if (_outlineController != null && _outlineController.BrickCheck(_brickController.BrickData))
            {
                _brickController.GoToSelectedSpot(hit.rigidbody.gameObject);
            }
        }
        else if (_brickController != null || _outlineController != null) 
        {
            // Used as a catch statement so that the brick isn't infinitely placing
            if (_brickController != null && _brickController.IsPlacing && _brickController.IsHeld)
            {
                _brickController.IsPlacing = false;
            }

            // Makes sure that there isn't a reference at times, important
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
