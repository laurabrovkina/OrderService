using OrderService.Data.DatabaseConfiguration;
using OrderService.GraphQL.Types;

namespace OrderService.Application.Mappings;

public static class OrderMapper
{
    public static Order ToOrder(this OrderDbOrder dbOrder)
    {
        return new Order
        {
            Username = dbOrder.Username,
            OrderType = MapType(dbOrder.OrderType.Id)
        };
    }

    private static OrderType MapType(int dbOrderOrderType)
    {
        return dbOrderOrderType switch
        {
            1 => OrderType.Individual,
            2 => OrderType.Bulk,
            3 => OrderType.Prepaid,
            4 => OrderType.Other,
            _ => OrderType.Unknown
        };
    }
}