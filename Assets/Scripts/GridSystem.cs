using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private GameObject MouseIndicator, CellIndicator;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Grid _grid;

    private void MouseGridInteraction()
    {
        Vector3 mousePosition = _playerController.GetSelectedMapPosition();
        MouseIndicator.transform.position = mousePosition;

        //Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
        //CellIndicator.transform.position = _grid.CellToWorld(gridPosition);
    }

    private void Update()
    {
        //MouseGridInteraction();
    }
}
