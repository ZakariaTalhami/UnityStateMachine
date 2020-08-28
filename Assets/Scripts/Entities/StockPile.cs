using UnityEngine;

public class StockPile : MonoBehaviour
{
    [SerializeField] private int _storageLimit = 200;

    private int _storage = 0;

    public bool Store(int storeAmount, out int stored)
    {
        stored = 0;
        if (IsFull())
            return false;

        stored = (_storage + storeAmount <= _storageLimit) ? storeAmount : _storageLimit - _storage;
        _storage += stored;

        return true;
    }

    public bool IsFull()
    {
        return  _storageLimit <= _storage;
    }
}