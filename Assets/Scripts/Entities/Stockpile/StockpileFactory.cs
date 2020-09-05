using UnityEngine;
using System.Collections.Generic;

public abstract class StockpileFactory : IStockpileFactory
{

    protected Dictionary<ResourceType, StockpileStructure> _typePrfabs;

    public StockpileFactory()
    {
        _typePrfabs = new Dictionary<ResourceType, StockpileStructure>();
        LoadPrefabs();
    }

    protected abstract void LoadPrefabs();

    public GameObject InstantiateStockpile(ResourceType type, Vector3 position)
    {
        GameObject stockpilePrefab = _typePrfabs[type].Prefab;

        if (stockpilePrefab == null) return null;

        return GameObject.Instantiate(stockpilePrefab, position, Quaternion.identity);
    }

}