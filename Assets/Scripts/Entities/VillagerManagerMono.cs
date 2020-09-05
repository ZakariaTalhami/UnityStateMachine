using UnityEngine;

public class VillagerManagerMono : MonoBehaviour {
    
    private VillagerManager _villagerManager;

    private void Start() {
        IVillagerSpawner spawner = new VillagerSpawner();
        _villagerManager = new VillagerManager(spawner);    
    }
}