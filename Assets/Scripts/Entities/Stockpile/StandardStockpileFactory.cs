using UnityEngine;

public class StandardStockpileFactory : StockpileFactory
{
    protected override void LoadPrefabs()
    {
        StockpileStructure[] temp = Resources.LoadAll<StockpileStructure>("Structrures/");
        foreach (StockpileStructure stockpileType in temp)
            _typePrfabs[stockpileType.resourceType] = stockpileType;
    }
}