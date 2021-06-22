using System;
using App.Metrics;
using Core.Reporter;
using NUnit.Framework;

namespace UnitTests
{
    public class CreatingMetricsBuilderTests
    {
        private readonly TestsUtils _testsUtils = new TestsUtils();

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testsUtils.SetUp();
        }

        [Test]
        public void Create_WithReportOptions_Success()
        {
            try
            {
                var metrics = new MetricsBuilder()
                    .Report.ToLogzioHttp(options =>
                    {
                        options.Logzio.EndpointUri = new Uri(_testsUtils.Endpoint);
                        options.Logzio.Token = _testsUtils.Token;
                        options.FlushInterval = TimeSpan.FromSeconds(15);
                        options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                        options.HttpPolicy.FailuresBeforeBackoff = 5;
                        options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                    })
                    .Build();

                _testsUtils.AddGaugeMetricToMetricsBuilder(metrics);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.Pass();
        }

        [Test]
        public void Create_WithReportConfig_Success()
        {
            try
            {
                var metrics = new MetricsBuilder()
                    .Report.ToLogzioHttp(TestsUtils.LogzioConfigFilePath)
                    .Build();

                _testsUtils.AddGaugeMetricToMetricsBuilder(metrics);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.Pass();
        }

        [Test]
        public void Create_WithReportConfigWithTimespan_Success()
        {
            try
            {
                var metrics = new MetricsBuilder()
                    .Report.ToLogzioHttp(TestsUtils.LogzioConfigFilePath, TimeSpan.FromMilliseconds(20))
                    .Build();

                _testsUtils.AddGaugeMetricToMetricsBuilder(metrics);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.Pass();
        }
        
        [Test]
        public void Create_WithReportStrings_Success()
        {
            try
            {
                var metrics = new MetricsBuilder()
                    .Report.ToLogzioHttp(_testsUtils.Endpoint, _testsUtils.Token)
                    .Build();

                _testsUtils.AddGaugeMetricToMetricsBuilder(metrics);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.Pass();
        }
        
        [Test]
        public void Create_WithReportStringsAndTimespan_Success()
        {
            try
            {
                var metrics = new MetricsBuilder()
                    .Report.ToLogzioHttp(_testsUtils.Endpoint, _testsUtils.Token, TimeSpan.FromMilliseconds(25))
                    .Build();

                _testsUtils.AddGaugeMetricToMetricsBuilder(metrics);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.Pass();
        }
    }
}