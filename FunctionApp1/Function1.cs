using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionApp1;

public class Function1(ILogger<Function1> logger, FunctionMetrics functionMetrics)
{
    public static readonly ActivitySource MyActivities = new("Sample.DistributedTracing");

    [Function("Function1")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        CancellationToken cancellationToken = default)
    {
        using var activity = MyActivities.StartActivity();
        functionMetrics.IncrementExecutionCount();

        string name = req.Query["name"];
        activity?.AddTag("test.key", name);


        logger.LogInformation("Echoing");

        cancellationToken.ThrowIfCancellationRequested();

        return new OkObjectResult(name);
    }
}
