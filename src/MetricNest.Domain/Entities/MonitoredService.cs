using MetricNest.Domain.Enums;

namespace MetricNest.Domain.Entities;

public sealed class MonitoredService
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Url { get; private set; } = string.Empty;

    public ServiceStatus Status { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public DateTime? LastCheckedAtUtc { get; private set; }

    private MonitoredService()
    {
    }

    public MonitoredService(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Service name is required.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("Service URL is required.", nameof(url));
        }

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp &&
             uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new ArgumentException(
                "Service URL must be a valid HTTP or HTTPS URL.",
                nameof(url));
        }

        Id = Guid.NewGuid();
        Name = name.Trim();
        Url = url.Trim();
        Status = ServiceStatus.Unknown;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void UpdateStatus(
        ServiceStatus status,
        DateTime checkedAtUtc)
    {
        Status = status;
        LastCheckedAtUtc = checkedAtUtc;
    }
}