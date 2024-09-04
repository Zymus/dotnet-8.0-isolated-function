using FunctionApp1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var hostBuilder = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services => services
        .AddSingleton<FunctionMetrics>()
        .AddOpenTelemetry()
        .WithTracing(tracing => tracing
            .SetSampler(new AlwaysOnSampler())
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MyService").AddTelemetrySdk())
            .AddSource("Sample.DistributedTracing")
            .AddAspNetCoreInstrumentation()
            .AddConsoleExporter()));

var host = hostBuilder.Build();

host.Run();
