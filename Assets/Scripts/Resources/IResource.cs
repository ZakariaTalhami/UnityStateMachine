interface IResource
{
    bool Take(int takeAmount, out int collected);
    void UpdateSize();
    bool IsDepleted();

}