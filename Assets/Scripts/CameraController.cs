using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float cameraInput;
    [SerializeField] private GameObject BuildingPlane;

    public float CameraInput { get => cameraInput; set => cameraInput = value; }

    private void FixedUpdate()
    {
        if (cameraInput != 0)
        {
            transform.RotateAround(BuildingPlane.transform.position, Vector3.up, cameraInput);
        }
    }
}
