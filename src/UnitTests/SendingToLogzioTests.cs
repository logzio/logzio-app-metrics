using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Apdex;
using App.Metrics.Gauge;
using App.Metrics.Histogram;
using App.Metrics.Meter;
using App.Metrics.Timer;
using Core.Client;
using Core.Reporter;
using log4net;
using log4net.Config;
using NUnit.Framework;

namespace UnitTests
{
    public class SendingToLogzioTests
    {
        private const string LogzioConfigFilePath = "logzio.config";
        private const string Log4NetConfigFilePath = "log4net.config";

        private IMetricsRoot _metrics;  
        private string _endpoint;
        private string _token;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo(Log4NetConfigFilePath));
            
            MetricsLogzioReporterBuilder.GetLogzioConnection(LogzioConfigFilePath, out _endpoint, out _token);
        }
        
        [SetUp]
        public void Setup()
        {
            _metrics = new MetricsBuilder()
                .Report.ToLogzioHttp(LogzioConfigFilePath)
                .Report.ToLogzioHttp(_endpoint, _token)
                .Report.ToLogzioHttp(_endpoint, _token, TimeSpan.FromSeconds(15))
                .Build();
        }

        [Test]
        public void Send_GaugeMetric_Success()
        {
            var gauge = new GaugeOptions { Name = "gauge_test", Tags = new MetricTags("test", "test") };
            _metrics.Measure.Gauge.SetValue(gauge, 25);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(_metrics.Snapshot.Get()));
        }
        
        [Test]
        public void Send_CounterMetric_Success()
        {
            var counter = new CounterOptions { Name = "counter_test", Tags = new MetricTags("test", "test") };
            _metrics.Measure.Counter.Increment(counter);
        
            Assert.AreEqual(true, SendSnapshotToLogzio(_metrics.Snapshot.Get()));
        }
        
        [Test]
        public void Send_MeterMetric_Success()
        {
            var meter = new MeterOptions { Name = "meter_test", Tags = new MetricTags("test", "test") };
            _metrics.Measure.Meter.Mark(meter, 10);
        
            Assert.AreEqual(true, SendSnapshotToLogzio(_metrics.Snapshot.Get()));
        }
        
        [Test]
        public void Send_HistogramMetric_Success()
        {
            var histogram = new HistogramOptions { Name = "histogram_test", Tags = new MetricTags("test", "test") };
            _metrics.Measure.Histogram.Update(histogram, 25);
        
            Assert.AreEqual(true, SendSnapshotToLogzio(_metrics.Snapshot.Get()));
        }
        
        [Test]
        public void Send_TimerMetric_Success()
        {
            var timer = new TimerOptions { Name = "timer_test", Tags = new MetricTags("test", "test") };
            _metrics.Measure.Timer.Time(timer, () => {});
        
            Assert.AreEqual(true, SendSnapshotToLogzio(_metrics.Snapshot.Get()));
        }

        [Test]
        public void Send_ApdexMetric_Success()
        {
            var apdex = new ApdexOptions {Name = "apdex_test", Tags = new MetricTags("test", "test")};
            _metrics.Measure.Apdex.Track(apdex);

            Assert.AreEqual(true, SendSnapshotToLogzio(_metrics.Snapshot.Get()));
        }

        [Test]
        public void Send_AllMetricTypes_Success()
        {
            var gauge = new GaugeOptions { Name = "gauge_test", Tags = new MetricTags("test", "test") };
            _metrics.Measure.Gauge.SetValue(gauge, 25);
            
            var counter = new CounterOptions { Name = "counter_test", Tags = new MetricTags("test", "test") };
            _metrics.Measure.Counter.Increment(counter);
            
            var meter = new MeterOptions { Name = "meter_test", Tags = new MetricTags("test", "test") };
            _metrics.Measure.Meter.Mark(meter, 10);
            
            var histogram = new HistogramOptions { Name = "histogram_test", Tags = new MetricTags("test", "test") };
            _metrics.Measure.Histogram.Update(histogram, 25);
            
            var timer = new TimerOptions { Name = "timer_test", Tags = new MetricTags("test", "test") };
            _metrics.Measure.Timer.Time(timer, () => {});
            
            var apdex = new ApdexOptions {Name = "apdex_test", Tags = new MetricTags("test", "test")};
            _metrics.Measure.Apdex.Track(apdex);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(_metrics.Snapshot.Get()));
        }

        [Test]
        public void Check()
        {
            var runReportsTask = Task.WhenAll(_metrics.ReportRunner.RunAllAsync());

            if (runReportsTask.Wait(15000) && runReportsTask.Status == TaskStatus.RanToCompletion)
            {
                Assert.Pass();
            }
            
            Assert.Fail();
        }

        private bool SendSnapshotToLogzio(MetricsDataValueSource snapshot)
        {
            var logzioMetricsReporter = new LogzioMetricsReporter(new MetricsReportingLogzioOptions(),
                new DefaultLogzioHttpClient(new HttpClient(), new LogzioOptions(new Uri(_endpoint), _token), new HttpPolicy()));

            return logzioMetricsReporter.FlushAsync(snapshot).Result;
        }
    }
}