using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Data.DatabaseConfiguration;

[Table("Order", Schema = "OrderSchema")]
public class OrderDbOrder
{
    [Key]
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    
    public ICollection<OrderDbOrderType> OrderTypes { get; set; }
}