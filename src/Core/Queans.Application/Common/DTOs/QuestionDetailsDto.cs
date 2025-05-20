namespace Queans.Application.Common.DTOs
{
    public record QuestionDetailsDto(Guid Id,
        int Rating,
        string AuthorName,
        string Title,
        List<TagDto> Tags,
        List<AnswerDto> Answers);
}
