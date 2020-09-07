using UnityEngine;

public class Build : IState
{
    private Builder _builder;
    private float _timeForNextBuild;

    public Build(Builder builder)
    {
        _builder = builder;
    }

    public void OnEnter()
    {
        GameObject building = _builder.structureRequirement.Build(_builder.buildLocation);
        if(building != null)
            _builder.buildingTarget = building.GetComponent<Building>();
    }

    public void OnExit()
    {
        _builder.ResetStateVariables();
    }

    public void Tick()
    {
        if (_builder.buildingTarget != null)
        {
            if(_timeForNextBuild < Time.time)
            {
                _timeForNextBuild = Time.time + (1f / _builder.buildSpeed);
                _builder.ApplyWorkToBuilding();
            }
        }
    }
}