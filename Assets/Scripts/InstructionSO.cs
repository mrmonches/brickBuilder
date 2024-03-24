using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InstructionsPage", menuName = "Instructions Page")]
public class InstructionSO : ScriptableObject
{
    [SerializeField] private List<GameObject> _pageObjects = new List<GameObject>();

    public List<GameObject> PageObjects { get => _pageObjects; set => _pageObjects = value; }
}
