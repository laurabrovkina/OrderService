using Mediator;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Mappings;
using OrderService.Data.DatabaseConfiguration;
using OrderService.GraphQL.Types;

namespace OrderService.Application.Queries;

/// <summary>
/// Handles the request to retrieve an order by a specified username.
/// </summary>
/// <remarks>
/// This handler processes a query to fetch a user's order by their username.
/// It interacts with the database context to retrieve the relevant order data.
/// </remarks>
public class GetOrderByUsernameHandler : IRequestHandler<GetOrderByUsernameRequest, Order>
{
    private readonly OrderDbReadContext _orderDbContext;

    public GetOrderByUsernameHandler(OrderDbReadContext orderDbContext)
    {
        _orderDbContext = orderDbContext;
    }

    public async ValueTask<Order> Handle(GetOrderByUsernameRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderDbContext.Orders
            .Where(x => x.Username == request.Username)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        _ = order ?? throw new Exception("Orders not found");

        return order.ToOrder();
    }
}