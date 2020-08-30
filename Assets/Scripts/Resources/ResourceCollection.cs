using System;
using System.Linq;
using System.Collections.Generic;

public class ResourceCollection
{

    private Dictionary<ResourceType, int> _collection;

    public ResourceCollection()
    {
        _collection = new Dictionary<ResourceType, int>();
        SetupCollectionResourceTypes();
    }

    public ResourceCollection(Dictionary<ResourceType, int> collection)
    {
        this._collection = collection;
        SetupCollectionResourceTypes();
    }

    private void SetupCollectionResourceTypes()
    {
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            int initailValue = _collection.TryGetValue(type, out int value) ? value : 0;
            _collection[type] = initailValue;
        }
    }

    public int GetResourceAmount(ResourceType type)
    {
        return _collection[type];
    }

    public void AddToResource(ResourceType type, int addedAmount)
    {
        _collection[type] += addedAmount;
    }

    public bool TakeResource(ResourceType type, int TakenAmount)
    {
        bool taken = false;
        if(TakenAmount <= _collection[type] )
        {
            taken = true;
            _collection[type] -= TakenAmount;
        }
        return taken;
    }

    public int TotalResourceAmount()
    {
        return _collection.Sum(resource => resource.Value);
    }

    public static ResourceCollection operator +(ResourceCollection rc1, ResourceCollection rc2)
    {
        Dictionary<ResourceType, int> newCollection = new Dictionary<ResourceType, int>();
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            newCollection.Add(type, rc1.GetResourceAmount(type) + rc2.GetResourceAmount(type));
        }

        return new ResourceCollection(newCollection);
    }

    public static ResourceCollection operator -(ResourceCollection rc1, ResourceCollection rc2)
    {
        Dictionary<ResourceType, int> newCollection = new Dictionary<ResourceType, int>();
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            newCollection.Add(type, rc1.GetResourceAmount(type) - rc2.GetResourceAmount(type));
        }

        return new ResourceCollection(newCollection);
    }

}