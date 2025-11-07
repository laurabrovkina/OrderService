namespace OrderService.GraphQL.Types;

public enum OrderType
{
    Unknown = 0,
    Individual = 1,
    Bulk = 2,
    Prepaid = 3,
    Other = 4
}