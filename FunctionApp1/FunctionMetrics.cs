using System.Diagnostics.Metrics;

namespace FunctionApp1;

public class FunctionMetrics
{
    private readonly Counter<int> _executionCount;

    public FunctionMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("FunctionMetrics");
        _executionCount = meter.CreateCounter<int>("z.function.execution.count");
    }

    public void IncrementExecutionCount() => _executionCount.Add(1);
}
