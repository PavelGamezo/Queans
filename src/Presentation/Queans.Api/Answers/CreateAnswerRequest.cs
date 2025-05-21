namespace Queans.Api.Answers
{
    public record CreateAnswerRequest(
        Guid Id,
        string Text,
        int Rating,
        DateTime CreatedAt,
        DateTime UpdatedAt);
}
