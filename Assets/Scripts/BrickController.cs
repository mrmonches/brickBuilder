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
    private bool isPlacing;

    [SerializeField] private int DefaultLayer, BrickLayer;

    public bool IsPlaced { get => isPlaced; set => isPlaced = value; }
    public bool IsHeld { get => isHeld; set => isHeld = value; }
    public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }
    public bool IsPlacing { get => isPlacing; set => isPlacing = value; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    /// <summary>
    /// Script that allows the brick to smoothly follow the mouse
    /// </summary>
    private void FollowPlayerMouse()
    {
        transform.position = Vector3.MoveTowards(transform.position, AdjustedMousePos(), FollowSpeed * Time.deltaTime);
    }

    public void SetDefaultLayer()
    {
        gameObject.layer = DefaultLayer;
    }

    public void SetBrickLayer()
    {
        gameObject.layer = BrickLayer;
    }

    private Vector3 AdjustedMousePos()
    {
        return new Vector3(_playerController.GetSelectedMapPosition().x, _playerController.GetSelectedMapPosition().y
            + YOffset, _playerController.GetSelectedMapPosition().z);
    }

    public void GoToSelectedSpot(GameObject selectedSpot)
    {
        transform.position = selectedSpot.transform.position;

        if (!IsPlacing)
        {
            IsPlacing = true;
        }
    }

    private void LateUpdate()
    {
        if (IsHeld && !IsPlacing)
        {
            FollowPlayerMouse();
        }
    }
}
