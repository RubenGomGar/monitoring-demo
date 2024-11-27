using Prometheus;

namespace AspireSample.ApiService.Infrastructure;

public static class CustomMetrics
{
    public static readonly Gauge TotalMessage = Metrics.CreateGauge("apiservice_total_messages", "Total messages");
    public static readonly Gauge PendingMessages = Metrics.CreateGauge("apiservice_pending_messages", "Pending messages");
    public static readonly Gauge CompletedMessages = Metrics.CreateGauge("apiservice_completed_messages", "Completed messages");
    public static readonly Gauge FailedMessages = Metrics.CreateGauge("apiservice_failed_messages", "Failed messages");
}