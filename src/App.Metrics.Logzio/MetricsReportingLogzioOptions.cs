using System;
using App.Metrics.Filters;
using App.Metrics.Formatters;
using App.Metrics.Logzio.Client;

namespace App.Metrics.Logzio
{
    public class MetricsReportingLogzioOptions
    {
        public MetricsReportingLogzioOptions()
        {
            FlushInterval = TimeSpan.FromSeconds(15);
            HttpPolicy = new HttpPolicy
            {
                FailuresBeforeBackoff = Constants.DefaultFailuresBeforeBackoff,
                BackoffPeriod = Constants.DefaultBackoffPeriod,
                Timeout = Constants.DefaultTimeout
            };
            
            Logzio = new LogzioOptions();
        }

        /// <summary>
        ///     Gets or sets the HTTP policy settings which allows circuit breaker configuration to be adjusted
        /// </summary>
        /// <value>
        ///     The HTTP policy.
        /// </value>
        public HttpPolicy HttpPolicy { get; set; }
        
        /// <summary>
        ///     Gets or sets the available options for Logzio connectivity.
        /// </summary>
        /// <value>
        ///     The <see cref="LogzioOptions" />.
        /// </value>
        public LogzioOptions Logzio { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="IFilterMetrics" /> to use for just this reporter.
        /// </summary>
        /// <value>
        ///     The <see cref="IFilterMetrics" /> to use for this reporter.
        /// </value>
        public IFilterMetrics Filter { get; set; }

        /// <summary>
        ///     Gets or sets the interval between flushing metrics.
        /// </summary>
        public TimeSpan FlushInterval { get; set; }
    }
}