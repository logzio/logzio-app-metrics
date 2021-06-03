using System;
using App.Metrics;

namespace Core.Formatter
{
    public static class MetricsLogzioOutputFormatterBuilder
    {
        /// <summary>
        ///     Add the <see cref="MetricsLogzioOutputFormatter" /> allowing metrics to optionally be reported as Logzio compressed protobuf.
        /// </summary>
        /// <param name="metricFormattingBuilder">
        ///     The <see cref="IMetricsOutputFormattingBuilder" /> used to configure Logzio formatting
        ///     options.
        /// </param>
        /// <returns>
        ///     An <see cref="IMetricsBuilder" /> that can be used to further configure App Metrics.
        /// </returns>
        public static IMetricsBuilder AsLogzioCompressedProtobuf(
            this IMetricsOutputFormattingBuilder metricFormattingBuilder)
        {
            if (metricFormattingBuilder == null)
            {
                throw new ArgumentNullException(nameof(metricFormattingBuilder));
            }

            var formatter = new MetricsLogzioOutputFormatter();

            return metricFormattingBuilder.Using(formatter);
        }
    }
}