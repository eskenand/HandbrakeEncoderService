using System.IO;

namespace VideoEncoderLibrary.FileWatchers
{
    public class MkvFileWatcher : FileSystemWatcher
    {
        public MkvFileWatcher() : this("")
        {
        }

        public MkvFileWatcher(string directory) : base(directory, "*.mkv")
        {
            EnableRaisingEvents = true;
        }
    }
}