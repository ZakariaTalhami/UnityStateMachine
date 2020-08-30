
public class StockPileController : IStockpile
{
    private int _storageLimit;
    private ResourceType _type;
    private ResourceCollection _storage;
    private int _totalStorageAmount => _storage.TotalResourceAmount();

    public StockPileController(ResourceType type, int storageLimit)
    {
        this._type = type;
        _storageLimit = storageLimit;
        _storage = new ResourceCollection(); ;
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