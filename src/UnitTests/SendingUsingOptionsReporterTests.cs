using System;
using App.Metrics;
using Core.Reporter;
using NUnit.Framework;

namespace UnitTests
{
    public class SendingUsingOptionsReporterTests
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
            var options = new MetricsReportingLogzioOptions
            {
                Logzio = {EndpointUri = new Uri(_testsUtils.Endpoint), Token = _testsUtils.Token},
                FlushInterval = TimeSpan.FromSeconds(15),
                HttpPolicy =
                {
                    BackoffPeriod = TimeSpan.FromSeconds(30),
                    FailuresBeforeBackoff = 5,
                    Timeout = TimeSpan.FromSeconds(10)
                }
            };
                
            _metrics = new MetricsBuilder()
                .Report.ToLogzioHttp(options)
                .Build();
        }
        
        [Test]
        public void Send_BadEndpoint_Fail()
        {
            var options = new MetricsReportingLogzioOptions
            {
                Logzio = {EndpointUri = new Uri(TestsUtils.BadEndpoint), Token = _testsUtils.Token},
                FlushInterval = TimeSpan.FromSeconds(15),
                HttpPolicy =
                {
                    BackoffPeriod = TimeSpan.FromSeconds(30),
                    FailuresBeforeBackoff = 5,
                    Timeout = TimeSpan.FromSeconds(10)
                }
            };
                
            _metrics = new MetricsBuilder()
                .Report.ToLogzioHttp(options)
                .Build();
            
            _testsUtils.AddGaugeMetricToMetricsBuilder(_metrics);

            Assert.AreEqual(false,
                _testsUtils.SendMetricsToLogzio(_metrics).Result);
        }

        [Test]
        public void Send_BadToken_Fail()
        {
            var options = new MetricsReportingLogzioOptions
            {
                Logzio = {EndpointUri = new Uri(_testsUtils.Endpoint), Token = TestsUtils.BadToken},
                FlushInterval = TimeSpan.FromSeconds(15),
                HttpPolicy =
                {
                    BackoffPeriod = TimeSpan.FromSeconds(30),
                    FailuresBeforeBackoff = 5,
                    Timeout = TimeSpan.FromSeconds(10)
                }
            };
                
            _metrics = new MetricsBuilder()
                .Report.ToLogzioHttp(options)
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