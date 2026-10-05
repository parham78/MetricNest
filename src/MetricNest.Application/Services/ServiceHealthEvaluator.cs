using MetricNest.Domain.Enums;

namespace MetricNest.Application.Services;

public sealed class ServiceHealthEvaluator
{
    public ServiceStatus Evaluate(int statusCode)
    {
        if (statusCode >= 200 && statusCode <= 299)
        {
            return ServiceStatus.Healthy;
        }

        if (statusCode >= 500 && statusCode <= 599)
        {
            return ServiceStatus.Down;
        }

        return ServiceStatus.Degraded;
    }
}