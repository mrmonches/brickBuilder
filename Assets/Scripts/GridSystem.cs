using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private GameObject MouseIndicator, CellIndicator;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Grid _grid;

    private void Update()
    {
        Vector3 mousePosition = _playerController.GetSelectedMapPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
        MouseIndicator.transform.position = mousePosition;
        CellIndicator.transform.position = _grid.CellToWorld(gridPosition);
    }
}
