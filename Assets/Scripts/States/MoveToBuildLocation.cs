using UnityEngine;
using UnityEngine.AI;

public class MoveToBuildLocation : IState
{
    private readonly Builder _builder;
    private readonly NavMeshAgent _navMeshAgent;
    private Vector3 _lastPosition;

    public float stuckTime = 0;


    public MoveToBuildLocation(Builder builder, NavMeshAgent navMeshAgent)
    {
        _builder = builder;
        _navMeshAgent = navMeshAgent;
    }

    public void Tick()
    {
        if (Vector3.Distance(_builder.transform.position, _lastPosition) <= 0f)
            stuckTime += Time.deltaTime;

        _lastPosition = _builder.transform.position;
        Debug.DrawLine(_builder.transform.position, _builder.buildLocation.GetOriginWorldPosition());
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_builder.buildLocation.GetOriginWorldPosition());
        _navMeshAgent.stoppingDistance = 1f;
        stuckTime = 0f;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }
}