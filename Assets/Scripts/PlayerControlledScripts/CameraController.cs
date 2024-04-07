/*****************************************************************************
// File Name : CameraController.cs
// Author : Nolan J. Stein
// Creation Date : March 23, 2024
//
// Brief Description : This is a script that controls the camera position.
*****************************************************************************/
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float CameraAngle;
    [SerializeField] private GameObject BuildingPlane;

    [SerializeField] private Transform[] CameraPositions;
    [SerializeField] private float RotationDuration;
    private int currentPos = 0;

    private bool _isMoving;

    public bool IsMoving { get => _isMoving; set => _isMoving = value; }

    private void Awake()
    {
        _isMoving = false;
    }

    public void CameraShift()
    {
        transform.RotateAround(BuildingPlane.transform.position, Vector3.up, CameraAngle);

        //_isMoving = true;

        //if (currentPos >= 3)
        //{
        //    currentPos = 0;
        //}
        //else
        //{
        //    currentPos++;
        //}

        //StartCoroutine(LerpFromTo(transform.position, CameraPositions[currentPos].position, RotationDuration,
        //    transform.rotation, CameraPositions[currentPos].rotation));
    }

    private void Update()
    {
        //if (_isMoving && transform.position != CameraPositions[currentPos].transform.position)
        //{
        //    transform.RotateAround(BuildingPlane.transform.position, Vector3.up, CameraAngle);
        //}
        //else if (_isMoving && transform.position == CameraPositions[currentPos].transform.position)
        //{
        //    _isMoving = false;

        //    transform.position = CameraPositions[currentPos].transform.position;
        //    transform.rotation = CameraPositions[currentPos].transform.rotation;
        //}
    }

    private IEnumerator LerpFromTo(Vector3 pos1, Vector3 pos2, float duration, Quaternion rot1, Quaternion rot2)
    {
        for (float time = 0f; time < duration; time += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(pos1, pos2, time / duration);
            transform.rotation = Quaternion.Lerp(rot1, rot2, time / duration);
            yield return null;
        }
        transform.position = pos2;
        transform.rotation = rot2;
        _isMoving = false;
    }
}