using UnityEngine;

public abstract class StructureRequirement
{

    protected StructureRequirement(Structure structure, GridManager gridManager, ResourceManager resourceManager)
    {
        this.structure = structure;
        this.gridManager = gridManager;
        this.resourceManager = resourceManager;
    }

    protected Structure structure { get; private set; }
    protected GridManager gridManager { get; private set; }
    protected ResourceManager resourceManager { get; private set; }

    public abstract int priority { get; }

    public abstract bool IsApplicable();

    // How to deal with this???
    public abstract void GetFactory();

    public abstract IGrid FindBuildLoaction();

    public abstract void PostInstantiationProcessing(BuildingMetadata structure);

    public GameObject Build(IGrid structureGrid)
    {
        GameObject buildingPrefab = structure.Prefab;
        Vector3 position = structureGrid.GetOriginWorldPosition();
        resourceManager.TakeResources(structure.GetResourceRequirements());
        GameObject structureGO = GameObject.Instantiate(buildingPrefab, position, Quaternion.identity);
        structureGrid.SetGridContent(structureGO);
        BuildingMetadata buildingMetadata = new BuildingMetadata(structureGO, structure, structureGrid, 0, 0);
        PostInstantiationProcessing(buildingMetadata);
        return structureGO;
    }
}