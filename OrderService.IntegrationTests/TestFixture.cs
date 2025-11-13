using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Data.DatabaseConfiguration;
using OrderService.IntegrationTests.GraphQL;

namespace OrderService.IntegrationTests;

public class TestFixture : WebApplicationFactory<Program>
{
    private IOrderServiceClient? _orderServiceClient;
    private readonly DbContextOptions<OrderDbReadContext> _orderDbReadContextOptions;
    
    public TestFixture()
    {
        var config = GetConfiguration();

        var postgreSqlOptionsBuilder = new DbContextOptionsBuilder<OrderDbReadContext>();
        postgreSqlOptionsBuilder.UseNpgsql(config.GetConnectionString("OrderDb"),
            sqlOptions => { sqlOptions.EnableRetryOnFailure(); });
        _orderDbReadContextOptions = postgreSqlOptionsBuilder.Options;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureHostConfiguration(config =>
        {
            config.AddJsonFile("appsettings.json");
        });
        
        return base.CreateHost(builder);
    }

    public IOrderServiceClient GetOrderServiceClient()
    {
        InitializeOrderServiceClient();
        return _orderServiceClient ?? throw new Exception("OrderServiceClient is not initialized");
    }

    public OrderDbReadContext GetOrderDbReadContext()
    {
        var dbContext = new OrderDbReadContext(_orderDbReadContextOptions);
        return dbContext;
    }

    public async Task CreateOrder(OrderDbOrder order, OrderDbOrderType orderType)
    {
        await using var dbContext = GetOrderDbReadContext();
        dbContext.Attach(order);
        order.OrderType = orderType;
        dbContext.OrderTypes.Add(orderType);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteOrder(OrderDbOrder order)
    {
        await using var dbContext = GetOrderDbReadContext();
        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteOrderType(OrderDbOrderType orderType)
    {
        await using var dbContext = GetOrderDbReadContext();
        dbContext.OrderTypes.Remove(orderType);
        dbContext.Orders.RemoveRange(orderType.Orders);
        await dbContext.SaveChangesAsync();
    }

    private IConfiguration GetConfiguration()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        return config;
    }

    private void InitializeOrderServiceClient()
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddHttpClient("OrderServiceClient", c =>
        {
            c.BaseAddress = new Uri(Server.BaseAddress, "graphql");
        })
        .ConfigurePrimaryHttpMessageHandler(_ => Server.CreateHandler());
        
        serviceCollection.AddOrderServiceClient();
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _orderServiceClient = serviceProvider.GetRequiredService<IOrderServiceClient>();
    }
}