using System;
using UnityEngine;
using System.Linq;

public class SearchForResouces : IState
{
    private readonly Gatherer _gatherer;
    private readonly Type  _targetType;
    private readonly float _timeoutDuration;
    public float searchDuration;
    
    public SearchForResouces(Gatherer gatherer)
    {
        _gatherer = gatherer;
    }

    public void Tick()
    {
        _gatherer.target = SelectOneOfClosestResources(5);
        searchDuration += Time.deltaTime;
    }

    public Resource SelectOneOfClosestResources(int randomPoolSize)
    {
        Resource r =  UnityEngine.Object.FindObjectsOfType<Resource>()
            .OrderBy(t => Vector3.Distance(_gatherer.transform.position, t.transform.position))
            .Where(resource => _gatherer.resourceTargetType == resource.type)
            .Where(resource => ((IResource)resource).IsDepleted() == false)
            .Take(randomPoolSize)
            .OrderBy(t => UnityEngine.Random.Range(0, int.MaxValue))
            .FirstOrDefault();
        return r;
    }

    public void OnEnter() { searchDuration = 0;}

    public void OnExit() {}
}