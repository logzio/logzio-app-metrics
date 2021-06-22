using App.Metrics;
using Core.Reporter;
using NUnit.Framework;

namespace UnitTests
{
    public class SendingToLogzioTests
    {
        private readonly TestsUtils _testsUtils = new TestsUtils();
        private IMetricsRoot _metrics;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testsUtils.SetUp();
        }

        [SetUp]
        public void Setup()
        {
            _metrics = new MetricsBuilder()
                .Report.ToLogzioHttp(TestsUtils.LogzioConfigFilePath)
                .Build();
        }

        [Test]
        public void Send_BadEndpoint_Fail()
        {
            _testsUtils.AddGaugeMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(false,
                _testsUtils.SendSnapshotToLogzio(_metrics.Snapshot.Get(), TestsUtils.BadEndpoint, _testsUtils.Token));
        }

        [Test]
        public void Send_BadToken_Fail()
        {
            _testsUtils.AddGaugeMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(false,
                _testsUtils.SendSnapshotToLogzio(_metrics.Snapshot.Get(), _testsUtils.Endpoint, TestsUtils.BadToken));
        }

        [Test]
        public void Send_CounterMetric_Success()
        {
            _testsUtils.AddCounterMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(true,
                _testsUtils.SendSnapshotToLogzio(_metrics.Snapshot.Get(), _testsUtils.Endpoint, _testsUtils.Token));
        }

        [Test]
        public void Send_MeterMetric_Success()
        {
            _testsUtils.AddMeterMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(true,
                _testsUtils.SendSnapshotToLogzio(_metrics.Snapshot.Get(), _testsUtils.Endpoint, _testsUtils.Token));
        }

        [Test]
        public void Send_HistogramMetric_Success()
        {
            _testsUtils.AddHistogramMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(true,
                _testsUtils.SendSnapshotToLogzio(_metrics.Snapshot.Get(), _testsUtils.Endpoint, _testsUtils.Token));
        }

        [Test]
        public void Send_TimerMetric_Success()
        {
            _testsUtils.AddTimerMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(true,
                _testsUtils.SendSnapshotToLogzio(_metrics.Snapshot.Get(), _testsUtils.Endpoint, _testsUtils.Token));
        }

        [Test]
        public void Send_ApdexMetric_Success()
        {
            _testsUtils.AddApdexMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(true,
                _testsUtils.SendSnapshotToLogzio(_metrics.Snapshot.Get(), _testsUtils.Endpoint, _testsUtils.Token));
        }

        [Test]
        public void Send_AllMetricTypes_Success()
        {
            _testsUtils.AddGaugeMetricToMetricsBuilder(_metrics);
            _testsUtils.AddCounterMetricToMetricsBuilder(_metrics);
            _testsUtils.AddMeterMetricToMetricsBuilder(_metrics);
            _testsUtils.AddHistogramMetricToMetricsBuilder(_metrics);
            _testsUtils.AddTimerMetricToMetricsBuilder(_metrics);
            _testsUtils.AddApdexMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(true,
                _testsUtils.SendSnapshotToLogzio(_metrics.Snapshot.Get(), _testsUtils.Endpoint, _testsUtils.Token));
        }
    }
}