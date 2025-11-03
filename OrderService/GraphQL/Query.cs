using Mediator;
using OrderService.Application.Queries;
using OrderService.GraphQL.Types;

namespace OrderService.GraphQL;

public class Query
{
    /// <summary>
    /// Get Order by Username.
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<Order?> GetOrder([Service] IMediator mediator, string? username)
    {
        return await mediator.Send(new GetOrderByUsernameRequest { Username = username });
    }
}