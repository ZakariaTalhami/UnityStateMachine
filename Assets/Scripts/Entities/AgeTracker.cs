using System;
using UnityEngine;

public class AgeTracker : MonoSingleton<AgeTracker>
{
    public VillageAgeType currentAge { get; private set; }

    private void Start()
    {
        currentAge = VillageAgeType.Settlement;
        VillagerEventHandler.onVillagerCountUpdated += SetVillageAge;
    }

    private void SetVillageAge(int villagerCount)
    {
        VillageAgeType newAge = getVillageAgeByVillagerCount(villagerCount);
        
        if (newAge != currentAge)
        {
            currentAge = newAge;
            VillagerEventHandler.VillageAgeUpdated(currentAge);
            Debug.LogWarning("Village age set to : " + currentAge);
        }
    }

    private VillageAgeType getVillageAgeByVillagerCount(int villagerCount)
    {
        if (IsInRange(villagerCount, 1, 5))
            return VillageAgeType.Settlement;
        else if (IsInRange(villagerCount, 5, 14))
            return VillageAgeType.Village;
        else if (IsInRange(villagerCount, 15, 40))
            return VillageAgeType.Town;
        else if (40 < villagerCount)
            return VillageAgeType.GoldenCity;
        return VillageAgeType.Settlement;
    }

    private bool IsInRange(int value, int lowerBound, int upperBound)
    {
        return lowerBound <= value && value <= upperBound;
    }

}