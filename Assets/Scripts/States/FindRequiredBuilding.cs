public class FindRequiredBuilding : IState
{
    private readonly Builder _builder;
    private readonly BuildingManager _buildingManager;

    public FindRequiredBuilding(Builder builder, BuildingManager buildingManager)
    {
        _builder = builder;
        _buildingManager = buildingManager;
    }

    public void OnEnter() { }

    public void OnExit() { }

    public void Tick()
    {
        _builder.structureRequirement = _buildingManager.GetNextRequirement();
    }
}