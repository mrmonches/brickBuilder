using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera SceneCamera;

    private PlayerInput _playerInput;

    private InputAction select;

    //private Vector3 mousePosition;
    private bool selectActive;

    private Vector3 lastPosition;

    [SerializeField] private float CastDistance;

    [SerializeField] private LayerMask LevelMask;

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
        selectActive = true;
    }
    private void Select_canceled(InputAction.CallbackContext obj)
    {
        selectActive = false;
    }

    //private void OnMouse(InputValue mousePos)
    //{
    //    mousePosition = mousePos.Get<Vector2>();
    //}

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = SceneCamera.nearClipPlane;
        
        RaycastHit hit;
        if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, LevelMask))
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }

    private void GridIndication()
    {
        //Vector3Int gridPosition = _grid.WorldToCell(GetSelectedMapPosition());
    }

    private void Update()
    {
        //MouseIndicator.transform.position = GetSelectedMapPosition();
        //CursorIndicator.transform.position = _grid.CellToWorld(gridPosition)
    }
}
