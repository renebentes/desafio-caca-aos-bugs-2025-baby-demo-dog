using BugStore.Messaging;

namespace BugStore.Orders.GetOrderById;

public sealed record GetOrderByIdRequest(Guid Id)
    : IRequest<GetOrderByIdResponse>;
