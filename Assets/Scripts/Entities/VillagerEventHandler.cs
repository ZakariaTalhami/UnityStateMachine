using System;

public static class VillagerEventHandler
{
    public delegate void VillagerHandler(IVillager villager);

    public static event VillagerHandler onVillagerSpawned;
    public static event GenericDelegates.IntegerHandler onVillagerCountUpdated;

    public static void VillagerSpawned(IVillager villager)
    {
        onVillagerSpawned?.Invoke(villager);
    }

    public static void VillagerCountUpdated(int numberOfVillages)
    {
        onVillagerCountUpdated?.Invoke(numberOfVillages);
    }
}