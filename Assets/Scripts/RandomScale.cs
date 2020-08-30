using UnityEngine;

public class RandomScale : MonoBehaviour
{

    [Range(0, 1)]
    [SerializeField] private float _maxScaleModifier = default;
    [SerializeField] private bool _allowShrinking = false;
    [SerializeField] private Transform _GFX = null;

    void Start()
    {
        float minScaleModifier = _allowShrinking == true ? -_maxScaleModifier : 0;
        float randomScaleAddedScale = Random.Range(minScaleModifier, _maxScaleModifier);
        Vector3 scaleModifier = new Vector3(randomScaleAddedScale, randomScaleAddedScale, randomScaleAddedScale);
        _GFX.localScale += _GFX.localScale * randomScaleAddedScale;
    }
}
