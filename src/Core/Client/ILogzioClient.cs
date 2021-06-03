using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Formatters;

namespace Core.Client
{
    public interface ILogzioClient
    {
        Task<LogzioWriteResult> WriteAsync(byte[] payload, MetricsMediaTypeValue mediaType, CancellationToken cancellationToken = default);
    }
}