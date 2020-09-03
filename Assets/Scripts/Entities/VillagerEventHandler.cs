using System;

public static class VillagerEventHandler {
    public delegate void VillagerHandler(IVillager villager);
    public delegate void IntegerHandler(int amount);

    public static event VillagerHandler onVillagerSpawned;
    public static event IntegerHandler onVillagerCountUpdated;

    public static void VillagerSpawned(IVillager villager)
    {
        onVillagerSpawned?.Invoke(villager);
    }

    public static void VillagerCountUpdated(int numberOfVillages)
    {
        onVillagerCountUpdated?.Invoke(numberOfVillages);
    }
}