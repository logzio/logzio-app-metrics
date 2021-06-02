namespace App.Metrics.Logzio.Client
{
    public struct LogzioWriteResult
    {
        public LogzioWriteResult(bool success)
        {
            Success = success;
            ErrorMessage = null;
        }

        public LogzioWriteResult(bool success, string errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }

        public bool Success { get; }

        public static readonly LogzioWriteResult SuccessResult = new LogzioWriteResult(true);
    }
}