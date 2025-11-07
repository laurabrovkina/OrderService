using HotChocolate.Execution;
using OrderService.Data.DatabaseConfiguration;
using OrderService.GraphQL;
using OrderService.GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Path = System.IO.Path;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediator(options =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
});

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
    
    var ddlScript = dbContext.Database.GenerateCreateScript();
    // Write it to a file
    // bin\Debug\net9.0\
    var baseDir = AppContext.BaseDirectory;
    var projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\.."));
    var targetPath = Path.Combine(projectRoot, "OrderService.IntegrationTests", "Database", "initdb.sql");

    Directory.CreateDirectory(Path.GetDirectoryName(targetPath) ?? throw new InvalidOperationException());
    File.WriteAllText(targetPath, ddlScript);
}

app.MapGraphQL();

app.RunWithGraphQLCommands(args);

public partial class Program;