/*****************************************************************************
// File Name : BrickDataSO.cs
// Author : Nolan J. Stein
// Creation Date : March 20, 2024
//
// Brief Description : This is a script that allows for quick data on bricks.
*****************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "BrickData", menuName = "Brick Data")]
public class BrickDataSO : ScriptableObject
{
    [SerializeField] private BrickType _brickType;
    [SerializeField] private BrickColor _brickColor;

    public BrickType BrickType { get => _brickType; private set => _brickType = value; }
    public BrickColor BrickColor { get => _brickColor; private set => _brickColor = value; }
}
