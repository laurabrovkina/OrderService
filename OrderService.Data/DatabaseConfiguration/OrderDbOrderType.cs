using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Data.DatabaseConfiguration;

[Table("OrderTypes", Schema = "OrderSchema")]
public class OrderDbOrderType
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<OrderDbOrder> Orders { get; set; } = [];
}
