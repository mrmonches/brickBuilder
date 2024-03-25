/*****************************************************************************
// File Name : GridSystem.cs
// Author : Nolan J. Stein
// Creation Date : March 21, 2024
//
// Brief Description : This is a script that was being used for the grid 
system. This script is currently not in use.
*****************************************************************************/
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private GameObject MouseIndicator;

    private GameObject currentBrick;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Grid _grid;

    [SerializeField] private Vector3Int BrickOffset;

    private bool gridActive;

    public bool GridActive { get => gridActive; set => gridActive = value; }

    /// <summary>
    /// A function that snaps objects in-line with the grid.
    /// </summary>
    private void MouseGridInteraction()
    {
        Vector3 mousePosition = _playerController.GetSelectedMapPosition();
        MouseIndicator.transform.position = mousePosition;

        //if (_playerController.IsHolding)
        //{
        //    Vector3Int gridPosition = _grid.WorldToCell(mousePosition) + BrickOffset;
        //    currentBrick.transform.position = _grid.CellToWorld(gridPosition);
        //}
        //else
        //{
        //    currentBrick = null;
        //}
    }

    /// <summary>
    /// A function that gets an adjusted brick value.
    /// </summary>
    /// <param name="mousePos"></param> Parameter for the mouse position.
    /// <returns></returns> Returns a Vector3Int of adjusted position.
    //private Vector3Int AdjustedBrickValue(Vector3 mousePos)
    //{
    //    //return new Vector3Int(_grid.WorldToCell(mousePos).x + _grid.WOrl);
    //}

    /// <summary>
    /// A function that gets the current brick.
    /// </summary>
    /// <param name="mousePos"></param> Parameter for the current brick.
    //public void GetCurrentBrick(GameObject brick)
    //{
    //    if (brick != null)
    //    {
    //        currentBrick = brick;
    //    }
    //}

    /// <summary>
    /// A function that is called every frame.
    /// </summary>
    //private void Update()
    //{
    //    if (gridActive)
    //    {
    //        MouseGridInteraction();
    //    }
    //}
}
