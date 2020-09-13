using System;

public static class VillagerEventHandler
{
    public delegate void VillagerHandler(IVillager villager);
    public delegate void VillageAgeHandler(VillageAgeType age);
    public delegate void BuildingStructureHandler(BuildingMetadata buildingStructure);

    public static event VillagerHandler onVillagerSpawned;
    public static event GenericDelegates.IntegerHandler onVillagerCountUpdated;
    public static event VillageAgeHandler onVillageAgeUpdated;
    public static event BuildingStructureHandler onBuildingBuilt;

    public static void VillagerSpawned(IVillager villager)
    {
        onVillagerSpawned?.Invoke(villager);
    }

    public static void VillagerCountUpdated(int numberOfVillages)
    {
        onVillagerCountUpdated?.Invoke(numberOfVillages);
    }

    public static void VillageAgeUpdated(VillageAgeType age)
    {
        onVillageAgeUpdated?.Invoke(age);
    }

    public static void BuildingBuilt(BuildingMetadata buildingStructure)
    {
        onBuildingBuilt?.Invoke(buildingStructure);
    }
}