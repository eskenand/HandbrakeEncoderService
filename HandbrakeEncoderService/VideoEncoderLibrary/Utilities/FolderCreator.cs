using System.IO;
using System.Diagnostics;

namespace VideoEncoderLibrary.Utilities
{
    public class FolderCreator
    {
        private string _destFolder;

        public FolderCreator(string destfolder)
        {
            _destFolder = destfolder;
        }

        public string FolderPath { get; private set; }

        public void CreateFileRootFolder(object source, FileSystemEventArgs e)
        {
            try
            {
                var folderPath = Path.GetFileNameWithoutExtension(e.FullPath);
                Directory.CreateDirectory($"{_destFolder}/{folderPath}");
            }
            catch (IOException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}