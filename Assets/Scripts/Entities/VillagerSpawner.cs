using UnityEngine;

public class VillagerSpawner : IVillagerSpawner
{
    private Gatherer _gathererVillagerPrefab;
    private IVillager _builderVillagerPrefab;

    public VillagerSpawner()
    {
        _gathererVillagerPrefab = Resources.Load<Gatherer>("Prefabs/NPC/GathererVillager");
    }

    public IVillager SpawnBuilderVillager(Vector3 spawnPosition)
    {
        throw new System.NotImplementedException();
    }

    public IVillager SpawnGathererVillager(ResourceType gatherResource, Vector3 spawnPosition)
    {
        Gatherer gatherer = GameObject.Instantiate(_gathererVillagerPrefab, spawnPosition, Quaternion.identity);
        gatherer.resourceTargetType = gatherResource;
        return gatherer;
    }
}