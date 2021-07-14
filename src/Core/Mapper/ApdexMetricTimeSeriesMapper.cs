using System.Collections.Generic;
using App.Metrics.Apdex;
using Core.Prometheus;

namespace Core.Mapper
{
    public sealed class ApdexMetricTimeSeriesMapper : MetricTimeSeriesMapper<ApdexValueSource, ApdexValue>
    {
        private const string SampleSizeAppendName = "_sample_size";
        private const string ScoreAppendName = "_score";
        private const string FrustratingAppendName = "_frustrating";
        private const string SatisfiedAppendName = "_satisfied";
        private const string ToleratingAppendName = "_tolerating";
        
        public ApdexMetricTimeSeriesMapper(ApdexValueSource apdexMetric, string context) : base(apdexMetric, context)
        {
        }

        public override IEnumerable<TimeSeries> CreateTimesSeries()
        {
            yield return CreateTimeSeries(Metric.Value.SampleSize, GetMetricName(SampleSizeAppendName));
            yield return CreateTimeSeries(Metric.Value.Score, GetMetricName(ScoreAppendName));
            yield return CreateTimeSeries(Metric.Value.Frustrating, GetMetricName(FrustratingAppendName));
            yield return CreateTimeSeries(Metric.Value.Satisfied, GetMetricName(SatisfiedAppendName));
            yield return CreateTimeSeries(Metric.Value.Tolerating, GetMetricName(ToleratingAppendName));
        }
    }
}