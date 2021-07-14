using System.Collections.Generic;
using Core.Prometheus;
using App.Metrics.Meter;

namespace Core.Mapper
{
    public sealed class MeterMetricTimeSeriesMapper : MetricTimeSeriesMapper<MeterValueSource, MeterValue>
    {
        private const string MeterMetricRateUnitKey = "rate_unit";
        
        private const string CountAppendName = "_count";
        private const string OneMinRateAppendName = "_one_min_rate";
        private const string FiveMinRateAppendName = "_five_min_rate";
        private const string FifteenMinRateAppendName = "_fifteen_min_rate";
        private const string MeanRateAppendName = "_mean_rate";
        
        public MeterMetricTimeSeriesMapper(MeterValueSource meterMetric, string context) : base(meterMetric, context)
        {
        }

        public override IEnumerable<TimeSeries> CreateTimesSeries()
        {
            yield return CreateMeterMetricTimeSeries(Metric.Value.Count, GetMetricName(CountAppendName));
            yield return CreateMeterMetricTimeSeries(Metric.Value.OneMinuteRate, GetMetricName(OneMinRateAppendName));
            yield return CreateMeterMetricTimeSeries(Metric.Value.FiveMinuteRate, GetMetricName(FiveMinRateAppendName));
            yield return CreateMeterMetricTimeSeries(Metric.Value.FifteenMinuteRate, GetMetricName(FifteenMinRateAppendName));
            yield return CreateMeterMetricTimeSeries(Metric.Value.MeanRate, GetMetricName(MeanRateAppendName));
        }

        private TimeSeries CreateMeterMetricTimeSeries(double value, string name)
        {
            var timeSeries = CreateTimeSeries(value, name);
            AddLabelToTimeSeries(MeterMetricRateUnitKey, Metric.Value.RateUnit.ToString(), timeSeries);

            return timeSeries;
        }
    }
}