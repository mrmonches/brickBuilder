/*****************************************************************************
// File Name : MusicContinueScript.cs
// Author : Nolan J. Stein
// Creation Date : May 5, 2024
//
// Brief Description : This is a script that ensures the music plays across 
scenes.
*****************************************************************************/
using UnityEngine;

public class MusicContinueScript : MonoBehaviour
{
    /// <summary>
    /// A function that runs on object being awake
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
