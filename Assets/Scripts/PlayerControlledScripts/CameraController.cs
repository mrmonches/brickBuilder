/*****************************************************************************
// File Name : CameraController.cs
// Author : Nolan J. Stein
// Creation Date : March 23, 2024
//
// Brief Description : This is a script that controls the camera position.
*****************************************************************************/
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float cameraInput;
    [SerializeField] private GameObject BuildingPlane;

    public float CameraInput { get => cameraInput; set => cameraInput = value; }

    /// <summary>
    /// A function that updates on a fixed timer.
    /// Allows the player to rotate around the Building Plane.
    /// </summary>
    private void FixedUpdate()
    {
        if (cameraInput != 0)
        {
            transform.RotateAround(BuildingPlane.transform.position, Vector3.up, cameraInput);
        }
    }
}