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
            .With(c => c.Username = "100011")
            .Build();
        
        await _fixture.CreateOrder(order);
        
        // Act
        var response = await client.GetOrderByUserName.ExecuteAsync(order.Username);
        
        // Assert
        response.Data.ShouldNotBeNull();
        response.Data.OrderByUserName.ShouldNotBeNull();
        response.Data.OrderByUserName.Username.ShouldBe(order.Username);
    }
}