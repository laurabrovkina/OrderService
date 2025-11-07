using OrderService.Data.DatabaseConfiguration;
using OrderService.IntegrationTests.Builder;
using Shouldly;
using Xunit;

namespace OrderService.IntegrationTests.Tests.Queries;

public class GetOrderByUsername : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;
    
    public GetOrderByUsername(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetOrderByUsername_ReturnsOrder()
    {
        // Arrange
        var client = _fixture.GetOrderServiceClient();

        var order = new OrderBuilder().BasicOrderBuilder()
            .With(c => c.Username = Guid.NewGuid().ToString())
            .Build();
        var orderType = new OrderDbOrderType
        {
            Name = "Individual",
            Orders = [order]
        };

        await _fixture.CreateOrder(order, orderType);
        
        // Act
        var response = await client.GetOrderByUserName.ExecuteAsync(order.Username);
        await _fixture.DeleteOrder(order);
        await _fixture.DeleteOrderType(orderType);
        
        // Assert
        response.Data.ShouldNotBeNull();
        response.Data.OrderByUserName.ShouldNotBeNull();
        response.Data.OrderByUserName.Username.ShouldBe(order.Username);
    }
}