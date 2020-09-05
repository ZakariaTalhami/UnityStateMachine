public interface IStockpile {
    void SetCapacityUpdateEvent(GenericDelegates.FloatHandler capacityEvent);
    bool Store(ResourceCollection storeAmount, out ResourceCollection stored);
    bool Take(ResourceCollection takeAmount, out ResourceCollection taken);
    int GetResourceAmount(ResourceType type);
    bool IsFull();
}