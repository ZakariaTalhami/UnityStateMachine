using UnityEngine;
using UnityEngine.AI;

public class MoveToResouce : IState
{
    private readonly Gatherer _gatherer;
    private readonly NavMeshAgent _navMeshAgent;
    private Vector3 _lastPosition;

    public float stuckTime = 0;


    public MoveToResouce(Gatherer gatherer, NavMeshAgent navMeshAgent)
    {
        _gatherer = gatherer;
        _navMeshAgent = navMeshAgent;
    }

    public void Tick()
    {
        if (Vector3.Distance(_gatherer.transform.position, _lastPosition) <= 0f)
            stuckTime += Time.deltaTime;

        _lastPosition = _gatherer.transform.position;
        Debug.DrawLine(_gatherer.transform.position, _gatherer.target.transform.position);
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_gatherer.target.transform.position);
        _navMeshAgent.stoppingDistance = 1f;
        stuckTime = 0f;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }
}