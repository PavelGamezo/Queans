using MediatR;

namespace Queans.Application.Common.CQRS.Queries
{
    public interface IQueryHandler<in TQuery> : 
        IRequestHandler<TQuery> where TQuery : IRequest
    {
    }

    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult> where TQuery : IRequest<TResult>
    {
    }
}
