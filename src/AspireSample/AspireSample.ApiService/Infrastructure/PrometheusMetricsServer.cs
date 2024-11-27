using AspireSample.ApiService.Options;
using Microsoft.Extensions.Options;
using Prometheus;
using Prometheus.DotNetRuntime;

namespace AspireSample.ApiService.Infrastructure;

internal class PrometheusMetricsServer : IHostedService
{
    private MetricServer _server = null;
    private readonly IHostEnvironment _environment;
    private readonly ApiServiceOptions _options;

    public PrometheusMetricsServer(IHostEnvironment environment, IOptions<ApiServiceOptions> options)
    {
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        _ = options ?? throw new ArgumentNullException(nameof(options));

        _options = options.Value;
    }

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        ConfigureMetrics();

        _server = new MetricServer("*", _options.PrometheusPort);
        _server.Start();

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        await _server?.StopAsync();
    }
    private void ConfigureMetrics()
    {
        // Supress default metrics because is not necessary 
        Prometheus.Metrics.SuppressDefaultMetrics();

        // Configure global
        Prometheus.Metrics.DefaultRegistry.SetStaticLabels(new Dictionary<string, string>
        {
            { "env", _environment.EnvironmentName },
            { "scope","schedulenotification"}
        });

        // Initialize custom metrics
        CustomMetrics.TotalMessage.Set(0);
        CustomMetrics.PendingMessages.Set(0);
        CustomMetrics.CompletedMessages.Set(0);
        CustomMetrics.FailedMessages.Set(0);

        // Start collecting 
        DotNetRuntimeStatsBuilder
            .Customize()
            .StartCollecting();
    }
}