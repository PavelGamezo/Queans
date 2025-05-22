namespace Queans.Api.Questions
{
    public record UpdateQuestionRequest(
        string Title,
        string Description,
        List<string> Tags);
}
