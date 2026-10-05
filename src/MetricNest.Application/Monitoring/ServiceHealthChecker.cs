using MetricNest.Domain.Enums;

namespace MetricNest.Application.Monitoring;

public sealed class ServiceHealthChecker
{
    private readonly HttpClient _httpClient;

    public ServiceHealthChecker(HttpClient httpClient)
    {
        _httpClient = httpClient;
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

            if (response.IsSuccessStatusCode)
            {
                return ServiceStatus.Healthy;
            }

            if ((int)response.StatusCode >= 500)
            {
                return ServiceStatus.Down;
            }

            return ServiceStatus.Degraded;
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