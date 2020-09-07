using UnityEngine;

public class SubGrid : IGrid
{
    public int width { get; private set; }
    public int height { get; private set; }
    private IGrid parentGrid;
    // Might be confusing with the Vector3 origin in Grid
    public Vector2Int origin;

    public SubGrid(IGrid parentGrid, Vector2Int origin, int width, int height)
    {
        this.parentGrid = parentGrid;
        this.origin = origin;
        this.width = width;
        this.height = height;
    }

    public void ShowDebuglines(Color color)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Debug.DrawLine(GetWorldPostion(x, y), GetWorldPostion(x + 1, y), color, 100f);
                Debug.DrawLine(GetWorldPostion(x, y), GetWorldPostion(x, y + 1), color, 100f);
            }
        }
        Debug.DrawLine(GetWorldPostion(0, height), GetWorldPostion(width, height), color, 100f);
        Debug.DrawLine(GetWorldPostion(width, 0), GetWorldPostion(width, height), color, 100f);
    }

    public IGrid GetFreeArea(int width, int height)
    {
        SubGrid subGrid = null;
        for (int y = 0; y < this.height - height; y++)
        {
            for (int x = 0; x < this.width - width; x++)
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
            int x = Random.Range(0, this.width - width);
            int y = Random.Range(0, this.height - height);

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
        return parentGrid.GetCellofWorldPosition(worldPosition);
    }

    public Vector3 GetWorldPositionOfCell(int x, int y)
    {
        return parentGrid.GetWorldPositionOfCell(x + origin.x, y + origin.y);
    }

    public Vector3 GetWorldPositionOfCell(Vector2Int cellPosition)
    {
        return parentGrid.GetWorldPositionOfCell(cellPosition + origin);
    }

    public Vector3 GetWorldPostion(int x, int y)
    {
        return parentGrid.GetWorldPostion(x + origin.x, y + origin.y);
    }

    public SubGrid GetSubGrid(Vector2Int origin, int width, int height)
    {
        return new SubGrid(this, origin, width, height);
    }

    public bool IsCompleteltyFree()
    {
        bool isFree = true;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (GetCell(x, y).isOccupied == true)
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
        SubGrid subGrid = new SubGrid(this, origin, areaWidth, areaHeight);
        return subGrid.IsCompleteltyFree();
    }

    public void SetCellContent(int x, int y, GameObject gameObject)
    {
        parentGrid.SetCellContent(x + origin.x, y + origin.y, gameObject);
    }

    public void SetGridContent(GameObject gameObject)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                SetCellContent(x, y, gameObject);
            }
        }
    }

    public GridCell GetCell(int x, int y)
    {
        return parentGrid.GetCell(x + origin.x, y + origin.y);
    }

    public Vector3 GetOriginWorldPosition()
    {
        return GetCell(0,0).worldPosition;
    }

}