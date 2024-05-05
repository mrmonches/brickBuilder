/*****************************************************************************
// File Name : PauseMenuScript.cs
// Author : Nolan J. Stein
// Creation Date : March 24, 2024
//
// Brief Description : This is a script that has been repurposed to manage
functions inside of the level, when UI is being used. It is not for a pause
menu anymore, but it is easier to continue using this script instead of
rewriting and attaching a new script.
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MenuScript
{
    [SerializeField] private GameObject PauseScreen;

    [SerializeField] private int CurrentSceneIndex;

    /// <summary>
    /// A function that pauses the game
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseScreen.SetActive(true);
    }

    /// <summary>
    /// A function that unpauses the game
    /// </summary>
    public void UnpauseGame()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// A function that loads the main menu
    /// </summary>
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenuScene");
    }

    /// <summary>
    /// A function that loads the next possible scene
    /// </summary>
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(CurrentSceneIndex + 1);
    }
}
