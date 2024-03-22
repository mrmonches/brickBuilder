using UnityEngine;

[CreateAssetMenu(fileName = "BrickData", menuName = "Brick Data")]
public class BrickDataSO : ScriptableObject
{
    [SerializeField] private BrickType _brickType;
    [SerializeField] private Color _color;

    public BrickType BrickType { get => _brickType; private set => _brickType = value; }
    public Color Color { get => _color; private set => _color = value; }
}
