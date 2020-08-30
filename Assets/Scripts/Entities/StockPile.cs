using UnityEngine;

public class Stockpile : MonoBehaviour
{
    [SerializeField] private int _storageLimit = 200;
    public ResourceType type = default;

    private ResourceCollection _storage = new ResourceCollection();
    private int _totalStorageAmount => _storage.TotalResourceAmount();
    private IStockpile _stockpileController;

    private void Start()
    {
        _stockpileController = new StockPileController(type, _storageLimit);
    }

    public bool Store(ResourceCollection storeAmount, out ResourceCollection stored)
    {
        return _stockpileController.Store(storeAmount, out stored);
    }

    public bool IsFull()
    {
        return _stockpileController.IsFull();
    }
}