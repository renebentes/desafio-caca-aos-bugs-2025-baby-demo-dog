using BugStore.Common.Primitives.Results;

namespace BugStore.Messaging;

public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<Result<TResponse>> HandleAsync(
        TRequest request,
        CancellationToken cancellationToken = default);
}

public interface IRequestHandler<in TRequest>
    where TRequest : IRequest
{
    Task<Result> HandleAsync(
        TRequest request,
        CancellationToken cancellationToken = default);
}
