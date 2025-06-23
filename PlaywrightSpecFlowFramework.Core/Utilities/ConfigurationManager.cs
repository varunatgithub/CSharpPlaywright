using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace PlaywrightSpecFlowFramework.Core.Utilities
{
    public class TestSettings
    {
        public string BrowserType { get; set; } = "Chromium";
        public string BaseUrl { get; set; } = string.Empty;
        public bool Headless { get; set; } = true;
        public float? SlowMo { get; set; } = 0;
        public float DefaultTimeout { get; set; } = 30000;
        public ViewportSize? Viewport { get; set; }
        public string ScreenshotPath { get; set; } = "Screenshots";
    }

    public class ViewportSize
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public static class ConfigurationManager
    {
        private static readonly Lazy<IConfiguration> _configuration;
        private static readonly Lazy<TestSettings> _testSettings;

        static ConfigurationManager()
        {
            _configuration = new Lazy<IConfiguration>(() =>
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()) // Assumes appsettings.json is in the output directory
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                return builder.Build();
            });

            _testSettings = new Lazy<TestSettings>(() =>
            {
                var settings = new TestSettings();
                ConfigurationRoot.Bind(settings);
                return settings;
            });
        }

        public static IConfiguration ConfigurationRoot => _configuration.Value;
        public static TestSettings AppSettings => _testSettings.Value;
    }
}
