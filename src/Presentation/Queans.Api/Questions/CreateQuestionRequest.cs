namespace Queans.Api.Questions
{
    public record CreateQuestionRequest(string Title, string Description, List<string> Tags);
}
