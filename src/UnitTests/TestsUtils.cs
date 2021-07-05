using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using App.Metrics;
using App.Metrics.Apdex;
using App.Metrics.Counter;
using App.Metrics.Gauge;
using App.Metrics.Histogram;
using App.Metrics.Meter;
using App.Metrics.Timer;
using Core.Client;
using Core.Reporter;
using log4net;
using log4net.Config;

namespace UnitTests
{
    public class TestsUtils
    {
        private const string Log4NetConfigFilePath = "log4net.config";
     
        public const string LogzioConfigFilePath = "logzio.config";
        public const string LogzioBadConfigFilePath = "bad_logzio.config";
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

        public bool SendSnapshotToLogzio(MetricsDataValueSource snapshot, string endpoint, string token)
        {
            var logzioMetricsReporter = new LogzioMetricsReporter(new MetricsReportingLogzioOptions(),
                new DefaultLogzioHttpClient(new HttpClient(), new LogzioOptions(new Uri(endpoint), token), new HttpPolicy()));
        
            return logzioMetricsReporter.FlushAsync(snapshot).Result;
        }
    }
}