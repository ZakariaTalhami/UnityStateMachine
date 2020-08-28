using UnityEngine;
using UnityEngine.AI;

public class MoveToStockPile : IState
{
    private readonly Gatherer _gatherer;
    private readonly NavMeshAgent _navMeshAgent;
    private Vector3 _lastPosition = Vector3.zero;

    public float stuckTime { get; private set; }


    public MoveToStockPile(Gatherer gatherer, NavMeshAgent navMeshAgent)
    {
        _gatherer = gatherer;
        _navMeshAgent = navMeshAgent;
    }

    public void Tick()
    {
        if (Vector3.Distance(_gatherer.transform.position, _lastPosition) <= 0f)
            stuckTime += Time.deltaTime;

        _lastPosition = _gatherer.transform.position;
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_gatherer.stockPile.transform.position);
        _lastPosition = _gatherer.transform.position;
        stuckTime = 0;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }
}