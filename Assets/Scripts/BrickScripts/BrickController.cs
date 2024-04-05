/*****************************************************************************
// File Name : BrickController.cs
// Author : Nolan J. Stein
// Creation Date : March 20, 2024
//
// Brief Description : This is a script that manages the attributes and 
controls how the bricks behave in game.
*****************************************************************************/
using UnityEngine;
using UnityEngine.AI;

public class BrickController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    private NavMeshObstacle _navObstacle;

    [SerializeField] private float YOffset;
    [SerializeField] private float FollowSpeed;
    [SerializeField] private Vector3 OffsetPos;

    private Vector3 placedPos;

    private Rigidbody _rigidbody;

    private bool isPlaced;
    private bool isHeld = false;
    private bool isPlacing;

    [SerializeField] private int DefaultLayer, BrickLayer;

    [SerializeField] private BrickDataSO _brickData;

    private bool lockBrick;

    public bool IsPlaced { get => isPlaced; set => isPlaced = value; }
    public bool IsHeld { get => isHeld; set => isHeld = value; }
    public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }
    public bool IsPlacing { get => isPlacing; set => isPlacing = value; }
    public BrickDataSO BrickData { get => _brickData; set => _brickData = value; }

    /// <summary>
    /// A function that assigns necessary references.
    /// </summary>
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    /// <summary>
    /// A function that allows the brick to follow the mouse.
    /// </summary>
    private void FollowPlayerMouse()
    {
        transform.position = Vector3.MoveTowards(transform.position, AdjustedMousePos(), FollowSpeed * Time.deltaTime);
    }

    /// <summary>
    /// A function that sets the brick's layer to default.
    /// </summary>
    public void SetDefaultLayer()
    {
        gameObject.layer = DefaultLayer;
    }

    /// <summary>
    /// A function that sets the brick's layer to brick.
    /// </summary>
    public void SetBrickLayer()
    {
        gameObject.layer = BrickLayer;
    }

    /// <summary>
    /// A function that returns an adjusted mouse position.
    /// </summary>
    /// <returns></returns> Returns the adjusted mouse position.
    private Vector3 AdjustedMousePos()
    {
        return new Vector3(_playerController.GetSelectedMapPosition().x, _playerController.GetSelectedMapPosition().y
            + YOffset, _playerController.GetSelectedMapPosition().z);
    }

    /// <summary>
    /// A function that brings the brick to the selected outline.
    /// </summary>
    /// <param name="selectedSpot"></param> Parameter based on selected outline.
    public void GoToSelectedSpot(GameObject selectedSpot)
    {
        placedPos = selectedSpot.transform.position;

        // Fix to a bug where bricks other than OneByOne's would be placed into the building plane.
        if (_brickData.BrickType != BrickType.OneByOne)
        {
            transform.position = selectedSpot.transform.position + OffsetPos;
        }
        else
        {
            transform.position = selectedSpot.transform.position;
        }

        // Allows bricks to be rotated to the correct spot based on the outline.
        transform.rotation = selectedSpot.transform.rotation;

        // Makes brick unable to be picked up.
        if (!IsPlacing)
        {
            IsPlacing = true;
        }
    }

    /// <summary>
    /// A function that updates every frame, after Update runs.
    /// </summary>
    private void LateUpdate()
    {
        // Makes sure the brick is only following at certain points.
        if (IsHeld && !IsPlacing)
        {
            FollowPlayerMouse();
        }

        // Locks brick in place
        if (IsPlaced && !lockBrick)
        {
            lockBrick = true;

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
