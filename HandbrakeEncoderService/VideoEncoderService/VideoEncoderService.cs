using System;
using System.ServiceProcess;
using System.Timers;
using VideoEncoderLibrary;
using VideoEncoderLibrary.FileWatchers;
using VideoEncoderLibrary.Utilities;
using VideoFilesEntityModel;

namespace VideoEncoderService
{
    public partial class VideoEncoderService : ServiceBase
    {
        private static string Dropfolder => ConfigSettings.SourceFolder;
        private static string Destfolder => ConfigSettings.DestFolder;
        private static string HandbrakePath => ConfigSettings.HandbrakePath;
        private static string HandbrakePresetString => ConfigSettings.HandbrakePresetString;
        private static string _handbrakePresetFilePath = ConfigSettings.HandbrakePresetFilePath;
        private static readonly VideoRepository _repository = new VideoRepository();
        private static Timer _timer;

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public VideoEncoderService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            MkvFileWatcher mkvWatcher = new MkvFileWatcher(Dropfolder);
            mkvWatcher.Created += MkvFileCreated;
            _timer = new Timer(10000);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Enabled = true;
            log.Info("Starting VideoEncoderService.");
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            MainProcess();
        }

        protected override void OnStop()
        {
            log.Info("VideoEncoderService stopped.");
        }

        private static void MkvFileCreated(object sender, System.IO.FileSystemEventArgs e)
        {
            VideoFile video = VideoFactory.CreateVideoFile(sender, e);
            SaveVideoToDb(video);
        }

        private static void MainProcess()
        {
            try
            {
                VideoFile nextfile = _repository.GetNextVideoToEncode();
                if (IsFileReady(nextfile))
                {
                    _timer.Stop();
                    log.Info($"Starting encode of file: {nextfile.FullPath}");
                    StartHandbrake(nextfile);
                    log.Info($"Finished encoding filename: {nextfile.FullPath}");
                    _timer.Start();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        private static bool IsFileReady(VideoFile videofile)
        {
            if (videofile == null) return false;
            return !FileChecker.IsFileLocked(videofile.FullPath);
        }

        private static void SaveVideoToDb(VideoFile video)
        {
            if (!_repository.IsFileNameInDb(video))
            {
                _repository.InsertVideo(video);
                log.Info($"New video added to database. Filename: {video.FileName}");
            }
            else
                log.Warn($"Video not added to database, filename {video.FileName} already exsits.");
        }

        private static void StartHandbrake(VideoFile file)
        {
            HandbrakeSettings handbrakesettings =
                new HandbrakeSettings(HandbrakePath, file, Destfolder) {PresetString = HandbrakePresetString};
            log.Info($"Using handbrake preset string: {handbrakesettings.PresetString}");
            HandbrakeProcess handbrake = new HandbrakeProcess(handbrakesettings);
            handbrake.StartProcess();
            if (handbrake.IsEncodeSuccess)
            {
                file.IsEncoded = true;
                file.DateEncoded = DateTime.Now;
                _repository.UpdateVideo(file.Id);
            }
            else
            {
                log.Error("Video encoding failed. Output message: " + "\n" + handbrake.HandbrakeOutput);
            }
        }
    }
}