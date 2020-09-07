using UnityEngine;
using UnityEngine.AI;
using System;

public class Builder : Villager
{
    public float buildSpeed;
    [SerializeField] private int _buildAmount = default;

    private BuildingManager _buildingManager;
    public StructureRequirement structureRequirement { get; set; }
    public IGrid buildLocation { get; set; }
    public Building buildingTarget { get; set; }

    public override VillagerType villagertype => VillagerType.Builder;

    protected override NavMeshAgent GetNavMeshAgent()
    {
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = false;
        return navMeshAgent;
    }

    protected override void Init()
    {
        _buildingManager = FindObjectOfType<BuildingManager>();
    }

    protected override StateMachine SetupVillagerStateMachine()
    {
        StateMachine stateMachine = new StateMachine();

        IState findRequiredBuilding = new FindRequiredBuilding(this, _buildingManager);
        IState waitForResources = new WaitForResources(this, 5f);
        IState findBuildLocation = new FindBuildLocation(this);
        IState moveToBuildLocation = new MoveToBuildLocation(this, _navMeshAgent);
        IState build = new Build(this);

        At(findRequiredBuilding, waitForResources, HasNewRequirement());
        At(waitForResources, findRequiredBuilding, HasWaitForResourcesExpired());
        At(waitForResources, findBuildLocation, IsRequirementApplicable());
        At(findBuildLocation, moveToBuildLocation, BuildLocationFound());
        At(moveToBuildLocation, findBuildLocation, StuckForASecond());
        At(moveToBuildLocation, build, ReachedBuildLocation());
        At(build, findRequiredBuilding, BuildingComplete());

        stateMachine.SetState(findRequiredBuilding);

        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        Func<bool> HasNewRequirement() => () => structureRequirement != null;
        Func<bool> HasWaitForResourcesExpired() => () => ((WaitForResources)waitForResources).hasTimedOut;
        Func<bool> IsRequirementApplicable() => () => structureRequirement.IsApplicable();
        Func<bool> BuildLocationFound() => () => buildLocation != null;
        Func<bool> StuckForASecond() => () => ((MoveToBuildLocation)moveToBuildLocation).stuckTime > 2f;
        Func<bool> ReachedBuildLocation() => () => buildLocation != null && Vector3.Distance(transform.position, buildLocation.GetOriginWorldPosition()) < 1.8f;
        Func<bool> BuildingComplete() => () => (buildingTarget != null && buildingTarget.IsComplete());

        return stateMachine;
    }

    public void ApplyWorkToBuilding()
    {
        buildingTarget.AddWork(_buildAmount);
    }

    public void ResetStateVariables()
    {
        structureRequirement = null;
        buildingTarget = null;
        buildLocation = null;
    }
}