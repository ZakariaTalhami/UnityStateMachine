using UnityEngine;
public interface IStockpileFactory
{
    GameObject InstantiateStockpile(ResourceType type, Vector3 position); 
} 