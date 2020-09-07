public class FindBuildLocation : IState
{
    private readonly Builder _builder;

    public FindBuildLocation(Builder builder)
    {
        this._builder = builder;
    }

    public void OnEnter() { }

    public void OnExit() { }

    public void Tick()
    {
        _builder.buildLocation = _builder.structureRequirement.FindBuildLoaction();
    }
}