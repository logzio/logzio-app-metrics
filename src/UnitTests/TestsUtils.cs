using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Apdex;
using App.Metrics.Counter;
using App.Metrics.Gauge;
using App.Metrics.Histogram;
using App.Metrics.Meter;
using App.Metrics.Timer;
using Core.Reporter;
using log4net;
using log4net.Config;

namespace UnitTests
{
    public class TestsUtils
    {
        private const string Log4NetConfigFilePath = "config/log4net.config";
     
        public const string LogzioConfigFilePath = "config/logzio.config";
        public const string LogzioConfigFileBadPath = "config/logzio_bad_path.config";
        public const string LogzioConfigFileBadFormatPath = "config/logzio_bad_format.config";
        public const string LogzioConfigFileBadUriPath = "config/logzio_bad_uri.config";
        public const string LogzioConfigFileBadEndpointPath = "config/logzio_bad_endpoint.config";
        public const string LogzioConfigFileBadTokenPath = "config/logzio_bad_token.config";
        public const string BadEndpoint = "https://bad.endpoint:1234";
        public const string BadUri = "https:/bad.uri:1234";
        public const string BadToken = "1234567890";

        public string Endpoint { get; private set; }
        public string Token { get; private set; }
        
        public void SetUp()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo(Log4NetConfigFilePath));

            MetricsLogzioReporterBuilder.GetLogzioConnection(LogzioConfigFilePath, out var endpoint, out var token);
            Endpoint = endpoint;
            Token = token;
        }
        
        public void AddGaugeMetricToMetricsBuilder(IMetricsRoot metrics)
        {
            var gauge = new GaugeOptions { Name = "gauge_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Gauge.SetValue(gauge, 25);
        }
        
        public void AddCounterMetricToMetricsBuilder(IMetricsRoot metrics)
        {
            var counter = new CounterOptions { Name = "counter_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Counter.Increment(counter);
        }
        
        public void AddMeterMetricToMetricsBuilder(IMetricsRoot metrics)
        {
            var meter = new MeterOptions { Name = "meter_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Meter.Mark(meter, 10);
        }
        
        public void AddHistogramMetricToMetricsBuilder(IMetricsRoot metrics)
        {
            var histogram = new HistogramOptions { Name = "histogram_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Histogram.Update(histogram, 25);
        }
        
        public void AddTimerMetricToMetricsBuilder(IMetricsRoot metrics)
        {
            var timer = new TimerOptions { Name = "timer_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Timer.Time(timer, () => {});
        }
        
        public void AddApdexMetricToMetricsBuilder(IMetricsRoot metrics)
        {
            var apdex = new ApdexOptions {Name = "apdex_test", Tags = new MetricTags("test", "test")};
            metrics.Measure.Apdex.Track(apdex);
        }

        public async Task<bool> SendMetricsToLogzio(IMetricsRoot metrics)
        {
            return await metrics.Reporters.First().FlushAsync(metrics.Snapshot.Get());
        }
    }
}