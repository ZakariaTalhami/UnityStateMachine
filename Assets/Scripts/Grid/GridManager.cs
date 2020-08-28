using UnityEngine;
using System;

public class GridManager : MonoSingleton<GridManager>
{

    [Serializable]
    public struct GridMeta
    {
        public Vector3Int origin;
        public int width;
        public int height;
    }

    #region Grid Information
    [Header("Walkable Area Grid")]
    public float gridCellSize;
    public GridMeta walkableGridMeta;

    [Header("Village Area Grid")]
    public GridMeta villageGridMeta;

    [Header("Wood Stockpile Area Grid")]
    public GridMeta woodStockpileGridMeta;

    [Header("Stone Stockpile Area Grid")]
    public GridMeta stoneStockpileGridMeta;
    #endregion

    #region Grid Instances
    private GridArea walkableGrid;
    private SubGrid villageGrid;
    private SubGrid woodStockpileGrid;
    private SubGrid stoneStockpileGrid;
    #endregion

    private void Start()
    {
        walkableGrid = new GridArea(walkableGridMeta.origin, walkableGridMeta.width, walkableGridMeta.height, gridCellSize);
        Color color = Color.blue;
        color.a = 0.1f;
        walkableGrid.ShowDebuglines(color);

        villageGrid = CreateSubGrid(villageGridMeta);
        woodStockpileGrid = CreateSubGrid(woodStockpileGridMeta);
        stoneStockpileGrid = CreateSubGrid(stoneStockpileGridMeta);
    }

    private SubGrid CreateSubGrid(GridMeta gridMeta)
    {
        Vector2Int startingCell = walkableGrid.GetCellofWorldPosition(gridMeta.origin).gridPosition;
        SubGrid subGrid = walkableGrid.GetSubGrid(startingCell, gridMeta.width, gridMeta.height);
        Color color = Color.red;
        color.a = 0.1f;
        subGrid.ShowDebuglines(color);

        return subGrid;
    }

    public IGrid GetFreeVillagePosition(int width, int height, GameObject content)
    {
        IGrid grid = villageGrid.GetFreeArea(width, height);
        return grid;
    }

    public IGrid GetFreeWoodStockpilePosition(int width, int height, GameObject content)
    {
        IGrid grid = woodStockpileGrid.GetFreeArea(width, height);
        return grid;
    }

    public IGrid GetFreeStoneStockpilePosition(int width, int height, GameObject content)
    {
        IGrid grid = stoneStockpileGrid.GetFreeArea(width, height);
        return grid;
    }
}
