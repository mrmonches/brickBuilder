using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] private BrickType _brickType;
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private float YOffset;
    [SerializeField] private float FollowSpeed;

    private Rigidbody _rigidbody;

    private bool isPlaced;
    private bool isHeld = false;

    public bool IsPlaced { get => isPlaced; set => isPlaced = value; }
    public bool IsHeld { get => isHeld; set => isHeld = value; }
    public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Script that allows the brick to smoothly follow the mouse
    /// Currently not in use
    /// </summary>
    //private void FollowPlayerMouse()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, AdjustedMousePos(), FollowSpeed * Time.deltaTime);
    //}

    private Vector3 AdjustedMousePos()
    {
        return new Vector3(_playerController.GetSelectedMapPosition().x, _playerController.GetSelectedMapPosition().y
            + YOffset, _playerController.GetSelectedMapPosition().z);
    }

    //private void LateUpdate()
    //{
    //    if (IsHeld)
    //    {
    //        FollowPlayerMouse();
    //    }
    //}
}
