/*****************************************************************************
// File Name : LevelEndScript.cs
// Author : Nolan J. Stein
// Creation Date : March 24, 2024
//
// Brief Description : This is a script that manages the end of a level. This
script accounts for the player feedback to understand that they finished the 
level.
*****************************************************************************/
using System.Collections;
using UnityEngine;

public class LevelEndScript : MonoBehaviour
{
    private PlayerController _playerController;

    [SerializeField] private GameObject ParticleEffect;

    [SerializeField] private float LevelEndTimer;

    [SerializeField] private GameObject LevelEndScreen;

    private void Awake()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    /// <summary>
    /// A function that handles the end of the level.
    /// </summary>
    public void OnLevelEnd()
    {
        _playerController.CanRotate = false;

        ParticleEffect.SetActive(true);

        StartCoroutine(FreezeScene());
    }

    /// <summary>
    /// A function that freezes the scene after a specific amount of time.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FreezeScene()
    {
        if (true)
        {
            yield return new WaitForSeconds(LevelEndTimer);

            Time.timeScale = 0f;

            LevelEndScreen.SetActive(true);
        }
    }
}
