using System;
using System.IO;
using VideoFilesEntityModel;

namespace VideoEncoderLibrary.Utilities
{
    public static class VideoFactory
    {
        public static VideoFile CreateVideoFile(object source, FileSystemEventArgs e)
        {
            var file = new FileInfo(e.Name);

            var videoFile = new VideoFile()
            {
                FileName = file.Name,
                FileExtension = file.Extension,
                FullPath = e.FullPath,
                DateCreated = DateTime.Now,
                IsEncoded = false,
            };

            return videoFile;
        }
    }
}