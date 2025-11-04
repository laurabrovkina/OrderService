using HotChocolate.Execution;
using OrderService.Data.DatabaseConfiguration;
using OrderService.GraphQL;
using OrderService.GraphQL.Types;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediator();

builder.Services.AddDbContextPool<OrderDbReadContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("OrderDb")));

await builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<Order>()
    .UseDefaultPipeline()
    .BuildSchemaAsync();
    
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbReadContext>();
    dbContext.Database.EnsureCreated();
}

app.MapGraphQL();

app.RunWithGraphQLCommands(args);

public partial class Program;