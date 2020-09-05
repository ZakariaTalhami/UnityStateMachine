using UnityEngine;

[CreateAssetMenu(fileName = "Stockpile", menuName = "State Machine/Structures/Stockpile", order = 0)]
public class StockpileStructure : Structure
{
    public ResourceType resourceType;
    public int maxStorageCapacity;
}