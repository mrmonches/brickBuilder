using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    [SerializeField] private int PageCount;
    [SerializeField] private int CurrentPage;

    [SerializeField] private List<GameObject> InstructionPages = new List<GameObject>();
    [SerializeField] private List<GameObject> InstructionObjects = new List<GameObject>();

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

    private void DeactivatePage(int page)
    {
        InstructionPages[page].SetActive(false);
    }

    private void DeactivateExample(int page)
    {
        InstructionObjects[page].SetActive(false);
    }

    private void ActivatePage(int page)
    {
        InstructionPages[page].SetActive(true);
    }

    private void ActivateExample(int page)
    {
        InstructionObjects[page].SetActive(true);
    }
}
