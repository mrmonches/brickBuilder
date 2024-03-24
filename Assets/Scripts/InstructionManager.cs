using System.Collections.Generic;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    [SerializeField] private int PageCount;
    [SerializeField] private int CurrentPage;

    [SerializeField] private List<InstructionSO> InstructionPages = new List<InstructionSO>();

    public void IncrementPage()
    {
        if (CurrentPage < PageCount)
        {
            DeactivatePage(CurrentPage);
            CurrentPage++;
            ActivatePage(CurrentPage);
        }
    }

    public void DecrementPage()
    {
        if (CurrentPage > 2)
        {
            DeactivatePage(CurrentPage);
            CurrentPage--;
            ActivatePage(CurrentPage);
        }
    }

    private void DeactivatePage(int page)
    {
        int instructionIndex = InstructionPages[page].PageObjects.Count;

        for (int i = 0; i < instructionIndex; i++)
        {

        }
    }

    private void ActivatePage(int page)
    {

    }
}
