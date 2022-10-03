using System;

namespace WebApi.Configuration
{
    public class ProxyOptions
    {
        public const string Proxy = "Proxy";
        public string KnowNetwork { get; set; } = String.Empty;
        public int NetworkLenght { get; set; } = 16;
    }
}
