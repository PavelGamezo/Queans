namespace Queans.Domain.Common
{
    public abstract class ValueObject
    {
        public abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? other)
        {
            if (other is null || GetType() != other.GetType())
            {
                return false;
            }

            var valueObject = (ValueObject)other;

            return GetEqualityComponents()
                .SequenceEqual(valueObject.GetEqualityComponents());
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(q => q?.GetHashCode() ?? 0)
                .Aggregate((q, w) => q ^ w);
        }
    }
}
