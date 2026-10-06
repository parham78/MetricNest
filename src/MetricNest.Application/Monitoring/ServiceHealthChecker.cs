using MetricNest.Application.Services;
using MetricNest.Domain.Enums;

namespace MetricNest.Application.Monitoring;

public sealed class ServiceHealthChecker : IServiceHealthChecker
{
    private readonly HttpClient _httpClient;
    private readonly ServiceHealthEvaluator _healthEvaluator;

    public ServiceHealthChecker(
        HttpClient httpClient,
        ServiceHealthEvaluator healthEvaluator)
    {
        _httpClient = httpClient;
        _healthEvaluator = healthEvaluator;
    }

    public async Task<ServiceStatus> CheckAsync(
        string url,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync(
                url,
                cancellationToken);

            return _healthEvaluator.Evaluate(
                (int)response.StatusCode);
        }
        catch (HttpRequestException)
        {
            return ServiceStatus.Down;
        }
        catch (TaskCanceledException)
        {
            return ServiceStatus.Down;
        }
    }
}