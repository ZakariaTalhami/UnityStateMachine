using UnityEngine;
using System.Linq;
using UnityEditor;

public class GridArea : IGrid
{
    public int width { get; private set; }
    public int height { get; private set; }
    public float cellSize { get; private set; }
    public Vector3 origin { get; private set; }
    private GridCell[,] cells;

    public GridArea(Vector3 origin, int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;
        ConstructCells(this.width, this.height);
    }

    private void ConstructCells(int width, int height)
    {
        cells = new GridCell[this.width, this.height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = new GridCell(new Vector2Int(x, y), GetWorldPostion(x, y));
            }
        }
    }

    public void ShowDebuglines(Color color)
    {
        int count = 1;
        for (int y = 0; y < cells.GetLength(1); y++)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                Debug.DrawLine(GetWorldPostion(x, y), GetWorldPostion(x + 1, y), color, 100f);
                Debug.DrawLine(GetWorldPostion(x, y), GetWorldPostion(x, y + 1), color, 100f);
                count++;
            }
        }
        Debug.DrawLine(GetWorldPostion(0, cells.GetLength(1)), GetWorldPostion(cells.GetLength(0), cells.GetLength(1)), color, 100f);
        Debug.DrawLine(GetWorldPostion(cells.GetLength(0), 0), GetWorldPostion(cells.GetLength(0), cells.GetLength(1)), color, 100f);

        Debug.DrawLine(GetWorldPostion(0, 0), GetWorldPostion(1, 0), Color.green, 100f);
    }

    public IGrid GetFreeArea(int width, int height)
    {
        SubGrid subGrid = null;
        for (int y = 0; y < cells.GetLength(1) - height; y++)
        {
            for (int x = 0; x < cells.GetLength(0) - width; x++)
            {
                if (IsAreaFree(new Vector2Int(x, y), width, height))
                {
                    subGrid = GetSubGrid(new Vector2Int(x, y), width, height);
                    break;
                }
            }
            if (subGrid != null) break;
        }

        return subGrid;
    }

    public IGrid GetRandomFreeArea(int width, int height)
    {
        SubGrid subGrid = null;
        int retries = 5;
        for (int i = 0; i < retries; i++)
        {
            int x = Random.Range(0, cells.GetLength(0) - width);
            int y = Random.Range(0, cells.GetLength(1) - height);

            if (IsAreaFree(new Vector2Int(x, y), width, height))
            {
                subGrid = GetSubGrid(new Vector2Int(x, y), width, height);
                break;
            }
        }
        return subGrid;
    }

    public GridCell GetCellofWorldPosition(Vector3 worldPosition)
    {
        // Min and Max to keep the result in the range of the cells array
        int x = Mathf.Min(cells.GetUpperBound(0), Mathf.Max(0, Mathf.FloorToInt((worldPosition - origin).x / cellSize)));
        int y = Mathf.Min(cells.GetUpperBound(1), Mathf.Max(0, Mathf.FloorToInt((worldPosition - origin).z / cellSize)));

        return cells[x, y];
    }

    public Vector3 GetWorldPositionOfCell(int x, int y)
    {
        // TODO: Handle out of range
        return cells[x, y].worldPosition;
    }

    public Vector3 GetWorldPositionOfCell(Vector2Int cellPosition)
    {
        return GetWorldPositionOfCell(cellPosition.x, cellPosition.y);
    }

    public Vector3 GetWorldPostion(int x, int y)
    {
        return origin + new Vector3(x, 0, y) * cellSize;
    }

    public SubGrid GetSubGrid(Vector2Int origin, int width, int height)
    {
        return new SubGrid(this, origin, width, height);
    }

    public bool IsCompleteltyFree()
    {
        bool isFree = true;
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                if (cells[x, y].isOccupied == true)
                {
                    isFree = false;
                    break;
                }
            }
        }
        return isFree;
    }

    public bool IsAreaFree(Vector2Int origin, int areaWidth, int areaHeight)
    {
        // use subgrid to evaluate
        SubGrid subGrid = new SubGrid(this, origin, areaWidth, areaHeight);
        return subGrid.IsCompleteltyFree();
    }

    public void SetCellContent(int x, int y, GameObject gameObject)
    {
        cells[x, y].gameObject = gameObject;
        cells[x, y].isOccupied = true;
    }

    public void SetGridContent(GameObject gameObject)
    {
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                SetCellContent(x, y, gameObject);
            }
        }
    }

    public GridCell GetCell(int x, int y)
    {
        return cells[x, y];
    }
    
    public Vector3 GetOriginWorldPosition()
    {
        return origin;
    }

}