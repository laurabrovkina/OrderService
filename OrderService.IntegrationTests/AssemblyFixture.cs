using OrderService.IntegrationTests.Infrastructure;
using Xunit;

[assembly: TestFramework("OrderService.IntegrationTests.AssemblyFixture", "OrderService.IntegrationTests")]

namespace OrderService.IntegrationTests;

public class AssemblyFixture
{
    public AssemblyFixture()
    {
        var dockerComposeStarter = new DockerComposeStarter("../../../../compose.yaml");
        dockerComposeStarter.Start();
    }
}