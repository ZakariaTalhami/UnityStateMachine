using UnityEngine;
using System;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{

    private ResourceCollection _resources;
    private Dictionary<ResourceType, List<Stockpile>> _stockpiles;
    private Dictionary<ResourceType, int> _resourceCapacity;

    private void Awake()
    {
        _resources = new ResourceCollection();
        _stockpiles = new Dictionary<ResourceType, List<Stockpile>>();
        _resourceCapacity = new Dictionary<ResourceType, int>();

        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            _stockpiles[type] = new List<Stockpile>();
            _resourceCapacity[type] = 0; 
        }

        SetupListeners();
    }

    private void SetupListeners()
    {
        ResourceEventHandler.onResourceStoredInStockpile += ResourceAddedToStockpile;
        ResourceEventHandler.onResourceTakenFormStockpile += ResourceTakenFromStockpile;
        ResourceEventHandler.onStockpileConstructed += AddStockpile;
    }

    public bool IsResourceAvailable(ResourceCollection requiredResources)
    {
        return requiredResources <= _resources;
    }

    public bool TakeResources(ResourceCollection requiredResources)
    {
        if (!IsResourceAvailable(requiredResources))
            return false;

        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            List<Stockpile> stockpiles = _stockpiles[type];
            int index = 0;
            while(requiredResources.GetResourceAmount(type) != 0 && index < stockpiles.Count)
            {
                stockpiles[index].Take(requiredResources, out ResourceCollection taken);
                requiredResources -= taken;
                index++;
            }
        }

        return true;
    }

    private void ResourceAddedToStockpile(ResourceCollection resource)
    {
        _resources += resource;
        Debug.Log("Resource Added:");
        Debug.Log(_resources.ToString());
    }

    private void ResourceTakenFromStockpile(ResourceCollection resource)
    {
        _resources -= resource;
        Debug.Log("Resource Taken:");
        Debug.Log(_resources.ToString());
    }

    private void AddStockpile(Stockpile stockpile)
    {
        Debug.Log("Stockpile added of type " + stockpile.type);
        _stockpiles[stockpile.type].Add(stockpile);
        _resourceCapacity[stockpile.type] += stockpile.storageLimit;
    }

    public float GetResourceFullPercentage(ResourceType type)
    {
        return (float) _resources.GetResourceAmount(type) / _resourceCapacity[type];
    }
}