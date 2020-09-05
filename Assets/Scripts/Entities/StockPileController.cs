
public class StockPileController : IStockpile
{
    private int _storageLimit;
    private ResourceType _type;
    private ResourceCollection _storage;
    private int _totalStorageAmount => _storage.TotalResourceAmount();
    private event GenericDelegates.FloatHandler _onCapacityUpdate;

    public StockPileController(ResourceType type, int storageLimit)
    {
        this._type = type;
        _storageLimit = storageLimit;
        _storage = new ResourceCollection(); ;
    }

    public void SetCapacityUpdateEvent(GenericDelegates.FloatHandler capacityEvent)
    {
        _onCapacityUpdate += capacityEvent;
    }

    public bool Store(ResourceCollection storeAmount, out ResourceCollection stored)
    {
        ResourceCollection storeAmountForType = storeAmount.GetSingleResourceCollection(_type);
        stored = ResourceCollection.EMPTY_RESOURCE_COLLECTION;
        if (IsFull())
            return false;

        if (_totalStorageAmount + storeAmountForType.TotalResourceAmount() <= _storageLimit)
            stored = storeAmountForType;
        else
        {
            int exceededAmount = storeAmountForType.TotalResourceAmount() + _totalStorageAmount - _storageLimit;
            storeAmountForType.TakeResource(_type, exceededAmount);
            stored = storeAmountForType;
        }
        _storage += stored;

        _onCapacityUpdate?.Invoke((float)_totalStorageAmount / _storageLimit);
        ResourceEventHandler.ResourceStoredInStockpile(stored);

        return true;
    }

    public bool Take(ResourceCollection takeAmount, out ResourceCollection taken)
    {
        ResourceCollection takeAmountForType = takeAmount.GetSingleResourceCollection(_type);
        taken = ResourceCollection.EMPTY_RESOURCE_COLLECTION;

        if (_totalStorageAmount == 0)
            return false;

        if (_totalStorageAmount >= takeAmountForType.TotalResourceAmount())
            taken = takeAmountForType;
        else
        {
            int exceededAmount = takeAmountForType.TotalResourceAmount() - _totalStorageAmount;
            takeAmountForType.TakeResource(_type, exceededAmount);
            taken = takeAmountForType;
        }
        _storage += taken;

        _onCapacityUpdate?.Invoke((float)_totalStorageAmount / _storageLimit);
        ResourceEventHandler.ResourceTakenFormStockpile(taken);

        return true;
    } 

    public int GetResourceAmount(ResourceType type)
    {
        return _storage.GetResourceAmount(type);
    }

    public bool IsFull()
    {
        return _storageLimit <= _totalStorageAmount;
    }

}