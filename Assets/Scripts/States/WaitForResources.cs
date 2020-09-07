using UnityEngine;

public class WaitForResources : IState
{
    public bool hasTimedOut = false;
    public float waitTimeLimit;

    private Builder _builder;
    private float WaitTime;

    public WaitForResources(Builder builder, float waitTimeLimit)
    {
        this.waitTimeLimit = waitTimeLimit;
        this._builder = builder;
    }

    public void OnEnter()
    {
        hasTimedOut = false;
        WaitTime = 0;
    }

    public void OnExit()
    {
        hasTimedOut = false;
    }

    public void Tick()
    {
        WaitTime += Time.deltaTime;
        if (WaitTime > waitTimeLimit)
        {
            hasTimedOut = true;
            _builder.structureRequirement = null;
            Debug.Log("Wait for Resources has Expired");
        }
    }
}