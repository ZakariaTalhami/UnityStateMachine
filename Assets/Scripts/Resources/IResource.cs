interface IResource
{
    ResourceType type {get;}
    bool Take(int takeAmount, out int collected);
    void UpdateSize();
    bool IsDepleted();

}