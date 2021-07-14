using System;

namespace Core.Reporter
{
    public class LogzioOptions
    {
        public LogzioOptions(Uri endpointUri, string token)
        {
            EndpointUri = endpointUri ?? throw new ArgumentNullException(nameof(endpointUri));
            Token = token ?? throw new ArgumentNullException(nameof(token));
        }

        public LogzioOptions()
        {
        }
        
        public Uri EndpointUri { get; set; }

        public string Token { get; set; }
    }
}