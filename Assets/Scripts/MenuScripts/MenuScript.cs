/*****************************************************************************
// File Name : MenuScript.cs
// Author : Nolan J. Stein
// Creation Date : March 24, 2024
//
// Brief Description : This is a script that serves as the base for different
menu scripts. Usually used to be inherited for simple functions.
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    /// <summary>
    /// A function that quits the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// A function that reloads the current scene.
    /// </summary>
    public void ReloadScene()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
