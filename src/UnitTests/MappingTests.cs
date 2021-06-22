using System.Linq;
using App.Metrics;
using App.Metrics.Apdex;
using App.Metrics.Counter;
using App.Metrics.Gauge;
using App.Metrics.Histogram;
using App.Metrics.Meter;
using App.Metrics.Timer;
using Core.Mapper;
using Core.Reporter;
using NUnit.Framework;

namespace UnitTests
{
    public class MappingTests
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
        public void Map_GaugeMetric_TimesSeriesInNotEmpty()
        {
            _testsUtils.AddGaugeMetricToMetricsBuilder(_metrics);

            var context = _metrics.Snapshot.Get().Contexts.First();
            var gauge = context.Gauges.First();

            MetricTimeSeriesMapper<GaugeValueSource, double> metricTimeSeriesMapper =
                new GaugeMetricTimeSeriesMapper(gauge, context.Context);

            var timesSeries = metricTimeSeriesMapper.CreateTimesSeries();
            
            Assert.IsNotEmpty(timesSeries, "Gauge mapping went wrong...");
        }
        
        [Test]
        public void Map_CounterMetric_TimesSeriesInNotEmpty()
        {
            _testsUtils.AddCounterMetricToMetricsBuilder(_metrics);

            var context = _metrics.Snapshot.Get().Contexts.First();
            var counter = context.Counters.First();

            MetricTimeSeriesMapper<CounterValueSource, CounterValue> metricTimeSeriesMapper =
                new CounterMetricTimeSeriesMapper(counter, context.Context);

            var timesSeries = metricTimeSeriesMapper.CreateTimesSeries();
            
            Assert.IsNotEmpty(timesSeries, "Counter mapping went wrong...");
        }
        
        [Test]
        public void Map_MeterMetric_TimesSeriesInNotEmpty()
        {
            _testsUtils.AddMeterMetricToMetricsBuilder(_metrics);

            var context = _metrics.Snapshot.Get().Contexts.First();
            var meter = context.Meters.First();

            MetricTimeSeriesMapper<MeterValueSource, MeterValue> metricTimeSeriesMapper =
                new MeterMetricTimeSeriesMapper(meter, context.Context);

            var timesSeries = metricTimeSeriesMapper.CreateTimesSeries();
            
            Assert.IsNotEmpty(timesSeries, "Counter mapping went wrong...");
        }
        
        [Test]
        public void Map_HistogramMetric_TimesSeriesInNotEmpty()
        {
            _testsUtils.AddHistogramMetricToMetricsBuilder(_metrics);

            var context = _metrics.Snapshot.Get().Contexts.First();
            var histogram = context.Histograms.First();

            MetricTimeSeriesMapper<HistogramValueSource, HistogramValue> metricTimeSeriesMapper =
                new HistogramMetricTimeSeriesMapper(histogram, context.Context);

            var timesSeries = metricTimeSeriesMapper.CreateTimesSeries();
            
            Assert.IsNotEmpty(timesSeries, "Counter mapping went wrong...");
        }
        
        [Test]
        public void Map_TimerMetric_TimesSeriesInNotEmpty()
        {
            _testsUtils.AddTimerMetricToMetricsBuilder(_metrics);

            var context = _metrics.Snapshot.Get().Contexts.First();
            var timer = context.Timers.First();

            MetricTimeSeriesMapper<TimerValueSource, TimerValue> metricTimeSeriesMapper =
                new TimerMetricTimeSeriesMapper(timer, context.Context);

            var timesSeries = metricTimeSeriesMapper.CreateTimesSeries();
            
            Assert.IsNotEmpty(timesSeries, "Counter mapping went wrong...");
        }
        
        [Test]
        public void Map_ApdexMetric_TimesSeriesInNotEmpty()
        {
            _testsUtils.AddApdexMetricToMetricsBuilder(_metrics);

            var context = _metrics.Snapshot.Get().Contexts.First();
            var apdex = context.ApdexScores.First();

            MetricTimeSeriesMapper<ApdexValueSource, ApdexValue> metricTimeSeriesMapper =
                new ApdexMetricTimeSeriesMapper(apdex, context.Context);

            var timesSeries = metricTimeSeriesMapper.CreateTimesSeries();
            
            Assert.IsNotEmpty(timesSeries, "Counter mapping went wrong...");
        }
    }
}