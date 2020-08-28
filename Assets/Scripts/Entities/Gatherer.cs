using UnityEngine;
using UnityEngine.AI;
using System;

public class Gatherer : MonoBehaviour 
{
    public float gatherSpeed;
    public float storeSpeed;
    [SerializeField] private int _gatherAmount;
    [SerializeField] private int _storeAmount;
    [SerializeField] private int _maxResourceCarryLimit; 
    public Type resourceTargetType {get; set;} = typeof(Resource);

    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;
    private int _carrySize;
    public Resource target { get; set; } 
    public StockPile stockPile { get; set; } 

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.enabled = false;

        _stateMachine = new StateMachine();

        // Type t = Type.GetType("WoodResource");
        Type t = Type.GetType("StoneResource");
        
        resourceTargetType = t;

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
        At(placeInStockPile, searchForStockPile, HasStockPileFilled());
        At(placeInStockPile, searchForResource, HasEmptiedCarryLoad());

        _stateMachine.SetState(searchForResource);

        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
        Func<bool> HasTarget() => () => target != null;
        Func<bool> ReachedResource() => () => target != null && Vector3.Distance(transform.position, target.transform.position) < 1.8f;
        Func<bool> ResourceConsumed() => () => _carrySize < _maxResourceCarryLimit  && target != null && ((IResource)target).IsDepleted();
        Func<bool> StuckForASecond() => () => ((MoveToResouce) moveToResource).stuckTime > 2f;
        Func<bool> HasReachedCarryLimit() => () => _maxResourceCarryLimit <= _carrySize;
        Func<bool> HasStockpile() => () => stockPile != null;
        Func<bool> HasEmptiedCarryLoad() => () => _carrySize == 0;
        Func<bool> ReachedStockPile() => () => stockPile != null && Vector3.Distance(transform.position, stockPile.transform.position) < 1.8f;
        Func<bool> HasStockPileFilled() => () => stockPile.IsFull();
        Func<bool> StuckForStockPile() => () => ((MoveToStockPile) moveToStockpile).stuckTime > 2f;
    }

    private void Update() => _stateMachine.Tick();

    public void TakeFromTarget()
    {
        int possibleGatherAmount = (_carrySize + _gatherAmount < _maxResourceCarryLimit) ? _gatherAmount :  _maxResourceCarryLimit - _carrySize;
        if(target.Take(possibleGatherAmount, out int taken))
        {
            _carrySize += taken;
        } 
    }

    public void StoreInStockpile()
    {
        if(stockPile.Store(_storeAmount, out int stored))
        {
            _carrySize -= stored;
        } 
    }

    private void OnDrawGizmos()
    {
        if(_navMeshAgent != null)
            Gizmos.DrawCube(_navMeshAgent.destination, Vector3.one * 0.5f);
    }
}