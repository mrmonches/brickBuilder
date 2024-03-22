using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputAction select;

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

    public bool IsHolding { get => isHolding; set => isHolding = value; }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.currentActionMap.Enable();

        select = _playerInput.currentActionMap.FindAction("Select");

        select.started += Select_started;
        select.canceled += Select_canceled;
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
        if (_brickController != null && _brickController.IsHeld)
        {
            _brickController.IsHeld = false;

            isHolding = false;

            _brickController.Rigidbody.useGravity = true;

            _brickController.Rigidbody.excludeLayers = default;

            _brickController.SetBrickLayer();
        }
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
            // If this script has a BrickController reference and the brick is not held
            else if (_brickController != null && !_brickController.IsHeld)
            {
                _brickController = null;
            }
        }
        else if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, OutlineMask))
        {
            if (hit.rigidbody.gameObject.GetComponent<OutlineController>() != null && _outlineController == null)
            {
                _outlineController = hit.rigidbody.gameObject.GetComponent<OutlineController>();

                //if (_outlineController.OutlineCheck)
            }
        }
    }

    private void Update()
    {
        BrickHighlight();
    }
}
