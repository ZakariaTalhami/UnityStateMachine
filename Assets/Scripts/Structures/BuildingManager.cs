using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
public class BuildingManager : MonoBehaviour
{
    private const float RESOURCE_CAPACITY_THRESHOLD = 0.8f;
    private List<BuildingMetadata> _builds;
    private ResourceManager _resourceManager;
    private GridManager _gridManager;

    private List<Structure> _strockpileObjects;
    private List<Structure> _buidlingObjects;

    private void Start()
    {
        _builds = new List<BuildingMetadata>();
        _resourceManager = FindObjectOfType<ResourceManager>();
        _gridManager = FindObjectOfType<GridManager>();
        _strockpileObjects = GetStockpileResources();
        _buidlingObjects = GetBuldingResources();
    }

    public StructureRequirement GetNextRequirement()
    {
        List<StructureRequirement> requirements = new List<StructureRequirement>();

        requirements.Add(new BuildingRequirements(GetBuildingObject(AgeTracker.Instance.currentAge), _gridManager, _resourceManager));

        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            if(IsResourceStockpileRequired(type))
            {
                requirements.Add(new StockpileRequirement(GetStockpileObject(type), _gridManager, _resourceManager));
            }
        }

        return requirements.OrderBy(req => req.priority).First();
    }

    private bool IsResourceStockpileRequired(ResourceType type)
    {
        return _resourceManager.GetResourceFullPercentage(type) > RESOURCE_CAPACITY_THRESHOLD;
    }

    private List<Structure> GetStockpileResources()
    {
        StockpileStructure[] stockpiles = Resources.LoadAll<StockpileStructure>("Structrures/");
        return stockpiles.ToList<Structure>();
    } 

    
    private List<Structure> GetBuldingResources()
    {
        BuildingStructure[] stockpiles = Resources.LoadAll<BuildingStructure>("Structrures/");
        return stockpiles.ToList<Structure>();
    }

    private Structure GetStockpileObject(ResourceType type)
    {
        return _strockpileObjects.Find(structure => ((StockpileStructure)structure).resourceType == type);
    }

    private Structure GetBuildingObject(VillageAgeType age)
    {
        return _buidlingObjects.Find(structure => ((BuildingStructure)structure).ageType == age);
    }
}