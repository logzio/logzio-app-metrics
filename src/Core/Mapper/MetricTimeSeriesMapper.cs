using System;
using System.Collections.Generic;
using App.Metrics.Logzio.Prometheus;

namespace App.Metrics.Logzio.Mapper
{
    public abstract class MetricTimeSeriesMapper<T, S> where T : MetricValueSourceBase<S>
    {
        private const string MetricNameKey = "__name__";
        private const string MetricContextKey = "context";
        private const string MetricUnitKey = "unit";
        private const char MetricNameSeparator = '|';

        private readonly string _context;
        protected T Metric { get; }

        protected MetricTimeSeriesMapper(T metric, string context)
        {
            Metric = metric;
            _context = context;
        }

        public abstract IEnumerable<TimeSeries> CreateTimesSeries();

        protected TimeSeries CreateTimeSeries(double value, string name)
        {
            var timeSeries = new TimeSeries();

            AddSampleToTimeSeries(value, timeSeries);
            AddTagsToTimeSeries(timeSeries);
            AddLabelToTimeSeries(MetricNameKey, name, timeSeries);
            AddLabelToTimeSeries(MetricContextKey, _context, timeSeries);
            AddLabelToTimeSeries(MetricUnitKey, Metric.Unit.Name, timeSeries);

            return timeSeries;
        }

        protected void AddLabelToTimeSeries(string name, string value, TimeSeries timeSeries)
        {
            var label = new Label {Name = name, Value = value};
            timeSeries.Labels.Add(label);
        }
        
        protected string GetMetricName(string appendName = null)
        {
            var metricName = Metric.Name.Split(MetricNameSeparator)[0];

            return appendName == null ? metricName : string.Concat(metricName, appendName);
        }
        
        private void AddSampleToTimeSeries(double value, TimeSeries timeSeries)
        {
            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var sample = new Sample {Value = value, TimestampMillis = Convert.ToInt64(timeSpan.TotalMilliseconds)};

            timeSeries.Samples.Add(sample);
        }

        private void AddTagsToTimeSeries(TimeSeries timeSeries)
        {
            foreach (var (key, value) in Metric.Tags.ToDictionary())
            {
                AddLabelToTimeSeries(key, value, timeSeries);
            }
        }
    }
}