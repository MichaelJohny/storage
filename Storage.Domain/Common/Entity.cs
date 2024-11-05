namespace Storage.Domain.Common;

public class Entity : Entity<int> { }

public abstract class Entity<T>
{
    public virtual T Id { get; set; }
}
    
public interface IEntity<T>
{
    T Id { get; set; }
}
    
public interface IEntity : IEntity<int>{}

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}