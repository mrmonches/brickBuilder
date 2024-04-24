/*****************************************************************************
// File Name : BrickController.cs
// Author : Nolan J. Stein
// Creation Date : March 20, 2024
//
// Brief Description : This is a script that manages the attributes and 
controls how the bricks behave in game.
*****************************************************************************/
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private float YOffset;
    [SerializeField] private float SlerpSpeed;
    [SerializeField] private Vector3 OffsetPos;

    private Vector3 placedPos;

    private Rigidbody _rigidbody;

    private BoxCollider _boxCollider;

    private bool isPlaced;
    private bool isHeld = false;
    private bool isPlacing;

    [SerializeField] private int DefaultLayer, BrickLayer, PlaceLayer;
        
    [SerializeField] private LayerMask BrickMask;

    [SerializeField] private BrickDataSO _brickData;

    //private NavMeshObstacle brickObstacle;

    private bool lockBrick;

    //private CompanionController _companionController;

    [Header("Brick-hover variables")]
    [SerializeField] private float HoverHeight;
    [SerializeField] private float HoverSpeed;

    private Vector3 hoverPos;

    private bool isHovering;

    public bool IsPlaced { get => isPlaced; set => isPlaced = value; }
    public bool IsHeld { get => isHeld; set => isHeld = value; }
    public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }
    public bool IsPlacing { get => isPlacing; set => isPlacing = value; }
    public BrickDataSO BrickData { get => _brickData; set => _brickData = value; }
    public BoxCollider BoxCollider { get => _boxCollider; set => _boxCollider = value; }
    public bool IsHovering { get => isHovering; set => isHovering = value; }

    /// <summary>
    /// A function that assigns necessary references
    /// </summary>
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _boxCollider = GetComponent<BoxCollider>();

        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (gameObject.layer != 9)
        {
            //_companionController = GameObject.Find("Tiny").GetComponent<CompanionController>();

            //_companionController.CurrentBricks.Add(gameObject);

            //brickObstacle = GetComponent<NavMeshObstacle>();
        }
    }

    /// <summary>
    /// A function that allows the brick to follow the mouse
    /// </summary>
    private void FollowPlayerMouse()
    {
        transform.position = Vector3.Slerp(transform.position, AdjustedMousePos(), SlerpSpeed * Time.deltaTime);
    }

    /// <summary>
    /// A function that sets the brick's layer to default
    /// </summary>
    public void SetDefaultLayer()
    {
        gameObject.layer = DefaultLayer;
    }

    /// <summary>
    /// A function that sets the brick's layer to brick
    /// </summary>
    public void SetBrickLayer()
    {
        gameObject.layer = BrickLayer;
    }

    /// <summary>
    /// A function that sets the brick's layer to the placing plane
    /// </summary>
    public void SetPlacingLayer()
    {
        gameObject.layer = PlaceLayer;
    }

    /// <summary>
    /// A function that returns an adjusted mouse position
    /// </summary>
    /// <returns></returns> Returns the adjusted mouse position
    private Vector3 AdjustedMousePos()
    {
        return new Vector3(_playerController.GetSelectedMapPosition().x, _playerController.GetSelectedMapPosition().y
            + YOffset, _playerController.GetSelectedMapPosition().z);
    }

    /// <summary>
    /// A function that brings the brick to the selected outline
    /// </summary>
    /// <param name="selectedSpot"></param> Parameter based on selected outline
    public void GoToSelectedSpot(GameObject selectedSpot)
    {
        placedPos = selectedSpot.transform.position;

        // Fix to a bug where bricks other than OneByOne's would be placed into the building plane
        if (_brickData.BrickType != BrickType.OneByOne)
        {
            transform.position = Vector3.Slerp(transform.position, selectedSpot.transform.position + OffsetPos, 
                SlerpSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Slerp(transform.position, selectedSpot.transform.position, 
                SlerpSpeed * Time.deltaTime);
        }

        // Allows bricks to be rotated to the correct spot based on the outline
        transform.rotation = selectedSpot.transform.rotation;

        // Makes brick unable to be picked up
        if (!IsPlacing)
        {
            IsPlacing = true;
        }
    }

    /// <summary>
    /// A function that disables the brick's gravity
    /// </summary>
    public void DisableGravity()
    {
        _rigidbody.useGravity = false;
    }

    /// <summary>
    /// A function that enables the brick's gravity
    /// </summary>
    private void EnableGravity()
    {
        _rigidbody.useGravity = true;
    }

    /// <summary>
    /// A function that makes brick variables match conditions when picked up by player
    /// </summary>
    public void OnPickup()
    {
        _rigidbody.useGravity = false;

        _rigidbody.excludeLayers = BrickMask;

        _boxCollider.isTrigger = true;

        //brickObstacle.enabled = false; 

        SetDefaultLayer();
    }

    /// <summary>
    /// A function that makes brick variables match conditions when dropped by player
    /// </summary>
    public void OnDrop()
    {
        _rigidbody.useGravity = true;

        _rigidbody.excludeLayers = default;

        _boxCollider.isTrigger = false;

        //brickObstacle.enabled = true;

        SetBrickLayer();
    }

    /// <summary>
    /// A function that allows the brick to hover whenever the player's cursor is over the brick
    /// Currently not in use
    /// </summary>
    public void OnHover()
    {
        if (!isHovering)
        {
            isHovering = true;

            DisableGravity();

            HoverPosition();
        }
    }

    /// <summary>
    /// A function that allows the brick to unhover whenever the player's cursor leaves the brick
    /// Currently not in use
    /// </summary>
    public void OnUnhover()
    {
        if(isHovering)
        {
            isHovering = false;

            EnableGravity();
        }
    }

    /// <summary>
    /// A function that handles the brick hover
    /// </summary>
    private void HoverSlerp()
    {
        transform.position = Vector3.Slerp(transform.position, hoverPos, HoverSpeed * Time.deltaTime);
    }

    /// <summary>
    /// A function that calculates a new hover position when called
    /// </summary>
    private void HoverPosition()
    {
        hoverPos = new Vector3(transform.position.x, transform.position.y + HoverHeight, transform.position.z);
    }

    /// <summary>
    /// A function that updates every frame, after Update runs
    /// </summary>
    private void LateUpdate()
    {
        // Makes sure the brick is only following at certain points
        if (IsHeld && !IsPlacing)
        {
            FollowPlayerMouse();
        }

        if (isHovering)
        {
            HoverSlerp();
        }

        // Locks brick in place
        if (IsPlaced && !lockBrick)
        {
            lockBrick = true;

            //brickObstacle.size = ExpandedCarve;

            //_companionController.RemoveBrickFromRange(gameObject);

            if (_brickData.BrickType != BrickType.OneByOne)
            {
                transform.position = placedPos + OffsetPos;
            }
            else
            {
                transform.position = placedPos;
            }
        }
    }
}
