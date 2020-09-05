using UnityEngine;

public class Stockpile : MonoBehaviour, IProgressEntity
{
    // TODO: Convert this to Prop when using the Factory
    [SerializeField] private int _storageLimit = 200;
    public int storageLimit
    {
        get
        {
            return _storageLimit;
        }
    }

    public ResourceType type = default;

    private ResourceCollection _storage = new ResourceCollection();
    private int _totalStorageAmount => _storage.TotalResourceAmount();
    private IStockpile _stockpileController;

    public event GenericDelegates.FloatHandler onProgressUpdated;

    private void Start()
    {
        _stockpileController = new StockPileController(type, _storageLimit);
        _stockpileController.SetCapacityUpdateEvent(onProgressUpdated);
        ResourceEventHandler.StockpileConstructed(this);
    }

    public bool Store(ResourceCollection storeAmount, out ResourceCollection stored)
    {
        return _stockpileController.Store(storeAmount, out stored);
    }

    public bool Take(ResourceCollection takeAmount, out ResourceCollection taken)
    {
        return _stockpileController.Take(takeAmount, out taken);
    }

    public bool IsFull()
    {
        return _stockpileController.IsFull();
    }
    
}