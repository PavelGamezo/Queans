namespace Queans.Application.Common.DTOs
{
    public record AnswerDto(
        Guid Id,
        string Text,
        string AuthorName,
        int Rating,
        DateTime CreatedAt,
        DateTime UpdatedAt);
}
