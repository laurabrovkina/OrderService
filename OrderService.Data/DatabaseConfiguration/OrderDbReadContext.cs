using Microsoft.EntityFrameworkCore;

namespace OrderService.Data.DatabaseConfiguration;

public class OrderDbReadContext : DbContext
{
    public OrderDbReadContext(DbContextOptions<OrderDbReadContext> options) : base( options)
    {
    }
    
    public DbSet<OrderDbOrder> Orders { get; set; }
    public DbSet<OrderDbOrderType> OrderTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderDbOrder>()
            .HasOne(e => e.OrderType)
            .WithMany(e => e.Orders)
            .HasForeignKey(e => e.OrderTypeId);
    }
}