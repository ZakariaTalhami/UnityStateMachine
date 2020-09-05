using UnityEngine;

public class VillagerManagerMono : MonoBehaviour {
    
    private VillagerManager _villagerManager;

    public GameObject temp;

    private void Start() {
        IVillagerSpawner spawner = new VillagerSpawner();
        _villagerManager = new VillagerManager(spawner);    
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(temp, new Vector3(10, 0, 2), Quaternion.identity);
        }
    }
}