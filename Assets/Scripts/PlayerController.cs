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

    [SerializeField] private LayerMask LevelMask, BrickMask;
    private LayerMask blankMask;

    // Brick Settings
    private BrickController _brickController;
    private bool isHolding;

    [SerializeField] private GridSystem _gridSystem;

    public bool IsHolding { get => isHolding; set => isHolding = value; }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.currentActionMap.Enable();

        select = _playerInput.currentActionMap.FindAction("Select");

        select.started += Select_started;
        select.canceled += Select_canceled;

        _gridSystem = GetComponent<GridSystem>();
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
        if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, BrickMask))
        {
            if (hit.rigidbody.gameObject.GetComponent<BrickController>() != null && _brickController == null)
            {
                _brickController = hit.rigidbody.gameObject.GetComponent<BrickController>();
            }
            else if (_brickController != null && _brickController.IsHeld)
            {
                _brickController.GoToSelectedSpot(hit.rigidbody.gameObject);
            }
        }
        else if (_brickController != null && _brickController.IsPlacing && _brickController.IsHeld)
        {
            _brickController.IsPlacing = false;
        }
        else if (_brickController != null && !_brickController.IsHeld)
        {
            _brickController = null;
        }
    }

    private void Update()
    {
        BrickHighlight();
    }
}
