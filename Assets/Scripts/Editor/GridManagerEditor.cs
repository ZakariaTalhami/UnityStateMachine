using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{

    private void OnSceneGUI()
    {
        GridManager gridManager = (GridManager)target;
        Handles.Label(gridManager.walkableGridMeta.origin, "Walkable Area Origin");
        Handles.Label(gridManager.villageGridMeta.origin, "Village Area Origin");
        Handles.Label(gridManager.woodStockpileGridMeta.origin, "Wood Stockpile Area Origin");
        Handles.Label(gridManager.stoneStockpileGridMeta.origin, "Stone Stockpile Area Origin");
        EditorGUI.BeginChangeCheck();

        GUI.SetNextControlName("walkableAreaOrigin");
        Vector3Int newGridPosition = Vector3Int.FloorToInt(Handles.PositionHandle(gridManager.walkableGridMeta.origin, Quaternion.identity));

        GUI.SetNextControlName("villageAreaOrigin");
        Vector3Int newVillageGridPosition = Vector3Int.FloorToInt(Handles.PositionHandle(gridManager.villageGridMeta.origin, Quaternion.identity));

        GUI.SetNextControlName("woodStockpileAreaOrigin");
        Vector3Int newWoodStockpileGridPosition = Vector3Int.FloorToInt(Handles.PositionHandle(gridManager.woodStockpileGridMeta.origin, Quaternion.identity));

        GUI.SetNextControlName("stoneStockpileAreaOrigin");
        Vector3Int newStoneStockpileGridPosition = Vector3Int.FloorToInt(Handles.PositionHandle(gridManager.stoneStockpileGridMeta.origin, Quaternion.identity));

        if (EditorGUI.EndChangeCheck())
        {
            gridManager.walkableGridMeta.origin = newGridPosition;
            gridManager.villageGridMeta.origin = newVillageGridPosition;
            gridManager.woodStockpileGridMeta.origin = newWoodStockpileGridPosition;
            gridManager.stoneStockpileGridMeta.origin = newStoneStockpileGridPosition;
        }
    }
}