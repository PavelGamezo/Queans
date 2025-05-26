using ErrorOr;
using Queans.Application.Common.CQRS.Commands;

namespace Queans.Application.Tags.Commands.CreateTag
{
    public record CreateTagCommand(string Name) : ICommand<ErrorOr<Success>>;
}
