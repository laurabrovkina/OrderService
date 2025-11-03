using Mediator;
using OrderService.GraphQL.Types;

namespace OrderService.Application.Queries;

public class GetOrderByUsernameHandler : IRequestHandler<GetOrderByUsernameRequest, Order>
{
    public ValueTask<Order> Handle(GetOrderByUsernameRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}