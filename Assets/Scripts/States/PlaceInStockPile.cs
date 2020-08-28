using UnityEngine;

public class PlaceInStockPile : IState
{
    private readonly Gatherer _gatherer;
    private float _timeForNextStore;

    public PlaceInStockPile(Gatherer gatherer)
    {
        _gatherer = gatherer;
    }

    public void Tick()
    {
        if (_gatherer.stockPile != null)
        { 
            if(_timeForNextStore < Time.time)
            {
                _timeForNextStore = Time.time + (1f / _gatherer.storeSpeed);
                _gatherer.StoreInStockpile();
            } 
        }
    }

    public void OnEnter() { }

    public void OnExit() { }
}