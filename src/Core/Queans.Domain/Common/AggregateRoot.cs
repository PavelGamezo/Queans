namespace Queans.Domain.Common
{
    public abstract class AggregateRoot<TId> : Entity<TId>
    {
        public AggregateRoot(TId id) : base(id)
        {
        }
    }
}
