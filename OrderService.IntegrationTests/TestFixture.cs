using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

    public async Task CreateOrder(OrderDbOrder order)
    {
        await using var dbContext = GetOrderDbReadContext();
        await dbContext.Orders.AddAsync(order);
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