using UnityEngine;
using System.Collections;

public abstract class Resource : MonoBehaviour, IResource
{ 
    public abstract ResourceType type {get;}
    [SerializeField] private int _totalAvailable = 20;
    [SerializeField] private float _regenerationTime = 15f;
    private int _available;
    private Vector3 _initLocalScale;
    
    private void Start() {
        _available = _totalAvailable;
        _initLocalScale = transform.localScale;
    }

    IEnumerator ResetResource() {
        yield return new WaitForSeconds(_regenerationTime);
        _available = _totalAvailable;
        transform.localScale = _initLocalScale;
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
            // gameObject.SetActive(false);
            transform.localScale = Vector3.zero;
            StartCoroutine(ResetResource());
        }
    }

    public bool IsDepleted()
    {
        return _available <= 0;
    }
}