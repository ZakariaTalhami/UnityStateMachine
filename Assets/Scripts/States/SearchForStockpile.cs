using UnityEngine;
using System.Linq;

public class SearchForStockpile : IState
{
    private readonly Gatherer _gatherer;

    public SearchForStockpile(Gatherer gatherer)
    {
        _gatherer = gatherer;
    }
    
    public void Tick()
    {
        _gatherer.stockPile = GetClosestStockPile();
    }

    private Stockpile GetClosestStockPile()
    {
        return Object.FindObjectsOfType<Stockpile>()
            .OrderBy(s => Vector3.Distance(_gatherer.transform.position, s.transform.position))
            .Where(s => s.IsFull() == false && s.type == _gatherer.resourceTargetType)
            .FirstOrDefault();
    }

    public void OnEnter() { }

    public void OnExit() { }
}