using System.IO;
using System.Threading.Tasks;
using App.Metrics;
using Core.Formatter;
using NUnit.Framework;

namespace UnitTests
{
    public class FormattingTests
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
                .OutputMetrics.AsLogzioCompressedProtobuf()
                .Build();
        }

        [Test]
        public async Task Format_Snapshot_Success()
        {
            _testsUtils.AddGaugeMetricToMetricsBuilder(_metrics);
            _testsUtils.AddCounterMetricToMetricsBuilder(_metrics);
            _testsUtils.AddMeterMetricToMetricsBuilder(_metrics);
            _testsUtils.AddHistogramMetricToMetricsBuilder(_metrics);
            _testsUtils.AddTimerMetricToMetricsBuilder(_metrics);
            _testsUtils.AddApdexMetricToMetricsBuilder(_metrics);

            var snapshot = _metrics.Snapshot.Get();
            
            using (var stream = new MemoryStream())
            {
                await _metrics.DefaultOutputMetricsFormatter.WriteAsync(stream, snapshot);
                
                Assert.IsTrue(stream.Length > 0, "The stream is empty. Formatting went wrong...");
            }
        }
    }
}