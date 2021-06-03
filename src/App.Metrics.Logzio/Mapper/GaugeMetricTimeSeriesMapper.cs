using System.Collections.Generic;
using App.Metrics.Gauge;
using App.Metrics.Logzio.Prometheus;

namespace App.Metrics.Logzio.Mapper
{
    public sealed class GaugeMetricTimeSeriesMapper : MetricTimeSeriesMapper<GaugeValueSource, double>
    {
        public GaugeMetricTimeSeriesMapper(GaugeValueSource gaugeMetric, string context) : base(gaugeMetric, context)
        {
        }

        public override IEnumerable<TimeSeries> CreateTimesSeries()
        {
            yield return CreateTimeSeries(Metric.Value, GetMetricName());
        }
    }
}