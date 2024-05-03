/*****************************************************************************
// File Name : BrickController.cs
// Author : Nolan J. Stein
// Creation Date : March 22, 2024
//
// Brief Description : This is a script that manages all of the attributes for
the outlines in the game. Includes comparing bricks to the outline's data.
*****************************************************************************/
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    [SerializeField] private BrickDataSO OutlineData;

    private bool outlineCheck = true;

    private BrickController _brickController;

    [Header("Only for 4x1 & 10x1 Outlines")]
    [SerializeField] private Transform _adjustedTransform;

    public bool OutlineCheck { get => outlineCheck; private set => outlineCheck = value; }
    public Transform AdjustedTransform { get => _adjustedTransform; set => _adjustedTransform = value; }

    /// <summary>
    /// A function that will return if the brick has the same attributes as the outline.
    /// </summary>
    /// <param name="data"></param> The input for brick data.
    /// <returns></returns> Returns true if same attributes, false if otherwise.
    public bool BrickCheck(BrickDataSO data)
    {
        if (data.BrickColor == OutlineData.BrickColor && data.BrickType == OutlineData.BrickType)
        {
            return true;
        }
        else
        {
            return false;
        } 
    }

    /// <summary>
    /// A function that will return if the brick has the same attributes as the outline.
    /// </summary>
    /// <param name="brick"></param> The input for brick object.
    /// <returns></returns> Returns true if same attributes, false if otherwise.
    public bool BrickCheck(GameObject brick)
    {
        if (brick.CompareTag("Brick") && brick.GetComponent<BrickController>() != null)
        {
            _brickController = brick.GetComponent<BrickController>();

            BrickDataSO brickData = _brickController.BrickData;

            if (brickData.BrickColor == OutlineData.BrickColor && brickData.BrickType == OutlineData.BrickType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
