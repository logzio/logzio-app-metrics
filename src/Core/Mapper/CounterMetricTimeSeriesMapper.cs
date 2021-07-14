using System.Collections.Generic;
using App.Metrics.Counter;
using Core.Prometheus;

namespace Core.Mapper
{
    public sealed class CounterMetricTimeSeriesMapper : MetricTimeSeriesMapper<CounterValueSource, CounterValue>
    {
        public CounterMetricTimeSeriesMapper(CounterValueSource counterMetric, string context) : base(counterMetric, context)
        {
        }

        public override IEnumerable<TimeSeries> CreateTimesSeries()
        {
            yield return CreateTimeSeries(Metric.Value.Count, GetMetricName());
        }
    }
}