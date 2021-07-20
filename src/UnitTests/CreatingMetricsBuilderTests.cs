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
        public void Create_WithOptionsReportNullOptions_Fail()
        {
            try
            {
                MetricsReportingLogzioOptions options = null;
                
                new MetricsBuilder()
                    .Report.ToLogzioHttp(options)
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }

        [Test]
        public void Create_WithOptionsReporterNoLogzioOptionsData_Fail()
        {
            try
            {
                var options = new MetricsReportingLogzioOptions
                {
                    FlushInterval = TimeSpan.FromSeconds(15),
                    HttpPolicy =
                    {
                        BackoffPeriod = TimeSpan.FromSeconds(30),
                        FailuresBeforeBackoff = 5,
                        Timeout = TimeSpan.FromSeconds(10)
                    }
                };
                
                new MetricsBuilder()
                    .Report.ToLogzioHttp(options)
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }
        
        [Test]
        public void Create_WithOptionsReporterNullEndpointUri_Fail()
        {
            try
            {
                var options = new MetricsReportingLogzioOptions
                {
                    Logzio = { EndpointUri = null, Token = _testsUtils.Token },
                    FlushInterval = TimeSpan.FromSeconds(15),
                    HttpPolicy =
                    {
                        BackoffPeriod = TimeSpan.FromSeconds(30),
                        FailuresBeforeBackoff = 5,
                        Timeout = TimeSpan.FromSeconds(10)
                    }
                };
                
                new MetricsBuilder()
                    .Report.ToLogzioHttp(options)
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }
        
        [Test]
        public void Create_WithOptionsReporterEmptyToken_Fail()
        {
            try
            {
                var options = new MetricsReportingLogzioOptions
                {
                    Logzio = { EndpointUri = new Uri(_testsUtils.Endpoint), Token = String.Empty },
                    FlushInterval = TimeSpan.FromSeconds(15),
                    HttpPolicy =
                    {
                        BackoffPeriod = TimeSpan.FromSeconds(30),
                        FailuresBeforeBackoff = 5,
                        Timeout = TimeSpan.FromSeconds(10)
                    }
                };
                
                new MetricsBuilder()
                    .Report.ToLogzioHttp(options)
                    .Build();
                
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }

        [Test]
        public void Create_WithActionOptionsReporterNoLogzioEndpointUri_Fail()
        {
            try
            {
                new MetricsBuilder()
                    .Report.ToLogzioHttp(options =>
                    {
                        options.Logzio.Token = _testsUtils.Token;
                        options.FlushInterval = TimeSpan.FromSeconds(15);
                        options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                        options.HttpPolicy.FailuresBeforeBackoff = 5;
                        options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                    })
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }
        
        [Test]
        public void Create_WithActionOptionsReporterNoLogzioToken_Fail()
        {
            try
            {
                new MetricsBuilder()
                    .Report.ToLogzioHttp(options =>
                    {
                        options.Logzio.EndpointUri = new Uri(_testsUtils.Endpoint);
                        options.FlushInterval = TimeSpan.FromSeconds(15);
                        options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                        options.HttpPolicy.FailuresBeforeBackoff = 5;
                        options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                    })
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }
        
        [Test]
        public void Create_WithActionOptionsReporterNullEndpointUri_Fail()
        {
            try
            {
                new MetricsBuilder()
                    .Report.ToLogzioHttp(options =>
                    {
                        options.Logzio.EndpointUri = null;
                        options.Logzio.Token = _testsUtils.Token;
                        options.FlushInterval = TimeSpan.FromSeconds(15);
                        options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                        options.HttpPolicy.FailuresBeforeBackoff = 5;
                        options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                    })
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }
        
        [Test]
        public void Create_WithActionOptionsReporterEmptyToken_Fail()
        {
            try
            {
                new MetricsBuilder()
                    .Report.ToLogzioHttp(options =>
                    {
                        options.Logzio.Token = String.Empty;
                        options.Logzio.Token = _testsUtils.Token;
                        options.FlushInterval = TimeSpan.FromSeconds(15);
                        options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                        options.HttpPolicy.FailuresBeforeBackoff = 5;
                        options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                    })
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }

        [Test]
        public void Create_WithStringsReporterBadUri_Fail()
        {
            try
            {
                new MetricsBuilder()
                    .Report.ToLogzioHttp(TestsUtils.BadUri, _testsUtils.Token)
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }
        
        [Test]
        public void Create_WithStringsReporterEmptyToken_Fail()
        {
            try
            {
                new MetricsBuilder()
                    .Report.ToLogzioHttp(_testsUtils.Endpoint, String.Empty)
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }
        
        [Test]
        public void Create_WithStringsAndTimespanReporterBadUri_Fail()
        {
            try
            {
                new MetricsBuilder()
                    .Report.ToLogzioHttp(TestsUtils.BadUri, _testsUtils.Token, TimeSpan.FromMilliseconds(25))
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }
        
        [Test]
        public void Create_WithStringsAndTimespanReporterEmptyToken_Fail()
        {
            try
            {
                new MetricsBuilder()
                    .Report.ToLogzioHttp(_testsUtils.Endpoint, String.Empty, TimeSpan.FromMilliseconds(25))
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }

        [Test]
        public void Create_WithConfigReporterFileNotExist_Fail()
        {
            try
            {
                new MetricsBuilder()
                    .Report.ToLogzioHttp(TestsUtils.LogzioConfigFileBadPath)
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }
        
        [Test]
        public void Create_WithConfigReporterBadFormat_Fail()
        {
            try
            {
                new MetricsBuilder()
                    .Report.ToLogzioHttp(TestsUtils.LogzioConfigFileBadFormatPath)
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }
        
        [Test]
        public void Create_WithConfigReporterBadUri_Fail()
        {
            try
            {
                new MetricsBuilder()
                    .Report.ToLogzioHttp(TestsUtils.LogzioConfigFileBadUriPath)
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }

        [Test]
        public void Create_WithConfigAndTimespanReporterFileNotExist_Fail()
        {
            try
            {
                new MetricsBuilder()
                    .Report.ToLogzioHttp(TestsUtils.LogzioConfigFileBadPath, TimeSpan.FromMilliseconds(20))
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }
        
        [Test]
        public void Create_WithConfigAndTimespanReporterBadFormat_Fail()
        {
            try
            {
                new MetricsBuilder()
                    .Report.ToLogzioHttp(TestsUtils.LogzioConfigFileBadFormatPath, TimeSpan.FromMilliseconds(20))
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }
        
        [Test]
        public void Create_WithConfigAndTimespanReporterBadUri_Fail()
        {
            try
            {
                new MetricsBuilder()
                    .Report.ToLogzioHttp(TestsUtils.LogzioConfigFileBadUriPath, TimeSpan.FromMilliseconds(20))
                    .Build();
            }
            catch (Exception e)
            {
                Assert.Pass(e.Message);
            }

            Assert.Fail();
        }
    }
}