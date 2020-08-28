using UnityEngine;
public class CollectResource : IState
{
    private Gatherer _gatherer;

    private float _timeForNextCollection;

    public CollectResource(Gatherer gatherer)
    {
        _gatherer = gatherer;
    }

    public void Tick()
    {
        if (_gatherer.target != null)
        {
            if(_timeForNextCollection < Time.time)
            {
                _timeForNextCollection = Time.time + (1f / _gatherer.gatherSpeed);
                _gatherer.TakeFromTarget();
            }
        }
    }

    public void OnEnter()
    {
        Debug.Log("Enter Collect Resource");
    }

    public void OnExit()
    {
        _gatherer.target = null;
        Debug.Log("Exit Collect Resource");
    }
}