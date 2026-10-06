using MetricNest.Application.Services;
using MetricNest.Domain.Enums;

namespace MetricNest.Application.Tests.Services;

public sealed class ServiceHealthEvaluatorTests
{
    private readonly ServiceHealthEvaluator _evaluator = new();

    [Theory]
    [InlineData(200)]
    [InlineData(201)]
    [InlineData(204)]
    [InlineData(299)]
    public void Evaluate_WithSuccessfulStatusCode_ReturnsHealthy(int statusCode)
    {
        var result = _evaluator.Evaluate(statusCode);

        Assert.Equal(ServiceStatus.Healthy, result);
    }

    [Theory]
    [InlineData(500)]
    [InlineData(502)]
    [InlineData(503)]
    [InlineData(599)]
    public void Evaluate_WithServerError_ReturnsDown(int statusCode)
    {
        var result = _evaluator.Evaluate(statusCode);

        Assert.Equal(ServiceStatus.Down, result);
    }

    [Theory]
    [InlineData(300)]
    [InlineData(400)]
    [InlineData(401)]
    [InlineData(404)]
    public void Evaluate_WithOtherStatusCode_ReturnsDegraded(int statusCode)
    {
        var result = _evaluator.Evaluate(statusCode);

        Assert.Equal(ServiceStatus.Degraded, result);
    }
}