using UnityEngine;
public class BuildingMetadata
{
    public GameObject gameObject { get; private set; }
    public Structure structure { get; private set; }

    // Is this Needed?
    public IGrid gird { get; private set; }
    public int xGridPosition { get; private set; }
    public int yGridPosition { get; private set; }

    public BuildingMetadata(GameObject gameObject, Structure structure, IGrid gird, int xGridPosition, int yGridPosition)
    {
        this.gameObject = gameObject;
        this.structure = structure;
        this.gird = gird;
        this.xGridPosition = xGridPosition;
        this.yGridPosition = yGridPosition;
    }
}