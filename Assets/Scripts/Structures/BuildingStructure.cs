using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "State Machine/Structures/Building", order = 0)]
public class BuildingStructure : Structure
{
    public VillageAgeType ageType;
    public List<ResourceType> spawnVilagerTypes; 
}