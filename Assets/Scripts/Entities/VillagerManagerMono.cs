using UnityEngine;

public class VillagerManagerMono : MonoBehaviour {
    
    private VillagerManager _villagerManager;

    private void Start() {
        IVillagerSpawner spawner = new VillagerSpawner();
        _villagerManager = new VillagerManager(spawner);    
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.timeScale != 0)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            if(Time.timeScale != 5)
                Time.timeScale = 5;
            else
                Time.timeScale = 1;
        }
    }
}