using UnityEngine;
using UnityEngine.AI;

public abstract class Villager : MonoBehaviour, IVillager
{
    public abstract VillagerType villagertype { get; }
    protected StateMachine _stateMachine;
    protected NavMeshAgent _navMeshAgent;

    private void Start() {
        VillagerEventHandler.VillagerSpawned(this);
        Init();
        _navMeshAgent = GetNavMeshAgent();
        _stateMachine = SetupVillagerStateMachine();
    }

    protected abstract void Init();
    protected abstract StateMachine SetupVillagerStateMachine();
    protected abstract NavMeshAgent GetNavMeshAgent();
}