using OrderService.Data.DatabaseConfiguration;

namespace OrderService.IntegrationTests.Builder;

public class OrderBuilder : BaseBuilder<OrderDbOrder>
{
    public OrderBuilder BasicOrderBuilder()
    {
        Model.Username = "TestUser";
        Model.OrderTypes = [
            new OrderDbOrderType
            {
                OrderId = Guid.NewGuid(),
                Name = "Test Order Type",
                Order = Model
            }
        ];

        return this;
    }
}