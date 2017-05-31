using System.Configuration;

namespace VideoEncoderLibrary.Utilities
{
    public static class ConfigSettings
    {
        public static string SourceFolder { get; } = ConfigurationManager.AppSettings["SourceFolder"];
        public static string DestFolder { get; } = ConfigurationManager.AppSettings["DestFolder"];
        public static string HandbrakePath { get; } = ConfigurationManager.AppSettings["HandbrakePath"];

        public static string HandbrakePresetFilePath { get; } =
            ConfigurationManager.AppSettings["HandbrakePresetPath"] ?? "";

        public static string HandbrakePresetString { get; set; } =
            ConfigurationManager.AppSettings["HandbrakePresetString"] ?? "";
    }
}