using System;
using System.Diagnostics;
using System.IO;
using VideoFilesEntityModel;

namespace VideoEncoderLibrary
{
    public class HandbrakeProcess
    {
        private Process _handbrakecli;
        public string HandbrakeOutput { get; private set; }
        public bool IsEncodeSuccess { get; private set; }
        private HandbrakeSettings _handbrakesettings;

        public HandbrakeProcess(HandbrakeSettings settings)
        {
            _handbrakesettings = settings;
            _handbrakecli = new Process
            {
                StartInfo =
                {
                    FileName = settings.HandbrakeCliPath,
                    Arguments = GenerateHandbrakeArgs(settings.VideoToEncode, settings.DestFolder),
                    RedirectStandardError = true,
                    UseShellExecute = false
                },
                EnableRaisingEvents = true
            };
            _handbrakecli.Exited += Handbrakecli_Exited;
            IsEncodeSuccess = false;
        }

        private void Handbrakecli_Exited(object sender, EventArgs e)
        {
            SetIsEncodeStatus();
        }

        private bool IsEncodeDone()
        {
            return HandbrakeOutput.Contains("Encode done!");
        }

        private void SetIsEncodeStatus()
        {
            IsEncodeSuccess = IsEncodeDone() && _handbrakecli.ExitCode == 0;
        }

        public void StartProcess()
        {
            _handbrakecli.Start();
            HandbrakeOutput = _handbrakecli.StandardError.ReadToEnd();
            _handbrakecli.WaitForExit();
        }

        private string GenerateHandbrakeArgs(VideoFile file, string destfolder)
        {
            var outputFileName = Path.ChangeExtension(file.FileName, ".mp4");
            var arguments = $"-i {file.FullPath} -o {destfolder}\\{outputFileName}";
            arguments += SetHandbrakePreset();
            return arguments;
        }

        private string SetHandbrakePreset()
        {
            var preset = "";
            if (!string.IsNullOrEmpty(_handbrakesettings.PresetFilePath))
                preset = $" --preset-import-file {_handbrakesettings.PresetFilePath}";
            else if (!string.IsNullOrEmpty(_handbrakesettings.PresetString))
                preset += $" --preset \"{_handbrakesettings.PresetString}\"";
            return preset;
        }
    }
}