using System.IO;

namespace VideoEncoderLibrary.FileWatchers
{
    public class TxtFileWatcher : FileSystemWatcher
    {
        public TxtFileWatcher() : this("")
        {
        }

        public TxtFileWatcher(string directory) : base(directory, "*.txt")
        {
            EnableRaisingEvents = true;
        }
    }
}