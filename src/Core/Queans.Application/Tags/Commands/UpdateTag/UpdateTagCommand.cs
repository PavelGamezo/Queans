using ErrorOr;
using Queans.Application.Common.CQRS.Commands;

namespace Queans.Application.Tags.Commands.UpdateTag
{
    public record UpdateTagCommand(Guid TagId, string Name) : ICommand<ErrorOr<Success>>;
}
