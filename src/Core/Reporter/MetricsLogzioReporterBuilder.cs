using System;
using System.IO;
using System.Net.Http;
using System.Xml.Serialization;
using App.Metrics;
using App.Metrics.Builder;
using App.Metrics.Reporting;
using Core.Client;
using HttpPolicy = Core.Client.HttpPolicy;

namespace Core.Reporter
{
    public static class MetricsLogzioReporterBuilder
    {
        /// <summary>
        ///     Add the <see cref="LogzioMetricsReporter" /> allowing metrics to be reported to Logzio.
        /// </summary>
        /// <param name="metricReporterProviderBuilder">
        ///     The <see cref="IMetricsReportingBuilder" /> used to configure metrics reporters.
        /// </param>
        /// <param name="options">The Logzio reporting options to use.</param>
        /// <returns>
        ///     An <see cref="IMetricsBuilder" /> that can be used to further configure App Metrics.
        /// </returns>
        public static IMetricsBuilder ToLogzioHttp(
            this IMetricsReportingBuilder metricReporterProviderBuilder,
            MetricsReportingLogzioOptions options)
        {
            IsMetricReportingBuilderNull(metricReporterProviderBuilder);
            IsEndpointUriNull(options.Logzio.EndpointUri);
            IsTokenNullOrWhiteSpace(options.Logzio.Token);
            
            var provider = GetLogzioMetricsReporter(options);

            return metricReporterProviderBuilder.Using(provider);
        }

        /// <summary>
        ///     Add the <see cref="LogzioMetricsReporter" /> allowing metrics to be reported to Logzio.
        /// </summary>
        /// <param name="metricReporterProviderBuilder">
        ///     The <see cref="IMetricsReportingBuilder" /> used to configure metrics reporters.
        /// </param>
        /// <param name="setupAction">The Logzio reporting options to use.</param>
        /// <returns>
        ///     An <see cref="IMetricsBuilder" /> that can be used to further configure App Metrics.
        /// </returns>
        public static IMetricsBuilder ToLogzioHttp(
            this IMetricsReportingBuilder metricReporterProviderBuilder,
            Action<MetricsReportingLogzioOptions> setupAction)
        {
            IsMetricReportingBuilderNull(metricReporterProviderBuilder);

            var options = new MetricsReportingLogzioOptions();

            setupAction?.Invoke(options);
            
            IsEndpointUriNull(options.Logzio.EndpointUri);
            IsTokenNullOrWhiteSpace(options.Logzio.Token);

            var provider = GetLogzioMetricsReporter(options);

            return metricReporterProviderBuilder.Using(provider);
        }

        /// <summary>
        ///     Add the <see cref="LogzioMetricsReporter" /> allowing metrics to be reported to Logzio.
        /// </summary>
        /// <param name="metricReporterProviderBuilder">
        ///     The <see cref="IMetricsReportingBuilder" /> used to configure metrics reporters.
        /// </param>
        /// <param name="endpoint">The Logzio endpoint where metrics are POSTed.</param>
        /// <param name="token">The Logzio token</param>
        /// <returns>
        ///     An <see cref="IMetricsBuilder" /> that can be used to further configure App Metrics.
        /// </returns>
        public static IMetricsBuilder ToLogzioHttp(
            this IMetricsReportingBuilder metricReporterProviderBuilder,
            string endpoint,
            string token)
        {
            IsMetricReportingBuilderNull(metricReporterProviderBuilder);
            IsEndpointValid(endpoint, out var uri);
            IsTokenNullOrWhiteSpace(token);

            var options = GetMetricsReportingLogzioOptionsWithLogzio(uri, endpoint);
            var provider = GetLogzioMetricsReporter(options);

            return metricReporterProviderBuilder.Using(provider);
        }
        
        /// <summary>
        ///     Add the <see cref="LogzioMetricsReporter" /> allowing metrics to be reported to Logzio.
        /// </summary>
        /// <param name="metricReporterProviderBuilder">
        ///     The <see cref="IMetricsReportingBuilder" /> used to configure metrics reporters.
        /// </param>
        /// <param name="endpoint">The Logzio endpoint where metrics are POSTed.</param>
        /// <param name="token">The Logzio token</param>
        /// <param name="flushInterval">
        ///     The <see cref="T:System.TimeSpan" /> interval used if intended to schedule metrics
        ///     reporting.
        /// </param>
        /// <returns>
        ///     An <see cref="IMetricsBuilder" /> that can be used to further configure App Metrics.
        /// </returns>
        public static IMetricsBuilder ToLogzioHttp(
            this IMetricsReportingBuilder metricReporterProviderBuilder,
            string endpoint,
            string token,
            TimeSpan flushInterval)
        {
            IsMetricReportingBuilderNull(metricReporterProviderBuilder);
            IsEndpointValid(endpoint, out var uri);
            IsTokenNullOrWhiteSpace(token);

            var options = GetMetricsReportingLogzioOptionsWithLogzioAndFlushInterval(uri, token, flushInterval);
            var provider = GetLogzioMetricsReporter(options);

            return metricReporterProviderBuilder.Using(provider);
        }
        
