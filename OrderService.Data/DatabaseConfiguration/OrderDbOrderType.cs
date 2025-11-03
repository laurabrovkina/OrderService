using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Data.DatabaseConfiguration;

[Table("OrderType", Schema = "OrderSchema")]
public class OrderDbOrderType
{
    [Key]
    public int Id { get; set; }
    public Guid OrderId { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public OrderDbOrder Order { get; set; }
}