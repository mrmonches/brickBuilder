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

    //private Vector3Int AdjustedBrickValue(Vector3 mousePos)
    //{
    //    //return new Vector3Int(_grid.WorldToCell(mousePos).x + _grid.WOrl);
    //}

    //public void GetCurrentBrick(GameObject brick)
    //{
    //    if (brick != null)
    //    {
    //        currentBrick = brick;
    //    }
    //}

    //private void Update()
    //{
    //    if (gridActive)
    //    {
    //        MouseGridInteraction();
    //    }
    //}
}
