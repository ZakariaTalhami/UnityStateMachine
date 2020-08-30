using System;
using System.Linq;
using System.Collections.Generic;

public class ResourceCollection
{
    public readonly static ResourceCollection EMPTY_RESOURCE_COLLECTION = new ResourceCollection();
    
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

    public ResourceCollection GetSingleResourceCollection(ResourceType type)
    {
        ResourceCollection resource = new ResourceCollection();
        resource.AddToResource(type, _collection[type]);
        return resource;
    }

    public int TotalResourceAmount() => _collection.Sum(resource => resource.Value);

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

    public static bool operator <(ResourceCollection rc1, ResourceCollection rc2)
    {
        bool isLessThan = true;
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            if((rc1.GetResourceAmount(type) < rc2.GetResourceAmount(type)) == false)
            {
                isLessThan = false;
                break;
            }
        }

        return isLessThan; 
    }
    
    public static bool operator >(ResourceCollection rc1, ResourceCollection rc2)
    {
        return rc2 < rc1;
    }

    public static bool operator <=(ResourceCollection rc1, ResourceCollection rc2)
    {
        bool isLessThanOrEqual = true;
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            if((rc1.GetResourceAmount(type) <= rc2.GetResourceAmount(type)) == false)
            {
                isLessThanOrEqual = false;
                break;
            }
        }

        return isLessThanOrEqual; 
    }

    public static bool operator >=(ResourceCollection rc1, ResourceCollection rc2)
    {
        bool isGreaterThanOrEqual = true;
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            if((rc1.GetResourceAmount(type) >= rc2.GetResourceAmount(type)) == false)
            {
                isGreaterThanOrEqual = false;
                break;
            }
        }

        return isGreaterThanOrEqual;
    }
}