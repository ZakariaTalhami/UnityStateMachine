using UnityEngine;
using System.Collections.Generic;

public class VillagerManager
{
    private List<IVillager> _villagers;
    private IVillagerSpawner _villagerSpawner;

    public int villagerCount => _villagers.Count;


    public VillagerManager(IVillagerSpawner villagerSpawner)
    {
        _villagers = new List<IVillager>();
        _villagerSpawner = villagerSpawner;
        SetupListeners();
    }

    private void SetupListeners()
    {
        VillagerEventHandler.onVillagerSpawned += VillagerSpawned;
        VillagerEventHandler.onBuildingBuilt += SpawnVillagersFromBuilding;
        VillagerEventHandler.onVillageAgeUpdated += SpawnVillagerFromAge;
    }

    private void SpawnVillagerFromAge(VillageAgeType age)
    {
        ResourceType gathererType = GetVillagerByAge(age);
        SpawnGatherVillagers(gathererType, 1, Vector3.zero);
    }

    private void SpawnVillagersFromBuilding(BuildingMetadata buildingMetadata)
    {
        if(buildingMetadata.structure.GetType() == typeof(BuildingStructure))
        {
            BuildingStructure buildingStructure = (BuildingStructure) buildingMetadata.structure;
            foreach (ResourceType type in buildingStructure.spawnVilagerTypes)
            {
                Building building = buildingMetadata.gameObject.GetComponent<Building>();
                if(building != null)
                    SpawnGatherVillagers(type, 1, building.getSpawnPosition());
            }
        }
    }

    private void VillagerSpawned(IVillager villager)
    {
        _villagers.Add(villager);
        VillagerEventHandler.VillagerCountUpdated(_villagers.Count);
        Debug.Log("Villager Spawned, count updated to : " + _villagers.Count);
    }

    private void SpawnGatherVillagers(ResourceType resourceType, int numberOfVillagers, Vector3 spawnPosition)
    {
        for (int i = 0; i < numberOfVillagers; i++)
        {
            _villagerSpawner.SpawnGathererVillager(resourceType, spawnPosition);
        }
    }

    private void SpawnBuilderVillager(int numberOfVillagers, Vector3 spawnPosition)
    {
        _villagerSpawner.SpawnBuilderVillager(spawnPosition);
    }

    private ResourceType GetVillagerByAge(VillageAgeType age)
    {
        switch (age)
        {
            case VillageAgeType.Village:
                return ResourceType.Stone;
            case VillageAgeType.Town:
                return ResourceType.Gold;
            case VillageAgeType.GoldenCity:
                return ResourceType.Gold;
            default:
                return ResourceType.Wood;
        }
    }

}