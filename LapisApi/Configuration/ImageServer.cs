using System;

namespace WebApi.Configuration
{
    public class ImageServerOptions
    {
        public const string ImageServer = "ImageServer";
        public string ByPassAddress { get; set; } = String.Empty;
        public string SecurityPhase { get; set; } = String.Empty;
    }
}
