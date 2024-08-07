namespace LeaveTimes.Domain.Entities;

public abstract class Entity : IEntity
{
    protected Entity()
    {
    }

    public abstract object[] GetKeys();
}

public abstract class Entity<TKey> : Entity where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; } = default!;

    protected Entity()
    {
    }

    protected Entity(TKey id) : this()
    {
        Id = id;
    }

    public override object[] GetKeys() => [Id];
}
