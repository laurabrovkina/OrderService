using Mediator;
using OrderService.GraphQL.Types;

namespace OrderService.Application.Queries;

public class GetOrderByUsernameRequest : IRequest<Order>
{
    public string? Username { get; set; }
}