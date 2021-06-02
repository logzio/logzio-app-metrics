using System;
using System.IO;
using System.Net.Http;
using System.Xml.Serialization;
using App.Metrics.Builder;
using App.Metrics.Logzio.Client;
using HttpPolicy = App.Metrics.Logzio.Client.HttpPolicy;

namespace App.Metrics.Logzio
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
            if (metricReporterProviderBuilder == null)
            {
                throw new ArgumentNullException(nameof(metricReporterProviderBuilder));
            }

            var httpClient = CreateClient(options, options.HttpPolicy);
            var provider = new LogzioMetricsReporter(options, httpClient);

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
            if (metricReporterProviderBuilder == null)
            {
                throw new ArgumentNullException(nameof(metricReporterProviderBuilder));
            }

            var options = new MetricsReportingLogzioOptions();

            setupAction?.Invoke(options);

            var httpClient = CreateClient(options, options.HttpPolicy);
            var provider = new LogzioMetricsReporter(options, httpClient);

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
            if (metricReporterProviderBuilder == null)
            {
                throw new ArgumentNullException(nameof(metricReporterProviderBuilder));
            }

            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (!Uri.TryCreate(endpoint, UriKind.Absolute, out var uri))
            {
                throw new InvalidOperationException($"{nameof(endpoint)} must be a valid absolute URI");
            }

            var options = new MetricsReportingLogzioOptions
            {
                Logzio =
                {
                    EndpointUri = uri,
                    Token = token
                }
            };
            
            var httpClient = CreateClient(options, options.HttpPolicy);
            var provider = new LogzioMetricsReporter(options, httpClient);

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
            if (metricReporterProviderBuilder == null)
            {
                throw new ArgumentNullException(nameof(metricReporterProviderBuilder));
            }

            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (!Uri.TryCreate(endpoint, UriKind.Absolute, out var uri))
            {
                throw new InvalidOperationException($"{nameof(endpoint)} must be a valid absolute URI");
            }

            var options = new MetricsReportingLogzioOptions()
            {
                Logzio =
                {
                    EndpointUri = uri,
                    Token = token
                },
                FlushInterval = flushInterval
            };

            var httpClient = CreateClient(options, options.HttpPolicy);
            var provider = new LogzioMetricsReporter(options, httpClient);

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
            if (metricReporterProviderBuilder == null)
            {
                throw new ArgumentNullException(nameof(metricReporterProviderBuilder));
            }

            if (logzioConfigFilePath == null)
            {
                throw new ArgumentNullException(nameof(logzioConfigFilePath));
            }
            
            if (!File.Exists(logzioConfigFilePath))
            {
                throw new FileNotFoundException($"{logzioConfigFilePath} does not exist");
            }
            
            GetLogzioConnection(logzioConfigFilePath, out string endpoint, out string token);
            
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (!Uri.TryCreate(endpoint, UriKind.Absolute, out var uri))
            {
                throw new InvalidOperationException($"{nameof(endpoint)} must be a valid absolute URI");
            }

            var options = new MetricsReportingLogzioOptions
            {
                Logzio =
                {
                    EndpointUri = uri,
                    Token = token
                }
            };
            
            var httpClient = CreateClient(options, options.HttpPolicy);
            var provider = new LogzioMetricsReporter(options, httpClient);

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
            if (metricReporterProviderBuilder == null)
            {
                throw new ArgumentNullException(nameof(metricReporterProviderBuilder));
            }

            if (logzioConfigFilePath == null)
            {
                throw new ArgumentNullException(nameof(logzioConfigFilePath));
            }
            
            if (!File.Exists(logzioConfigFilePath))
            {
                throw new FileNotFoundException($"{logzioConfigFilePath} does not exist");
            }
            
            GetLogzioConnection(logzioConfigFilePath, out string endpoint, out string token);
            
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (!Uri.TryCreate(endpoint, UriKind.Absolute, out var uri))
            {
                throw new InvalidOperationException($"{nameof(endpoint)} must be a valid absolute URI");
            }

            var options = new MetricsReportingLogzioOptions()
            {
                Logzio =
                {
                    EndpointUri = uri,
                    Token = token
                },
                FlushInterval = flushInterval
            };

            var httpClient = CreateClient(options, options.HttpPolicy);
            var provider = new LogzioMetricsReporter(options, httpClient);

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

        public static void GetLogzioConnection(string logzioConfigFilePath, out string endpoint, out string token)
        {
            var xmlSerializer = new XmlSerializer(typeof(Configuration.Configuration));

            using (Stream reader = new FileStream(logzioConfigFilePath, FileMode.Open))
            {
                var logzioCobfiguration = (Configuration.Configuration) xmlSerializer.Deserialize(reader);

                endpoint = logzioCobfiguration.LogzioConnection.Endpoint;
                token = logzioCobfiguration.LogzioConnection.Token;
            }
        }
    }
}