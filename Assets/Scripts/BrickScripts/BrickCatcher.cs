/*****************************************************************************
// File Name : BrickCatcher.cs
// Author : Nolan J. Stein
// Creation Date : April 14, 2024
//
// Brief Description : This is a script that makes sure that bricks don't 
fall into the abyss.
*****************************************************************************/
using System.Collections.Generic;
using UnityEngine;

public class BrickCatcher : MonoBehaviour
{
    [SerializeField] private List<Transform> SpawnPoints = new List<Transform>();

    /// <summary>
    /// A function that checks the trigger so bricks don't fall
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
            other.gameObject.transform.transform.position = SpawnPoints[0].position;
        }
    }
}
