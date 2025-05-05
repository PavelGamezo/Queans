
using ErrorOr;

namespace Queans.Domain.Users.ValueObjects
{
    public class Rating : Common.ValueObject
    {
        public int Value { get; private set; }

        public Rating(int rating)
        {
            Value = rating;
        }

        public static ErrorOr<Rating> Create(int value)
        {
            if (value < 0)
            {
                return Errors.UserDomainErrors.FailureRatingError;
            }

            return new Rating(value);
        }

        public void UpvoteRating()
        {
            Value++;
        }

        public void DownvoteRating()
        {
            if (Value != 0)
            {
                Value--;
            }
        }

        public static implicit operator int(Rating rating) =>
            rating.Value;

        public static implicit operator Rating(int rating) => 
            new Rating(rating);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
