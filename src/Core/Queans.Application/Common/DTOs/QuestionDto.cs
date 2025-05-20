namespace Queans.Application.Common.DTOs
{
    public record QuestionDto(
        Guid Id,
        int Rating,
        string AuthorName,
        string Title,
        List<TagDto> Tags);
}
