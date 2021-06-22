using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Apdex;
using App.Metrics.Filtering;
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

        //private IMetricsRoot _metrics;  
        private string _endpoint;
        private string _token;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo(Log4NetConfigFilePath));
            
            MetricsLogzioReporterBuilder.GetLogzioConnection(LogzioConfigFilePath, out _endpoint, out _token);
        }
        
        // [SetUp]
        // public void Setup()
        // {
        //     _metrics = new MetricsBuilder()
        //         .Report.ToLogzioHttp(LogzioConfigFilePath)
        //         .Report.ToLogzioHttp(_endpoint, _token)
        //         .Report.ToLogzioHttp(_endpoint, _token, TimeSpan.FromSeconds(15))
        //         .Build();
        // }

        [Test]
        public void SendUsingReportOptions_GaugeMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportOptionsOption();
            
            AddGaugeMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportOptions_CounterMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportOptionsOption();
            
            AddCounterMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportOptions_MeterMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportOptionsOption();
            
            AddMeterMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportOptions_HistogramMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportOptionsOption();
            
            AddHistogramMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportOptions_TimerMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportOptionsOption();
            
            AddTimerMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }

        [Test]
        public void SendUsingReportOptions_ApdexMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportOptionsOption();
            
            AddApdexMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }

        [Test]
        public void SendUsingReportOptions_AllMetricTypes_Success()
        {
            var metrics = GetMetricsBuilderWithReportOptionsOption();
            
            AddGaugeMetricToMetricsBuilder(metrics);
            AddCounterMetricToMetricsBuilder(metrics);
            AddMeterMetricToMetricsBuilder(metrics);
            AddHistogramMetricToMetricsBuilder(metrics);
            AddTimerMetricToMetricsBuilder(metrics);
            AddApdexMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportConfig_GaugeMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportConfigOption();
            
            AddGaugeMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportConfig_CounterMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportConfigOption();
            
            AddCounterMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportConfig_MeterMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportConfigOption();
            
            AddMeterMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportConfig_HistogramMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportConfigOption();
            
            AddHistogramMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportConfig_TimerMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportConfigOption();
            
            AddTimerMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }

        [Test]
        public void SendUsingReportConfig_ApdexMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportConfigOption();
            
            AddApdexMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }

        [Test]
        public void SendUsingReportConfig_AllMetricTypes_Success()
        {
            var metrics = GetMetricsBuilderWithReportConfigOption();
            
            AddGaugeMetricToMetricsBuilder(metrics);
            AddCounterMetricToMetricsBuilder(metrics);
            AddMeterMetricToMetricsBuilder(metrics);
            AddHistogramMetricToMetricsBuilder(metrics);
            AddTimerMetricToMetricsBuilder(metrics);
            AddApdexMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportConfigAndTimespan_GaugeMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportConfigAndTimespanOption();
            
            AddGaugeMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportConfigAndTimespan_CounterMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportConfigAndTimespanOption();
            
            AddCounterMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportConfigAndTimespan_MeterMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportConfigAndTimespanOption();
            
            AddMeterMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportConfigAndTimespan_HistogramMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportConfigAndTimespanOption();
            
            AddHistogramMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportConfigAndTimespan_TimerMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportConfigAndTimespanOption();
            
            AddTimerMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }

        [Test]
        public void SendUsingReportConfigAndTimespan_ApdexMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportConfigAndTimespanOption();
            
            AddApdexMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }

        [Test]
        public void SendUsingReportConfigAndTimespan_AllMetricTypes_Success()
        {
            var metrics = GetMetricsBuilderWithReportConfigAndTimespanOption();
            
            AddGaugeMetricToMetricsBuilder(metrics);
            AddCounterMetricToMetricsBuilder(metrics);
            AddMeterMetricToMetricsBuilder(metrics);
            AddHistogramMetricToMetricsBuilder(metrics);
            AddTimerMetricToMetricsBuilder(metrics);
            AddApdexMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportStrings_GaugeMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportStringsOption();
            
            AddGaugeMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportStrings_CounterMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportStringsOption();
            
            AddCounterMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportStrings_MeterMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportStringsOption();
            
            AddMeterMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportStrings_HistogramMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportStringsOption();
            
            AddHistogramMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportStrings_TimerMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportStringsOption();
            
            AddTimerMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }

        [Test]
        public void SendUsingReportStrings_ApdexMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportStringsOption();
            
            AddApdexMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }

        [Test]
        public void SendUsingReportStrings_AllMetricTypes_Success()
        {
            var metrics = GetMetricsBuilderWithReportStringsOption();
            
            AddGaugeMetricToMetricsBuilder(metrics);
            AddCounterMetricToMetricsBuilder(metrics);
            AddMeterMetricToMetricsBuilder(metrics);
            AddHistogramMetricToMetricsBuilder(metrics);
            AddTimerMetricToMetricsBuilder(metrics);
            AddApdexMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportStringsAndTimespan_GaugeMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportStringsAndTimespanOption();
            
            AddGaugeMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportStringsAndTimespan_CounterMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportStringsAndTimespanOption();
            
            AddCounterMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportStringsAndTimespan_MeterMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportStringsAndTimespanOption();
            
            AddMeterMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportStringsAndTimespan_HistogramMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportStringsAndTimespanOption();
            
            AddHistogramMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }
        
        [Test]
        public void SendUsingReportStringsAndTimespan_TimerMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportStringsAndTimespanOption();
            
            AddTimerMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }

        [Test]
        public void SendUsingReportStringsAndTimespan_ApdexMetric_Success()
        {
            var metrics = GetMetricsBuilderWithReportStringsAndTimespanOption();
            
            AddApdexMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }

        [Test]
        public void SendUsingReportStringsAndTimespan_AllMetricTypes_Success()
        {
            var metrics = GetMetricsBuilderWithReportStringsAndTimespanOption();
            
            AddGaugeMetricToMetricsBuilder(metrics);
            AddCounterMetricToMetricsBuilder(metrics);
            AddMeterMetricToMetricsBuilder(metrics);
            AddHistogramMetricToMetricsBuilder(metrics);
            AddTimerMetricToMetricsBuilder(metrics);
            AddApdexMetricToMetricsBuilder(metrics);
            
            Assert.AreEqual(true, SendSnapshotToLogzio(metrics.Snapshot.Get()));
        }

        // [Test]
        // public void Check()
        // {
        //     var runReportsTask = Task.WhenAll(_metrics.ReportRunner.RunAllAsync());
        //
        //     if (runReportsTask.Wait(15000) && runReportsTask.Status == TaskStatus.RanToCompletion)
        //     {
        //         Assert.Pass();
        //     }
        //     
        //     Assert.Fail();
        // }
        
        private IMetricsRoot GetMetricsBuilderWithReportOptionsOption()
        {
            return new MetricsBuilder()
                .Report.ToLogzioHttp(options =>
                {
                    options.Logzio.EndpointUri = new Uri(_endpoint);
                    options.Logzio.Token = _token;
                    options.FlushInterval = TimeSpan.FromSeconds(15);
                    options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                    options.HttpPolicy.FailuresBeforeBackoff = 5;
                    options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                })
                .Build();
        }
        
        private IMetricsRoot GetMetricsBuilderWithReportConfigOption()
        {
            return new MetricsBuilder()
                .Report.ToLogzioHttp(LogzioConfigFilePath)
                .Build();
        }
        
        private IMetricsRoot GetMetricsBuilderWithReportConfigAndTimespanOption()
        {
            return new MetricsBuilder()
                .Report.ToLogzioHttp(LogzioConfigFilePath, TimeSpan.FromMilliseconds(20))
                .Build();
        }
        
        private IMetricsRoot GetMetricsBuilderWithReportStringsOption()
        {
            return new MetricsBuilder()
                .Report.ToLogzioHttp(_endpoint, _token)
                .Build();
        }
        
        private IMetricsRoot GetMetricsBuilderWithReportStringsAndTimespanOption()
        {
            return new MetricsBuilder()
                .Report.ToLogzioHttp(_endpoint, _token, TimeSpan.FromMilliseconds(25))
                .Build();
        }

        private void AddGaugeMetricToMetricsBuilder(IMetricsRoot metrics)
        {
            var gauge = new GaugeOptions { Name = "gauge_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Gauge.SetValue(gauge, 25);
        }
        
        private void AddCounterMetricToMetricsBuilder(IMetricsRoot metrics)
        {
            var counter = new CounterOptions { Name = "counter_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Counter.Increment(counter);
        }
        
        private void AddMeterMetricToMetricsBuilder(IMetricsRoot metrics)
        {
            var meter = new MeterOptions { Name = "meter_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Meter.Mark(meter, 10);
        }
        
        private void AddHistogramMetricToMetricsBuilder(IMetricsRoot metrics)
        {
            var histogram = new HistogramOptions { Name = "histogram_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Histogram.Update(histogram, 25);
        }
        
        private void AddTimerMetricToMetricsBuilder(IMetricsRoot metrics)
        {
            var timer = new TimerOptions { Name = "timer_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Timer.Time(timer, () => {});
        }
        
        private void AddApdexMetricToMetricsBuilder(IMetricsRoot metrics)
        {
            var apdex = new ApdexOptions {Name = "apdex_test", Tags = new MetricTags("test", "test")};
            metrics.Measure.Apdex.Track(apdex);
        }

        private bool SendSnapshotToLogzio(MetricsDataValueSource snapshot)
        {
            var logzioMetricsReporter = new LogzioMetricsReporter(new MetricsReportingLogzioOptions(),
                new DefaultLogzioHttpClient(new HttpClient(), new LogzioOptions(new Uri(_endpoint), _token), new HttpPolicy()));

            return logzioMetricsReporter.FlushAsync(snapshot).Result;
        }
    }
}