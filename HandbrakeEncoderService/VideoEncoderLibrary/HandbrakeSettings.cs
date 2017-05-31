using VideoFilesEntityModel;

namespace VideoEncoderLibrary
{
    public class HandbrakeSettings
    {
        public string HandbrakeCliPath { get; }
        public VideoFile VideoToEncode { get; }
        public string DestFolder { get; }
        public string PresetFilePath { get; set; }
        public string PresetString { get; set; }

        public HandbrakeSettings(string handbrakepath, VideoFile video, string destfolder, string presetfilepath = "")
        {
            HandbrakeCliPath = handbrakepath;
            VideoToEncode = video;
            DestFolder = destfolder;
            PresetFilePath = presetfilepath;
            PresetString = "";
        }
    }
}