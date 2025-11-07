using OrderService.Data.DatabaseConfiguration;

namespace OrderService.IntegrationTests.Builder;

public class OrderBuilder : BaseBuilder<OrderDbOrder>
{
    public OrderBuilder BasicOrderBuilder()
    {
        Model.Username = "TestUser";
        Model.OrderTypeId = 0;
        // this line prevents adding new order types to the database
        Model.OrderType = null!;

        return this;
    }
}