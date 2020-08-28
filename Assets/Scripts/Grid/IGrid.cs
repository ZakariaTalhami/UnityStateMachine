using UnityEngine;

public interface IGrid
{
    void ShowDebuglines(Color color);
    IGrid GetFreeArea(int width, int height);
    IGrid GetRandomFreeArea(int width, int height);
    GridCell GetCellofWorldPosition(Vector3 worldPosition);
    Vector3 GetWorldPositionOfCell(int x, int y);
    Vector3 GetWorldPositionOfCell(Vector2Int cellPosition);
    Vector3 GetWorldPostion(int x, int y);
    SubGrid GetSubGrid(Vector2Int origin, int width, int height);
    bool IsCompleteltyFree();
    bool IsAreaFree(Vector2Int origin, int width, int height);
    void SetCellContent(int x, int y, GameObject gameObject);
    void SetGridContent(GameObject gameObject);
    GridCell GetCell(int x, int y);
    Vector3 GetOriginWorldPosition();
}