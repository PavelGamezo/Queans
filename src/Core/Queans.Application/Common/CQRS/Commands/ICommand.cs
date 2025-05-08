using MediatR;

namespace Queans.Application.Common.CQRS.Commands
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
