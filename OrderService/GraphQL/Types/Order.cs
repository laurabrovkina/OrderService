using System.ComponentModel.DataAnnotations;
using HotChocolate.Types;

namespace OrderService.GraphQL.Types;

/// <summary>
/// Order Interface Object
/// </summary>
[ObjectType]
public abstract class Order
{
    /// <summary>
    /// Username is a Unique Order Identifier.
    /// Also As a Primary Key To Join With Other SubGraph
    /// </summary>
    [Key]
    public string Username { get; set; } = string.Empty;

    public OrderType OrderType { get; set; }
}