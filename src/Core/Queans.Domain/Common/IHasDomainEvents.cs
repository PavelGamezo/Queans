namespace Queans.Domain.Common
{
    public interface IHasDomainEvents
    {
        public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void ClearDomainEvents();
        void AddDomainEvent(IDomainEvent domainEvent);
    }
}
