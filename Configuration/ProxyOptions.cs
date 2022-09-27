using System;

namespace WebApi.Configuration
{
    public class ProxyOptions
    {
        public const string Proxy = "Proxy";
        public string KnownProxy { get; set; } = String.Empty;
    }
}
