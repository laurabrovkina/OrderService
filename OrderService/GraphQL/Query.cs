using Mediator;
using OrderService.Application.Queries;
using OrderService.GraphQL.Types;

namespace OrderService.GraphQL;

/// <summary>
/// The Query class serves as the entry point
/// for GraphQL queries in the OrderService application.
/// </summary>
public class Query
{
    /// <summary>
    /// Get Orders by Username.
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<Order?> GetOrderByUserName([Service] IMediator mediator, string? username)
    {
        return await mediator.Send(new GetOrderByUsernameRequest { Username = username });
    }
}