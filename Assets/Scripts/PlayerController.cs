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

    // Brick Settings
    [SerializeField] private BrickController _brickController;

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
        }
    }
    private void Select_canceled(InputAction.CallbackContext obj)
    {
        if (_brickController != null && _brickController.IsHeld)
        {
            _brickController.IsHeld = false;
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
            if (hit.rigidbody.gameObject.GetComponent<BrickController>() != null)
            {
                _brickController = hit.rigidbody.gameObject.GetComponent<BrickController>();
            }
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
