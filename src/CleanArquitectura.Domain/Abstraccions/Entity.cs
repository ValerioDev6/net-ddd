namespace CleanArquitectura.Domain.Abstraccions;

public abstract class Entity<TEntityId> : IEntity
{
    protected Entity()
    {

    }
    private readonly List<IDomainEvents> _domainEvents = new();
    
    protected Entity(TEntityId id)
    {
        Id = id;
    }
    
    public TEntityId? Id {get ; init;}

    public IReadOnlyList<IDomainEvents> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvents domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }


}