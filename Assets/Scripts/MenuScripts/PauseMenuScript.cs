/*****************************************************************************
// File Name : PauseMenuScript.cs
// Author : Nolan J. Stein
// Creation Date : March 24, 2024
//
// Brief Description : This is a script that pauses gameplay and presents
menu options.
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MenuScript
{
    [SerializeField] private GameObject PauseScreen;

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
}
