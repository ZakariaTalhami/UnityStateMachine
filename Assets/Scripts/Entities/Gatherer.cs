using UnityEngine;
using UnityEngine.AI;
using System;

public class Gatherer : Villager
{
    public float gatherSpeed;
    public float storeSpeed;
    [SerializeField] private int _gatherAmount = default;
    [SerializeField] private int _storeAmount = default;
    [SerializeField] private int _maxResourceCarryLimit = default; 
    public ResourceType resourceTargetType;

    private ResourceCollection _resourceCollection;
    private int _totalCarryAmount => _resourceCollection.TotalResourceAmount();
    public Resource target { get; set; } 
    public Stockpile stockPile { get; set; }

    public override VillagerType villagertype => throw new NotImplementedException();

    protected override void Init()
    {
        _resourceCollection = new ResourceCollection();
    }

    protected override StateMachine SetupVillagerStateMachine()
    {
        StateMachine stateMachine = new StateMachine();

        IState searchForResource = new SearchForResouces(this);
        IState moveToResource = new MoveToResouce(this, _navMeshAgent);
        IState collectResource = new CollectResource(this);
        IState searchForStockPile = new SearchForStockpile(this);
        IState moveToStockpile = new MoveToStockPile(this, _navMeshAgent);
        IState placeInStockPile = new PlaceInStockPile(this);

        At(searchForResource, moveToResource, HasTarget());
        At(moveToResource, searchForResource, StuckForASecond());
        At(moveToResource, collectResource, ReachedResource());
        At(collectResource, searchForResource, ResourceConsumed());
        At(collectResource, searchForStockPile, HasReachedCarryLimit());
        At(searchForStockPile, moveToStockpile, HasStockpile());
        At(moveToStockpile, searchForStockPile, StuckForStockPile());
        At(moveToStockpile, placeInStockPile, ReachedStockPile());
        At(placeInStockPile, searchForResource, HasEmptiedCarryLoad());
        At(placeInStockPile, searchForStockPile, HasStockPileFilled());

        stateMachine.SetState(searchForResource);

        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        Func<bool> HasTarget() => () => target != null;
        Func<bool> ReachedResource() => () => target != null && Vector3.Distance(transform.position, target.transform.position) < 1.8f;
        Func<bool> ResourceConsumed() => () => _resourceCollection.TotalResourceAmount() < _maxResourceCarryLimit  && target != null && ((IResource)target).IsDepleted();
        Func<bool> StuckForASecond() => () => ((MoveToResouce) moveToResource).stuckTime > 2f;
        Func<bool> HasReachedCarryLimit() => () => _maxResourceCarryLimit <= _resourceCollection.TotalResourceAmount();
        Func<bool> HasStockpile() => () => stockPile != null;
        Func<bool> HasEmptiedCarryLoad() => () => _resourceCollection.TotalResourceAmount() == 0;
        Func<bool> ReachedStockPile() => () => stockPile != null && Vector3.Distance(transform.position, stockPile.transform.position) < 1.8f;
        Func<bool> HasStockPileFilled() => () => stockPile.IsFull();
        Func<bool> StuckForStockPile() => () => ((MoveToStockPile) moveToStockpile).stuckTime > 2f;

        return stateMachine;
    }

    protected override NavMeshAgent GetNavMeshAgent()
    {
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = false;
        return navMeshAgent;
    }

    private void OnDrawGizmos()
    {
        if(_navMeshAgent != null)
            Gizmos.DrawCube(_navMeshAgent.destination, Vector3.one * 0.5f);
    }

    private void Update() => _stateMachine.Tick();

    public void TakeFromTarget()
    {
        int possibleGatherAmount = (_totalCarryAmount + _gatherAmount < _maxResourceCarryLimit) ? _gatherAmount :  _maxResourceCarryLimit - _totalCarryAmount;
        if(target.Take(possibleGatherAmount, out int taken))
        {
            _resourceCollection.AddToResource(resourceTargetType, taken) ;
        } 
    }

    public void StoreInStockpile()
    {
        ResourceCollection storeAmount = new ResourceCollection();
        int possibleStoreAmount = _resourceCollection.GetResourceAmount(resourceTargetType) >= _storeAmount ?
                                _storeAmount :
                                _resourceCollection.GetResourceAmount(resourceTargetType);
        storeAmount.AddToResource(resourceTargetType, possibleStoreAmount);
        if(stockPile.Store(storeAmount, out ResourceCollection stored))
        {
            _resourceCollection -= stored;
        } 
    }

}