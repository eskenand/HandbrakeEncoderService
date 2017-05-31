namespace VideoFilesEntityModel
{
    using System;

    public partial class VideoFile
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public string FullPath { get; set; }

        public DateTime DateCreated { get; set; }

        public int? Size { get; set; }

        public bool IsEncoded { get; set; }

        public DateTime? DateEncoded { get; set; }
    }
}