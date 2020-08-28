using UnityEngine;

public class GridCell
{
    public Vector2Int gridPosition { get; private set; }
    public bool isOccupied { get; set; }
    public Vector3 worldPosition { get; private set; }
    public GameObject gameObject { get; set; }

    public GridCell(Vector2Int gridPosition, Vector3 worldPosition)
    {
        this.gridPosition = gridPosition;
        this.worldPosition = worldPosition;
        this.isOccupied = false;
    }
}