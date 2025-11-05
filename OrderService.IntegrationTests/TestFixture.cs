using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderService.Data.DatabaseConfiguration;

namespace OrderService.IntegrationTests;

public class TestFixture : WebApplicationFactory<Program>
{
    private readonly DbContextOptions<OrderDbReadContext> _orderDbReadContextOptions;
    
    public TestFixture()
    {
        var config = GetConfiguration();

        var postgreSqlOptionsBuilder = new DbContextOptionsBuilder<OrderDbReadContext>();
        postgreSqlOptionsBuilder.UseNpgsql(config.GetConnectionString("OrderDb"),
            sqlOptions => { sqlOptions.EnableRetryOnFailure(); });
        _orderDbReadContextOptions = postgreSqlOptionsBuilder.Options;
    }

    public OrderDbReadContext GetOrderDbReadContext()
    {
        var dbContext = new OrderDbReadContext(_orderDbReadContextOptions);
        return dbContext;
    }

    private IConfiguration GetConfiguration()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        return config;
    }
}