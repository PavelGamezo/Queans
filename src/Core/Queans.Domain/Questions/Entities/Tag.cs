using ErrorOr;
using Queans.Domain.Common;
using Queans.Domain.Questions.Errors;
using Queans.Domain.Questions.Events;

namespace Queans.Domain.Questions.Entities
{
    public class Tag : Entity<Guid>
    {
        public string Name { get; private set; } = null!;

        private readonly List<Question> _questions = new();
        public IReadOnlyCollection<Question> Questions => _questions;

        public Tag(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public static ErrorOr<Tag> CreateTag(string name)
        {
            var identifier = Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(name))
            {
                return QuestionDomainErrors.IncorrectTagNameInputError;
            }

            var tag = new Tag(identifier, name);

            tag.AddDomainEvent(new TagCreatedEvent(tag.Id));

            return tag;
        }
    }
}
