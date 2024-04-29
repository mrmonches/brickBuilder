/*****************************************************************************
// File Name : PlatformController.cs
// Author : Nolan J. Stein
// Creation Date : April 28, 2024
//
// Brief Description : This is a script that controls the platform's rotation.
// This script's functions replace what CameraController previously did.
*****************************************************************************/
using System.Collections;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private float SlerpDuration;
    [SerializeField] private Transform[] PlatformPositions;

    private bool isRotating;

    private int currentPos;

    public bool IsRotating { get => isRotating; set => isRotating = value; }

    /// <summary>
    /// A function that ensures that values are set correctly.
    /// </summary>
    private void Awake()
    {
        isRotating = false;
        currentPos = 0;
    }

    /// <summary>
    /// A function that manages the calling and setting of rotating the platform.
    /// </summary>
    public void OnRotate()
    {
        isRotating = true;

        if (currentPos >= 3)
        {
            currentPos = 0;
        }
        else
        {
            currentPos++;
        }

        StartCoroutine(SlerpFromTo(SlerpDuration, transform.rotation, PlatformPositions[currentPos].rotation));
    }

    /// <summary>
    /// A function that allows the platform to slerp between two points.
    /// </summary>
    /// <param name="pos1"></param> Function's starting position
    /// <param name="pos2"></param> Function's ending position
    /// <param name="duration"></param> Function's lerp length
    /// <param name="rot1"></param> Function's starting rotation
    /// <param name="rot2"></param> Function's ending rotation
    /// <returns></returns>
    private IEnumerator SlerpFromTo(float duration, Quaternion rot1, Quaternion rot2)
    {
        for (float time = 0f; time < duration; time += Time.deltaTime)
        {
            transform.rotation = Quaternion.Slerp(rot1, rot2, time / duration);
            yield return null;
        }
        transform.rotation = rot2;

        isRotating = false;
    }
}
