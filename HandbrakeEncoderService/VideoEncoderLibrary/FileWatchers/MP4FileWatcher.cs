using System.IO;

namespace VideoEncoderLibrary.FileWatchers
{
    public class Mp4FileWatcher : FileSystemWatcher
    {
        public Mp4FileWatcher() : this("")
        {
        }

        public Mp4FileWatcher(string directory) : base(directory, "*.mp4")
        {
            EnableRaisingEvents = true;
        }
    }
}