        /// <summary>
        ///     Add the <see cref="LogzioMetricsReporter" /> allowing metrics to be reported to Logzio.
        /// </summary>
        /// <param name="metricReporterProviderBuilder">
        ///     The <see cref="IMetricsReportingBuilder" /> used to configure metrics reporters.
        /// </param>
        /// <param name="logzioConfigFilePath">The Logzio config file path</param>
        /// <returns>
        ///     An <see cref="IMetricsBuilder" /> that can be used to further configure App Metrics.
        /// </returns>
        public static IMetricsBuilder ToLogzioHttp(
            this IMetricsReportingBuilder metricReporterProviderBuilder,
            string logzioConfigFilePath)
        {
            IsMetricReportingBuilderNull(metricReporterProviderBuilder);
            IsConfigFilePathValid(logzioConfigFilePath, out var endpoint, out var token);
            IsEndpointValid(endpoint, out var uri);
            IsTokenNullOrWhiteSpace(token);

            var options = GetMetricsReportingLogzioOptionsWithLogzio(uri, token);
            var provider = GetLogzioMetricsReporter(options);

            return metricReporterProviderBuilder.Using(provider);
        }

        /// <summary>
        ///     Add the <see cref="LogzioMetricsReporter" /> allowing metrics to be reported to Logzio.
        /// </summary>
        /// <param name="metricReporterProviderBuilder">
        ///     The <see cref="IMetricsReportingBuilder" /> used to configure metrics reporters.
        /// </param>
        /// <param name="logzioConfigFilePath">The Logzio config file path</param>
        /// <param name="flushInterval">
        ///     The <see cref="T:System.TimeSpan" /> interval used if intended to schedule metrics
        ///     reporting.
        /// </param>
        /// <returns>
        ///     An <see cref="IMetricsBuilder" /> that can be used to further configure App Metrics.
        /// </returns>
        public static IMetricsBuilder ToLogzioHttp(
            this IMetricsReportingBuilder metricReporterProviderBuilder,
            string logzioConfigFilePath,
            TimeSpan flushInterval)
        {
            IsMetricReportingBuilderNull(metricReporterProviderBuilder);
            IsConfigFilePathValid(logzioConfigFilePath, out var endpoint, out var token);
            IsEndpointValid(endpoint, out var uri);
            IsTokenNullOrWhiteSpace(token);

            var options = GetMetricsReportingLogzioOptionsWithLogzioAndFlushInterval(uri, token, flushInterval);
            var provider = GetLogzioMetricsReporter(options);

            return metricReporterProviderBuilder.Using(provider);
        }

        private static ILogzioClient CreateClient(
            MetricsReportingLogzioOptions options,
            HttpPolicy httpPolicy,
            HttpMessageHandler httpMessageHandler = null)
        {
            var httpClient = httpMessageHandler == null
                ? new HttpClient()
                : new HttpClient(httpMessageHandler);

            httpClient.BaseAddress = options.Logzio.EndpointUri;
            httpClient.Timeout = httpPolicy.Timeout;

            return new DefaultLogzioHttpClient(
                httpClient,
                options.Logzio,
                httpPolicy);
        }

        public static bool GetLogzioConnection(string logzioConfigFilePath, out string endpoint, out string token)
        {
            var xmlSerializer = new XmlSerializer(typeof(Configuration.Configuration));
            endpoint = null;
            token = null;
            
            using (Stream reader = new FileStream(logzioConfigFilePath, FileMode.Open))
            {
                var logzioConfiguration = (Configuration.Configuration) xmlSerializer.Deserialize(reader);

                if (logzioConfiguration?.LogzioConnection?.Endpoint == null)
                {
                    return false;
                }

                if (logzioConfiguration.LogzioConnection.Token == null)
                {
                    return false;
                }
                
                endpoint = logzioConfiguration.LogzioConnection.Endpoint;
                token = logzioConfiguration.LogzioConnection.Token;
            }

            return true;
        }

        private static void IsMetricReportingBuilderNull(IMetricsReportingBuilder metricReporterProviderBuilder)
        {
            if (metricReporterProviderBuilder == null)
            {
                throw new ArgumentNullException(nameof(metricReporterProviderBuilder));
            }
        }

        private static void IsEndpointUriNull(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
        }

        private static void IsEndpointValid(string endpoint, out Uri uri)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new ArgumentNullException(nameof(endpoint));
            }
            
            if (!Uri.TryCreate(endpoint, UriKind.Absolute, out uri))
            {
                throw new InvalidOperationException($"{nameof(endpoint)} must be a valid absolute URI");
            }
        }

        private static void IsTokenNullOrWhiteSpace(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(nameof(token));
            }
        }

        private static void IsConfigFilePathValid(string configFilePath, out string endpoint, out string token)
        {
            if (configFilePath == null)
            {
                throw new ArgumentNullException(nameof(configFilePath));
            }
            
            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException($"{configFilePath} does not exist");
            }
            
            if (!GetLogzioConnection(configFilePath, out endpoint, out token))
            {
                throw new Exception("The config file is bad formatted");
            }
        }

        private static IReportMetrics GetLogzioMetricsReporter(MetricsReportingLogzioOptions options)
        {
            var httpClient = CreateClient(options, options.HttpPolicy);
            
            return new LogzioMetricsReporter(options, httpClient);
        }

        private static MetricsReportingLogzioOptions GetMetricsReportingLogzioOptionsWithLogzio(Uri uri, string token)
        {
            return new MetricsReportingLogzioOptions()
            {
                Logzio =
                {
                    EndpointUri = uri,
                    Token = token
                }
            };
        }

        private static MetricsReportingLogzioOptions GetMetricsReportingLogzioOptionsWithLogzioAndFlushInterval(Uri uri,
            string token, TimeSpan flushInterval)
        {
            var options = GetMetricsReportingLogzioOptionsWithLogzio(uri, token);

            options.FlushInterval = flushInterval;
            
            return options;
        }
    }
}