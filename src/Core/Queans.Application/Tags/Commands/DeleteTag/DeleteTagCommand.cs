using ErrorOr;
using Queans.Application.Common.CQRS.Commands;

namespace Queans.Application.Tags.Commands.DeleteTag
{
    public record DeleteTagCommand(Guid Id) : ICommand<ErrorOr<Success>>;
}
