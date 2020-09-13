using UnityEngine;

public class StockpileRequirement : StructureRequirement
{
    public StockpileRequirement(Structure structure, GridManager gridManager, ResourceManager resourceManager) : base(structure, gridManager, resourceManager)
    {
    }

    public override int priority => 1;

    public override IGrid FindBuildLoaction()
    {
        structure.GetWidthAndHeight(out int  width, out int height);
        ResourceType requiredType = ((StockpileStructure) structure).resourceType;
        IGrid freeLocation;
        if( requiredType == ResourceType.Stone || requiredType == ResourceType.Gold)
            freeLocation = gridManager.GetFreeStoneStockpilePosition(width, height);
        else 
            freeLocation = gridManager.GetFreeWoodStockpilePosition(width, height);
        return freeLocation;
    }

    public override void GetFactory()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsApplicable()
    {
        return resourceManager.IsResourceAvailable(structure.GetResourceRequirements());
    }

    public override void PostInstantiationProcessing(BuildingMetadata structure)
    {
        
    }
}