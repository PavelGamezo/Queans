using Queans.Domain.Questions.Entities;

namespace Queans.Application.UnitTests.TestUtils.Constants
{
    public static partial class Constants
    {
        public static class Question
        {
            public static readonly Guid QuestionId = Guid.NewGuid();

            public const string Title = "Question title";

            public const string Description = "Question description";

            public static readonly List<string> Tags = new List<string>
            {
                "Tag Name" 
            };
        }
    }
}
