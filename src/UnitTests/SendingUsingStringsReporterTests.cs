using App.Metrics;
using Core.Reporter;
using NUnit.Framework;

namespace UnitTests
{
    public class SendingUsingStringsReporterTests
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
                .Report.ToLogzioHttp(_testsUtils.Endpoint, _testsUtils.Token)
                .Build();
        }
        
        [Test]
        public void Send_BadEndpoint_Fail()
        {
            _metrics = new MetricsBuilder()
                .Report.ToLogzioHttp(TestsUtils.BadEndpoint, _testsUtils.Token)
                .Build();
            
            _testsUtils.AddGaugeMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(false,
                _testsUtils.SendMetricsToLogzio(_metrics).Result);
        }

        [Test]
        public void Send_BadToken_Fail()
        {
            _metrics = new MetricsBuilder()
                .Report.ToLogzioHttp(_testsUtils.Endpoint, TestsUtils.BadToken)
                .Build();
            
            _testsUtils.AddGaugeMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(false,
                _testsUtils.SendMetricsToLogzio(_metrics).Result);
        }

        [Test]
        public void Send_CounterMetric_Success()
        {
            _testsUtils.AddCounterMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(true,
                _testsUtils.SendMetricsToLogzio(_metrics).Result);
        }

        [Test]
        public void Send_MeterMetric_Success()
        {
            _testsUtils.AddMeterMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(true,
                _testsUtils.SendMetricsToLogzio(_metrics).Result);
        }

        [Test]
        public void Send_HistogramMetric_Success()
        {
            _testsUtils.AddHistogramMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(true,
                _testsUtils.SendMetricsToLogzio(_metrics).Result);
        }

        [Test]
        public void Send_TimerMetric_Success()
        {
            _testsUtils.AddTimerMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(true,
                _testsUtils.SendMetricsToLogzio(_metrics).Result);
        }

        [Test]
        public void Send_ApdexMetric_Success()
        {
            _testsUtils.AddApdexMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(true,
                _testsUtils.SendMetricsToLogzio(_metrics).Result);
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
                _testsUtils.SendMetricsToLogzio(_metrics).Result);
        }
    }
}