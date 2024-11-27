namespace AspireSample.ApiService.Options
{
    public class ApiServiceOptions
    {
        public bool UsePrometheusMetricsServer { get; set; } = true;
        public int PrometheusPort { get; set; } = 8080;
    }
}
