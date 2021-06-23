using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Formatters;
using App.Metrics.Logging;
using Core.Reporter;

namespace Core.Client
{
    public class DefaultLogzioHttpClient : ILogzioClient
    {
        private static readonly ILog Logger = LogProvider.For<DefaultLogzioHttpClient>();

        private static long _backOffTicks;
        private static long _failureAttempts;
        private static long _failuresBeforeBackoff;
        private static TimeSpan _backOffPeriod;
        private readonly HttpClient _httpClient;
        private readonly LogzioOptions _options;

        public DefaultLogzioHttpClient(
            HttpClient httpClient,
            LogzioOptions options,
            HttpPolicy httpPolicy)
        {
            if (httpPolicy == null)
            {
                throw new ArgumentNullException(nameof(httpPolicy));
            }

            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _backOffPeriod = httpPolicy.BackoffPeriod;
            _failuresBeforeBackoff = httpPolicy.FailuresBeforeBackoff;
            _failureAttempts = 0;
        }

        public async Task<LogzioWriteResult> WriteAsync(
            byte[] payload,
            MetricsMediaTypeValue mediaType,
            CancellationToken cancellationToken = default)
        {
            if (NeedToBackoff())
            {
                return new LogzioWriteResult(false, "Too many failures in writing to Logzio, Circuit Opened");
            }

            try
            {
                var message = new HttpRequestMessage(HttpMethod.Post, _options.EndpointUri);
                var byteArrayContent = new ByteArrayContent(payload, 0, payload.Length);
                var version = GetType().Assembly.GetName().Version;
                string versionStr = version == null ? "1.0.0.0" : version.ToString();

                message.Headers.Authorization = AuthenticationHeaderValue.Parse($"Bearer {_options.Token}");
                byteArrayContent.Headers.Remove("Content-Type");
                byteArrayContent.Headers.Add("Content-Type", mediaType.ContentType);
                byteArrayContent.Headers.Remove("Content-Encoding");
                byteArrayContent.Headers.Add("Content-Encoding", "snappy");
                byteArrayContent.Headers.Add("Logzio-Shipper",
                    $"logzio-app-metrics/v{versionStr}/{_failureAttempts}/0");

                message.Content = byteArrayContent;

                var response = await _httpClient.SendAsync(message, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    Interlocked.Increment(ref _failureAttempts);

                    var errorMessage =
                        $"Failed to write to Logzio - StatusCode: {response.StatusCode} Reason: {response.ReasonPhrase}";
                    Logger.Error(errorMessage);

                    return new LogzioWriteResult(false, errorMessage);
                }

                Logger.Trace("Successful write to Logzio");

                return new LogzioWriteResult(true);
            }
            catch (Exception ex)
            {
                Interlocked.Increment(ref _failureAttempts);
                Logger.Error(ex, "Failed to write to Logzio");
                return new LogzioWriteResult(false, ex.ToString());
            }
        }

        private bool NeedToBackoff()
        {
            if (Interlocked.Read(ref _failureAttempts) < _failuresBeforeBackoff)
            {
                return false;
            }

            Logger.Error($"Logzio write backoff for {_backOffPeriod.Seconds} secs");

            if (Interlocked.Read(ref _backOffTicks) == 0)
            {
                Interlocked.Exchange(ref _backOffTicks, DateTime.UtcNow.Add(_backOffPeriod).Ticks);
            }

            if (DateTime.UtcNow.Ticks <= Interlocked.Read(ref _backOffTicks))
            {
                return true;
            }

            Interlocked.Exchange(ref _failureAttempts, 0);
            Interlocked.Exchange(ref _backOffTicks, 0);

            return false;
        }
    }
}