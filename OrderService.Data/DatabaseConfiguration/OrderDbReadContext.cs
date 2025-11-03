using Microsoft.EntityFrameworkCore;

namespace OrderService.Data.DatabaseConfiguration;

public class OrderDbReadContext : DbContext
{
    public OrderDbReadContext(DbContextOptions<OrderDbReadContext> options) : base( options)
    {
    }
    
    public DbSet<OrderDbOrder> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderDbOrderType>()
            .HasOne(e => e.Order)
            .WithMany(e => e.OrderTypes)
            .HasForeignKey(e => e.OrderId);
    }
}