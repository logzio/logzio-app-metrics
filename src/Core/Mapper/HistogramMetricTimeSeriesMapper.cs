using System.Collections.Generic;
using App.Metrics.Histogram;
using Core.Prometheus;

namespace Core.Mapper
{
    public sealed class HistogramMetricTimeSeriesMapper : MetricTimeSeriesMapper<HistogramValueSource, HistogramValue>
    {
        private const string HistogramMetricLastUserValueKey = "last_user_value";
        private const string HistogramMetricMaxUserValueKey = "max_user_value";
        private const string HistogramMetricMinUserValueKey = "min_user_value";
        
        private const string CountAppendName = "_count";
        private const string SumAppendName = "_sum";
        private const string LastValueAppendName = "_lastValue";
        private const string MaxAppendName = "_max";
        private const string MeanAppendName = "_mean";
        private const string MedianAppendName = "_median";
        private const string MinAppendName = "_min";
        private const string Percentile75AppendName = "_percentile75";
        private const string Percentile95AppendName = "_percentile95";
        private const string Percentile98AppendName = "_percentile98";
        private const string Percentile99AppendName = "_percentile99";
        private const string Percentile999AppendName = "_percentile999";
        private const string SampleSizeAppendName = "_sample_size";
        private const string StdDevAppendName = "_std_dev";

        public HistogramMetricTimeSeriesMapper(HistogramValueSource histogramMetric, string context) : base(histogramMetric, context)
        {
        }
        
        public override IEnumerable<TimeSeries> CreateTimesSeries()
        {
            yield return CreateHistogramMetricTimeSeries(Metric.Value.Count, GetMetricName(CountAppendName));
            yield return CreateHistogramMetricTimeSeries(Metric.Value.Sum, GetMetricName(SumAppendName));
            yield return CreateHistogramMetricTimeSeries(Metric.Value.LastValue, GetMetricName(LastValueAppendName));
            yield return CreateHistogramMetricTimeSeries(Metric.Value.Max, GetMetricName(MaxAppendName));
            yield return CreateHistogramMetricTimeSeries(Metric.Value.Mean, GetMetricName(MeanAppendName));
            yield return CreateHistogramMetricTimeSeries(Metric.Value.Median, GetMetricName(MedianAppendName));
            yield return CreateHistogramMetricTimeSeries(Metric.Value.Min, GetMetricName(MinAppendName));
            yield return CreateHistogramMetricTimeSeries(Metric.Value.Percentile75, GetMetricName(Percentile75AppendName));
            yield return CreateHistogramMetricTimeSeries(Metric.Value.Percentile95, GetMetricName(Percentile95AppendName));
            yield return CreateHistogramMetricTimeSeries(Metric.Value.Percentile98, GetMetricName(Percentile98AppendName));
            yield return CreateHistogramMetricTimeSeries(Metric.Value.Percentile99, GetMetricName(Percentile99AppendName));
            yield return CreateHistogramMetricTimeSeries(Metric.Value.Percentile999, GetMetricName(Percentile999AppendName));
            yield return CreateHistogramMetricTimeSeries(Metric.Value.SampleSize, GetMetricName(SampleSizeAppendName));
            yield return CreateHistogramMetricTimeSeries(Metric.Value.StdDev, GetMetricName(StdDevAppendName));
        }
        
        private TimeSeries CreateHistogramMetricTimeSeries(double value, string name)
        {
            var timeSeries = CreateTimeSeries(value, name);

            if (string.IsNullOrWhiteSpace(Metric.Value.LastUserValue))
            {
                return timeSeries;
            }

            AddLabelToTimeSeries(HistogramMetricLastUserValueKey, Metric.Value.LastUserValue, timeSeries);
            AddLabelToTimeSeries(HistogramMetricMaxUserValueKey, Metric.Value.MaxUserValue, timeSeries);
            AddLabelToTimeSeries(HistogramMetricMinUserValueKey, Metric.Value.MinUserValue, timeSeries);
            
            return timeSeries;
        }
    }
}