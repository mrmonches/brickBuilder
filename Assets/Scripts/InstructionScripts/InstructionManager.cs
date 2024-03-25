/*****************************************************************************
// File Name : InstructionManager.cs
// Author : Nolan J. Stein
// Creation Date : March 24, 2024
//
// Brief Description : This is a script that manages the game's instruction
"pages". Will deactivate and activate certain pages. 
*****************************************************************************/
using System.Collections.Generic;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    [SerializeField] private int PageCount;
    [SerializeField] private int CurrentPage;

    [SerializeField] private List<GameObject> InstructionPages = new List<GameObject>();
    [SerializeField] private List<GameObject> InstructionObjects = new List<GameObject>();

    /// <summary>
    /// A function that will increment the page count.
    /// Also handles deactivating and activating certain portions.
    /// </summary>
    public void IncrementPage()
    {
        if (CurrentPage < PageCount && CanProgress())
        {
            DeactivatePage(CurrentPage);
            CurrentPage++;
            ActivatePage(CurrentPage);
            ActivateExample(CurrentPage);
        }
    }

    /// <summary>
    /// A function that checks if the player can progress beyond the current page.
    /// </summary>
    /// <returns></returns> Returns true if the player can progress, false if otherwise.
    private bool CanProgress()
    {
        if (InstructionPages[CurrentPage].GetComponentInChildren<OutlineController>() == null)
        {
            return true;
        } 
        else
        {
            return false;
        }
    }

    /// <summary>
    /// A function that will decrease the page count.
    /// Also handles deactivating and activating certain portions.
    /// </summary>
    public void DecrementPage()
    {
        if (CurrentPage > 0)
        {
            DeactivatePage(CurrentPage);
            DeactivateExample(CurrentPage);
            CurrentPage--;
            ActivatePage(CurrentPage);
        }
    }

    /// <summary>
    /// Responsible for deactivating a page.
    /// </summary>
    /// <param name="page"></param> Input that is the current page.
    private void DeactivatePage(int page)
    {
        InstructionPages[page].SetActive(false);
    }

    /// <summary>
    /// Responsible for deactivating an example.
    /// </summary>
    /// <param name="page"></param> Input that is the current page.
    private void DeactivateExample(int page)
    {
        InstructionObjects[page].SetActive(false);
    }

    /// <summary>
    /// Responsible for activating a page.
    /// </summary>
    /// <param name="page"></param> Input that is the current page.
    private void ActivatePage(int page)
    {
        InstructionPages[page].SetActive(true);
    }

    /// <summary>
    /// Responsible for activating an example.
    /// </summary>
    /// <param name="page"></param> Input that is the current page.
    private void ActivateExample(int page)
    {
        InstructionObjects[page].SetActive(true);
    }
}
