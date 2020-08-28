using UnityEngine;

public class RandomScale : MonoBehaviour
{

    [Range(0, 1)]
    [SerializeField] private  float maxScaleModifier;
    [SerializeField] private bool allowShrinking = false;
    [SerializeField] private Transform GFX;

    void Start()
    {
        float minScaleModifier = allowShrinking == true ? -maxScaleModifier : 0;
        float randomScaleAddedScale = Random.Range(minScaleModifier, maxScaleModifier);
        Vector3 scaleModifier = new Vector3(randomScaleAddedScale, randomScaleAddedScale, randomScaleAddedScale);
        GFX.localScale += GFX.localScale * randomScaleAddedScale;
    }
}
