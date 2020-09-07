using UnityEngine;
using System.Collections.Generic;

public class GFXProgress : MonoBehaviour
{

    private List<GameObject> _GFXList;
    private IProgressEntity _progressEntity;


    private void Awake()
    {
        _GFXList = new List<GameObject>();

        foreach (Transform child in transform)
            _GFXList.Add(child.gameObject);

        ProgressUpdated(0);

        _progressEntity = transform.parent.GetComponent<IProgressEntity>();
        _progressEntity.onProgressUpdated += ProgressUpdated;
    }

    private void ProgressUpdated(float progress)
    {
        int level = GetProgressLevel(progress);
        EnableGFXToLevel(level);
    }

    private int GetProgressLevel(float progress)
    {
        return Mathf.FloorToInt(progress * (_GFXList.Count -1));
    }

    private void EnableGFXToLevel(int level)
    {
        for (int i = 0; i < _GFXList.Count; i++)
        {
            _GFXList[i].SetActive(i <= level);
        }
      }
}