using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Formatters;

namespace App.Metrics.Logzio.Client
{
    public interface ILogzioClient
    {
        Task<LogzioWriteResult> WriteAsync(byte[] payload, MetricsMediaTypeValue mediaType, CancellationToken cancellationToken = default);
    }
}