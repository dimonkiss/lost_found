using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using lost_found_web.Services;

namespace lost_found_web.Tests.Integration;

public class WebApplicationFactory : WebApplicationFactory<lost_found_web.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Replace the real service with a test service if needed
            // This allows for more controlled testing scenarios
        });
    }
}
