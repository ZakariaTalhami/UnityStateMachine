using UnityEngine;

public class Resource : MonoBehaviour, IResource
{ 
    [SerializeField] private int _totalAvailable = 20;
    private int _available;
    private Vector3 _initLocalScale;
    
    private void Start() {
        _available = _totalAvailable;
        _initLocalScale = transform.localScale;
    }

    public bool Take(int takeAmount, out int collected)
    {
        collected = 0;
        if(_available <= 0)
            return false;

        _available -= takeAmount;
        collected = _available < 0 ? takeAmount + _available: takeAmount;

        UpdateSize();

        return true;
    }

    public void UpdateSize()
    {
        float scale = (float) _available / _totalAvailable;
        if(scale > 0 && scale < 1f)
        {
            Vector3 newScale = _initLocalScale * scale;
            transform.localScale = newScale;
        } 
        else if (scale <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public bool IsDepleted()
    {
        return _available <= 0;
    }
}