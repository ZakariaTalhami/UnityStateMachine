using UnityEngine;

public class BuildingRequirements : StructureRequirement
{
    public BuildingRequirements(Structure structure, GridManager gridManager, ResourceManager resourceManager) : base(structure, gridManager, resourceManager)
    {
    }

    public override int priority => 2;

    public override IGrid FindBuildLoaction()
    {
        structure.GetWidthAndHeight(out int  width, out int height);
        // return gridManager.GetFreeVillagePosition(width, height);
        return gridManager.GetRandomFreeVillagePosition(width, height);
    }

    public override void GetFactory()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsApplicable()
    {
        return resourceManager.IsResourceAvailable(structure.GetResourceRequirements());
    }

    public override void PostInstantiationProcessing(BuildingMetadata structureMetadata)
    {
        Building building = structureMetadata.gameObject.GetComponent<Building>();
        building.metadata = structureMetadata;
    }
}