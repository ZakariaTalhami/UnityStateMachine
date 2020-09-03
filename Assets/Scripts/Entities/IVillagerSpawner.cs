using UnityEngine;

public interface IVillagerSpawner
{
    IVillager SpawnGathererVillager(ResourceType gatherResource, Vector3 spawnPostion);
    IVillager SpawnBuilderVillager(Vector3 spawnPosition);
}