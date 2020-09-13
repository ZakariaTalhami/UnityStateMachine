using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Building))]
public class BuildingEditor : Editor {
    private void OnSceneGUI() {
        Building building = (Building)target;
        Handles.Label(building.spawnPostion + building.transform.position, "Villager Spawn Location");

        EditorGUI.BeginChangeCheck();

        GUI.SetNextControlName("Villager Spawn Location");
        Vector3 newSpawnPosition = Handles.PositionHandle(building.spawnPostion + building.transform.position, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            building.spawnPostion = newSpawnPosition - building.transform.position;
        }
    }
}