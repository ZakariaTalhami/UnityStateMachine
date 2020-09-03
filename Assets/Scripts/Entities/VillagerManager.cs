using UnityEngine;
using System.Collections.Generic;

public class VillagerManager
{
    private List<IVillager> _villagers;
    private IVillagerSpawner _villagerSpawner;

    public int villagerCount => _villagers.Count;


    public VillagerManager(IVillagerSpawner villagerSpawner) {
        _villagers = new List<IVillager>();
        _villagerSpawner = villagerSpawner;
        SetupListeners();
    }

    private void SetupListeners()
    {
        VillagerEventHandler.onVillagerSpawned += VillagerSpawned;
    }

    private void VillagerSpawned(IVillager villager)
    {
        _villagers.Add(villager);
        VillagerEventHandler.VillagerCountUpdated(_villagers.Count);
        Debug.Log("Villager Spawned, count updated to : " + _villagers.Count);
    }

    private void SpawGatherVillagers(ResourceType resourceType, int numberOfVillagers, Vector3 spawnPosition)
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

}