using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Formatters;
using App.Metrics.Logzio.Mapper;
using App.Metrics.Logzio.Prometheus;
using Google.Protobuf;

namespace App.Metrics.Logzio.Formatter
{
    public class MetricsLogzioOutputFormatter : IMetricsOutputFormatter
    {
        public MetricsMediaTypeValue MediaType =>
            new MetricsMediaTypeValue("application", "vnd.appmetrics.metrics.prometheus", "v1", "x-protobuf");

        public MetricFields MetricFields { get; set; }

        public Task WriteAsync(
            Stream output,
            MetricsDataValueSource snapshot,
            CancellationToken cancellationToken = default)
        {
            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var writeRequest = CreateWriteRequest(snapshot);
            var compressedProtobuf = Snappy.Sharp.Snappy.Compress(writeRequest.ToByteArray());

            return output.WriteAsync(compressedProtobuf, 0, compressedProtobuf.Length, cancellationToken);
        }

        private WriteRequest CreateWriteRequest(MetricsDataValueSource snapshot)
        {
            var writeRequest = new WriteRequest();

            foreach (var metricsContext in snapshot.Contexts)
            {
                foreach (var gaugeMetric in metricsContext.Gauges)
                {
                    AddTimesSeriesToWriteRequest(new GaugeMetricTimeSeriesMapper(gaugeMetric, metricsContext.Context),
                        writeRequest);
                }

                foreach (var counterMetric in metricsContext.Counters)
                {
                    AddTimesSeriesToWriteRequest(new CounterMetricTimeSeriesMapper(counterMetric, metricsContext.Context),
                        writeRequest);
                }

                foreach (var meterMetric in metricsContext.Meters)
                {
                    AddTimesSeriesToWriteRequest(new MeterMetricTimeSeriesMapper(meterMetric, metricsContext.Context),
                        writeRequest);
                }

                foreach (var histogramMetric in metricsContext.Histograms)
                {
                    AddTimesSeriesToWriteRequest(new HistogramMetricTimeSeriesMapper(histogramMetric, metricsContext.Context),
                        writeRequest);
                }

                foreach (var timerMetric in metricsContext.Timers)
                {
                    AddTimesSeriesToWriteRequest(new TimerMetricTimeSeriesMapper(timerMetric, metricsContext.Context),
                        writeRequest);
                }

                foreach (var apdexMetric in metricsContext.ApdexScores)
                {
                    AddTimesSeriesToWriteRequest(new ApdexMetricTimeSeriesMapper(apdexMetric, metricsContext.Context),
                        writeRequest);
                }
            }

            return writeRequest;
        }

        private void AddTimesSeriesToWriteRequest<T, S>(MetricTimeSeriesMapper<T, S> metricTimeSeriesMapper,
            WriteRequest writeRequest) where T : MetricValueSourceBase<S>
        {
            foreach (var timeSeries in metricTimeSeriesMapper.CreateTimesSeries())
            {
                writeRequest.Timeseries.Add(timeSeries);
            }
        }
    }
}