namespace ModernApi.Services;

using Microsoft.Extensions.Diagnostics.HealthChecks;

public class ApiHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = new())
    {
        var isHealthy = true;

        // TODO: check some health 

        if (isHealthy)
            return Task.FromResult(
                HealthCheckResult.Healthy("A healthy result."));

        return Task.FromResult(
            new HealthCheckResult(
                context.Registration.FailureStatus, "An unhealthy result."));
    }
}