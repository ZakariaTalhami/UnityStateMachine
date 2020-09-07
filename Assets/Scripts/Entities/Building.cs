using UnityEngine;

public class Building : MonoBehaviour, IProgressEntity
{
    [SerializeField] private int _constructionGoal = default;
    private int _work = 0;
    public event GenericDelegates.FloatHandler onProgressUpdated;

    public void AddWork(int workAmount)
    {
        if(!IsComplete())
        {
            _work += workAmount;
            if (_work > _constructionGoal) _work = _constructionGoal;
            onProgressUpdated?.Invoke((float) _work / _constructionGoal);
        }
    }

    public bool IsComplete()
    {
        return _work >= _constructionGoal;
    }
}