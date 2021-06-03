using System;
using System.Net.Http;
using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Apdex;
using App.Metrics.Gauge;
using App.Metrics.Histogram;
using App.Metrics.Meter;
using App.Metrics.Timer;
using Core.Client;
using Core.Reporter;
using NUnit.Framework;

namespace UnitTests
{
    public class SendingToLogzioTests
    {
        private const string LogzioConfigFilePath = "logzio.config";

        private string _endpoint;
        private string _token;

        [SetUp]
        public void Setup()
        {
            MetricsLogzioReporterBuilder.GetLogzioConnection(LogzioConfigFilePath, out _endpoint, out _token);
        }

        [Test]
        public void Send_GaugeMetric_Success()
        {
            var metrics = CreateMetricsBuilder();
            
            var gauge = new GaugeOptions { Name = "gauge_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Gauge.SetValue(gauge, 25);
            
            if (SendSnapshotToLogzio(metrics.Snapshot.Get()))
            {
                Assert.Pass();
                return;
            }
        
            Assert.Fail();
        }
        
        [Test]
        public void Send_CounterMetric_Success()
        {
            var metrics = CreateMetricsBuilder();
            
            var counter = new CounterOptions { Name = "counter_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Counter.Increment(counter);
        
            if (SendSnapshotToLogzio(metrics.Snapshot.Get()))
            {
                Assert.Pass();
                return;
            }
        
            Assert.Fail();
        }
        
        [Test]
        public void Send_MeterMetric_Success()
        {
            var metrics = CreateMetricsBuilder();
            
            var meter = new MeterOptions { Name = "meter_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Meter.Mark(meter, 10);
        
            if (SendSnapshotToLogzio(metrics.Snapshot.Get()))
            {
                Assert.Pass();
                return;
            }
        
            Assert.Fail();
        }
        
        [Test]
        public void Send_HistogramMetric_Success()
        {
            var metrics = CreateMetricsBuilder();
            
            var histogram = new HistogramOptions { Name = "histogram_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Histogram.Update(histogram, 25);
        
            if (SendSnapshotToLogzio(metrics.Snapshot.Get()))
            {
                Assert.Pass();
                return;
            }
        
            Assert.Fail();
        }
        
        [Test]
        public void Send_TimerMetric_Success()
        {
            var metrics = CreateMetricsBuilder();
            
            var timer = new TimerOptions { Name = "timer_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Timer.Time(timer, () => {});
        
            if (SendSnapshotToLogzio(metrics.Snapshot.Get()))
            {
                Assert.Pass();
                return;
            }
        
            Assert.Fail();
        }
        
        [Test]
        public void Send_ApdexMetric_Success()
        {
            var metrics = CreateMetricsBuilder();
            
            var apdex = new ApdexOptions { Name = "apdex_test", Tags = new MetricTags("test", "test") };
            metrics.Measure.Apdex.Track(apdex);
        
            if (SendSnapshotToLogzio(metrics.Snapshot.Get()))
            {
                Assert.Pass();
                return;
            }
        
            Assert.Fail();
        }

        private IMetricsRoot CreateMetricsBuilder()
        {
            return new MetricsBuilder()
                .Report.ToLogzioHttp(LogzioConfigFilePath)
                .Build();
        }

        private bool SendSnapshotToLogzio(MetricsDataValueSource snapshot)
        {
            var logzioMetricsReporter = new LogzioMetricsReporter(new MetricsReportingLogzioOptions(),
                new DefaultLogzioHttpClient(new HttpClient(), new LogzioOptions(new Uri(_endpoint), _token), new HttpPolicy()));

            return logzioMetricsReporter.FlushAsync(snapshot).Result;
        }
    }
}