using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Data.DatabaseConfiguration;

[Table("Orders", Schema = "OrderSchema")]
public class OrderDbOrder
{
    [Key]
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public int OrderTypeId { get; set; }

    public OrderDbOrderType OrderType { get; set; } = new();
}