using System.Collections.Generic;
using App.Metrics.Logzio.Prometheus;
using App.Metrics.Timer;

namespace App.Metrics.Logzio.Mapper
{
    public sealed class TimerMetricTimeSeriesMapper : MetricTimeSeriesMapper<TimerValueSource, TimerValue>
    {
        private const string TimerMetricDurationUnitKey = "duration_unit";
        private const string TimerMetricRateUnitKey = "rate_unit";

        private const string CountAppendName = "_count";
        private const string ActiveSessionAppendName = "_histogram_active_session";
        private const string HistogramSumAppendName = "_histogram_sum";
        private const string HistogramLastValueAppendName = "_histogram_lastValue";
        private const string HistogramMaxAppendName = "_histogram_max";
        private const string HistogramMeanAppendName = "_histogram_mean";
        private const string HistogramMedianAppendName = "_histogram_median";
        private const string HistogramMinAppendName = "_histogram_min";
        private const string HistogramPercentile75AppendName = "_histogram_percentile75";
        private const string HistogramPercentile95AppendName = "_histogram_percentile95";
        private const string HistogramPercentile98AppendName = "_histogram_percentile98";
        private const string HistogramPercentile99AppendName = "_histogram_percentile99";
        private const string HistogramPercentile999AppendName = "_histogram_percentile999";
        private const string HistogramSampleSizeAppendName = "_histogram_sample_size";
        private const string HistogramStdDevAppendName = "_histogram_std_dev";
        private const string RateOneMinRateAppendName = "_rate_one_min_rate";
        private const string RateFiveMinRateAppendName = "_rate_five_min_rate";
        private const string RateFifteenMinRateAppendName = "_rate_fifteen_min_rate";
        private const string RateMeanRateAppendName = "_rate_mean_rate";
        
        public TimerMetricTimeSeriesMapper(TimerValueSource timerMetric, string context) : base(timerMetric, context)
        {
        }

        public override IEnumerable<TimeSeries> CreateTimesSeries()
        {
            yield return CreateTimerMetricTimeSeries(Metric.Value.ActiveSessions, GetMetricName(ActiveSessionAppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Histogram.Count, GetMetricName(CountAppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Histogram.Sum, GetMetricName(HistogramSumAppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Histogram.LastValue, GetMetricName(HistogramLastValueAppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Histogram.Max, GetMetricName(HistogramMaxAppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Histogram.Mean, GetMetricName(HistogramMeanAppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Histogram.Median, GetMetricName(HistogramMedianAppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Histogram.Min, GetMetricName(HistogramMinAppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Histogram.Percentile75, GetMetricName(HistogramPercentile75AppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Histogram.Percentile95, GetMetricName(HistogramPercentile95AppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Histogram.Percentile98, GetMetricName(HistogramPercentile98AppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Histogram.Percentile99, GetMetricName(HistogramPercentile99AppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Histogram.Percentile999, GetMetricName(HistogramPercentile999AppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Histogram.SampleSize, GetMetricName(HistogramSampleSizeAppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Histogram.StdDev, GetMetricName(HistogramStdDevAppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Rate.OneMinuteRate, GetMetricName(RateOneMinRateAppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Rate.FiveMinuteRate, GetMetricName(RateFiveMinRateAppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Rate.FifteenMinuteRate, GetMetricName(RateFifteenMinRateAppendName));
            yield return CreateTimerMetricTimeSeries(Metric.Value.Rate.MeanRate, GetMetricName(RateMeanRateAppendName));
        }
        
        private TimeSeries CreateTimerMetricTimeSeries(double value, string name)
        {
            var timeSeries = CreateTimeSeries(value, name);
            AddLabelToTimeSeries(TimerMetricDurationUnitKey, Metric.Value.DurationUnit.ToString(), timeSeries);
            AddLabelToTimeSeries(TimerMetricRateUnitKey, Metric.RateUnit.ToString(), timeSeries);

            return timeSeries;
        }
    }
}