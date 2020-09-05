using UnityEngine;

public abstract class Structure :ScriptableObject 
{
    public new string name;
    public GameObject Prefab;
    public int requiredWoodResources = 0; 
    public int requiredStoneResources = 0;
    public int requiredGoldResources = 0; 
    public Vector2Int requiredGridSpaces;

    public virtual ResourceCollection GetResourceRequirements()
    {
        ResourceCollection requirements = new ResourceCollection();
        requirements.AddToResource(ResourceType.Wood, requiredWoodResources);
        requirements.AddToResource(ResourceType.Stone, requiredStoneResources);
        requirements.AddToResource(ResourceType. Gold, requiredGoldResources);

        return requirements;
    }

    public virtual void GetWidthAndHeight(out int width, out int height)
    {
        width = requiredGridSpaces.x;
        height = requiredGridSpaces.y;
    }
}