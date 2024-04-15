/*****************************************************************************
// File Name : MainMenuScript.cs
// Author : Nolan J. Stein
// Creation Date : March 24, 2024
//
// Brief Description : This is a script that manages the main menu of the 
game. Inherits from MenuScript.
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MenuScript
{
    [SerializeField] private GameObject MainMenu, LevelSelect, TutorialMenu;

    /// <summary>
    /// A function that sets the main menu status, based on the parameter
    /// </summary>
    /// <param name="status"></param> Parameter that indicates status
    public void MainMenuStatus(bool status) 
    {
        MainMenu.SetActive(status);
    }

    /// <summary>
    /// A function that sets the level select status, based on the parameter
    /// </summary>
    /// <param name="status"></param> Parameter that indicates status
    public void LevelSelectStatus(bool status)
    {
        LevelSelect.SetActive(status);
    }

    /// <summary>
    /// A functino that sets the tutorial status, based on the parameter
    /// </summary>
    /// <param name="status"></param> Parameter that indicates status
    public void TutorialStatus(bool status)
    {
        TutorialMenu.SetActive(status);
    }

    /// <summary>
    /// A function that loads the first level
    /// </summary>
    public void LoadLevelOne()
    {
        SceneManager.LoadScene("TestBuild");
    }

    /// <summary>
    /// A function that loads the second level
    /// </summary>
    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("BuildOne");
    }

    /// <summary>
    /// A function that loads the third level
    /// </summary>
    public void LoadLevelThree()
    {
        SceneManager.LoadScene("BuildTwo");
    }
}
