using MetricNest.Domain.Enums;

namespace MetricNest.Application.Monitoring;

public interface IServiceHealthChecker
{
    Task<ServiceStatus> CheckAsync(
        string url,
        CancellationToken cancellationToken = default);
}