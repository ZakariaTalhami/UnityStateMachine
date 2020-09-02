public static class ResourceEventHandler {
    public delegate void ResourceCollectionHandler(ResourceCollection resource);
    public delegate void StockpileHandler(Stockpile stockpile);

    public static event ResourceCollectionHandler onResourceStoredInStockpile;
    public static event ResourceCollectionHandler onResourceTakenFormStockpile;
    public static event StockpileHandler onStockpileConstructed;

    public static void ResourceStoredInStockpile(ResourceCollection resource)
    {
        onResourceStoredInStockpile?.Invoke(resource);
    }

    public static void ResourceTakenFormStockpile(ResourceCollection resource)
    {
        onResourceTakenFormStockpile?.Invoke(resource);
    }

    public static void StockpileConstructed(Stockpile stockpile)
    {
        onStockpileConstructed?.Invoke(stockpile);
    }
}