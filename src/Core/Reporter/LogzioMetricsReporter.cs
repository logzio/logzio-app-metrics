using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Filters;
using App.Metrics.Formatters;
using App.Metrics.Logging;
using Core.Client;
using Core.Formatter;
using App.Metrics.Reporting;
using App.Metrics.Reporting.Http;

namespace Core.Reporter
{
    public class LogzioMetricsReporter : IReportMetrics
    {
        private static readonly ILog Logger = LogProvider.For<LogzioMetricsReporter>();
        private readonly ILogzioClient _logzioClient;

        public LogzioMetricsReporter(
            MetricsReportingLogzioOptions options,
            ILogzioClient logzioClient)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.FlushInterval < TimeSpan.Zero)
            {
                throw new InvalidOperationException($"{nameof(MetricsReportingHttpOptions.FlushInterval)} must not be less than zero");
            }

            _logzioClient = logzioClient ?? throw new ArgumentNullException(nameof(logzioClient));
            
            Formatter = new MetricsLogzioOutputFormatter();

            FlushInterval = options.FlushInterval > TimeSpan.Zero
                ? options.FlushInterval
                : AppMetricsConstants.Reporting.DefaultFlushInterval;

            Filter = options.Filter;

            Logger.Info($"Using Metrics Reporter {this}. Url: {options.Logzio.EndpointUri} FlushInterval: {FlushInterval}");
        }

        /// <inheritdoc />
        public IFilterMetrics Filter { get; set; }

        /// <inheritdoc />
        public TimeSpan FlushInterval { get; set; }

        /// <inheritdoc />
        public IMetricsOutputFormatter Formatter { get; set; }

        /// <inheritdoc />
        public async Task<bool> FlushAsync(MetricsDataValueSource metricsData, CancellationToken cancellationToken = default)
        {
            Logger.Trace("Flushing metrics snapshot");

            using (var stream = new MemoryStream())
            {
                await Formatter.WriteAsync(stream, metricsData, cancellationToken);

                var result = await _logzioClient.WriteAsync(stream.ToArray(), Formatter.MediaType, cancellationToken);

                if (result.Success)
                {
                    Logger.Trace("Flushed metrics snapshot");

                    return true;
                }

                Logger.Error(result.ErrorMessage);

                return false;
            }
        }
    }
}