/*****************************************************************************
// File Name : PlayerController.cs
// Author : Nolan J. Stein
// Creation Date : March 20, 2024
//
// Brief Description : This is a script that handles the player controls and 
how the player interacts with the game world.
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputAction select;
    //private InputAction cameraControl;

    private Vector3 mousePosition;

    [SerializeField] private MenuScript _menuScript;

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

    /// <summary>
    /// A function that runs when the object is awake.
    /// Assigns references to variables and allows player to control the game.
    /// </summary>
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.currentActionMap.Enable();

        select = _playerInput.currentActionMap.FindAction("Select");
        //cameraControl = _playerInput.currentActionMap.FindAction("CameraControl");

        select.started += Select_started;
        select.canceled += Select_canceled;

        //cameraControl.started += CameraControl_started;
        //cameraControl.canceled += CameraControl_canceled;
    }


    /// <summary>
    /// A function that handles when a player selects an object.
    /// </summary>
    /// <param name="obj"></param> Parameter that comes from input.
    private void Select_started(InputAction.CallbackContext obj)
    {
        // Makes sure that there is a brick reference and the brick isn't placed.
        if (_brickController != null && !_brickController.IsPlaced)
        {
            _brickController.IsHeld = true;

            isHolding = true;

            _brickController.Rigidbody.useGravity = false;

            _brickController.Rigidbody.excludeLayers = BrickMask;

            _brickController.SetDefaultLayer();
        }
    }

    /// <summary>
    /// A function that handles when a player releases an object.
    /// </summary>
    /// <param name="obj"></param> Parameter that comes from input.
    private void Select_canceled(InputAction.CallbackContext obj)
    {
        // Makes sure there is a brick reference and that the brick is held and not placed.
        if (_brickController != null && _brickController.IsHeld && !_brickController.IsPlaced)
        {
            _brickController.IsHeld = false;

            isHolding = false;

            _brickController.Rigidbody.useGravity = true;

            _brickController.Rigidbody.excludeLayers = default;

            _brickController.SetBrickLayer();

            // Makes sure brick is placed.
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

    ///// <summary>
    ///// A function that handles the camera rotation based on player input.
    ///// </summary>
    ///// <param name="obj"></param> Parameter that comes from input.
    //private void CameraControl_started(InputAction.CallbackContext obj)
    //{
    //    _mainCameraController.CameraInput = cameraControl.ReadValue<float>();
    //    _instructionCameraController.CameraInput = cameraControl.ReadValue<float>();
    //}

    ///// <summary>
    ///// A function that resets the camera rotation based on player input.
    ///// </summary>
    ///// <param name="obj"></param> Parameter that comes from input.
    //private void CameraControl_canceled(InputAction.CallbackContext obj)
    //{
    //    _mainCameraController.CameraInput = 0f;
    //    _instructionCameraController.CameraInput = 0f;
    //}

    private void OnCameraControl()
    {
        if (!_mainCameraController.IsMoving && !_instructionCameraController.IsMoving)
        {
            _mainCameraController.CameraShift();
            _instructionCameraController.CameraShift();
        }
    }

    /// <summary>
    /// A function that assigns mouse position on mouse movement.
    /// </summary>
    /// <param name="mousePos"></param> Parameter that comes from input.
    private void OnMouse(InputValue mousePos)
    {
        mousePosition = mousePos.Get<Vector2>();
    }

    /// <summary>
    /// A function that reloads the current scene based on player input.
    /// </summary>
    private void OnReload()
    {
        _menuScript.ReloadScene();
    }

    /// <summary>
    /// A function that quits the game based on player input.
    /// </summary>
    private void OnQuit()
    {
        _menuScript.QuitGame();
    }

    /// <summary>
    /// A function that returns the map position based on mouse position.
    /// </summary>
    /// <returns></returns> Returns the desired map position.
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

    /// <summary>
    /// A function that handles the player-brick-outline interaction.
    /// Very complicated and bloated, potential for optimization.
    /// Currently arranged to catch any outlying conditions for best player experience.
    /// </summary>
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

    /// <summary>
    /// A function that is called every frame.
    /// </summary>
    private void Update()
    {
        BrickHighlight();
    }

    /// <summary>
    /// A function that is called on end of play.
    /// </summary>
    private void OnDestroy()
    {
        select.started -= Select_started;
        select.canceled -= Select_canceled;

        //cameraControl.started -= CameraControl_started;
        //cameraControl.canceled -= CameraControl_canceled;
    }
}