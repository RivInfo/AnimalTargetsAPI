namespace CupTest2023.Data;

public interface IRepository<T, K>
{
    public K Register(T requestData);

    public bool TryFindUser(T requestData);
}
