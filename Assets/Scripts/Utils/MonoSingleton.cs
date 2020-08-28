using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("MonoSingleton is Null!");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this as T;
        init();
    }

    protected virtual void init() { }
}