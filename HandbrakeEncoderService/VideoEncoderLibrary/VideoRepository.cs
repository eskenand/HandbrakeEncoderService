using System.Collections.Generic;
using System.Linq;
using VideoFilesEntityModel;


namespace VideoEncoderLibrary
{
    public class VideoRepository
    {
        private VideoFileEntities _context;

        public VideoRepository()
        {
            _context = new VideoFileEntities();
        }

        public string ConnString => _context.Database.Connection.ConnectionString;

        public void InsertVideo(VideoFile file)
        {
            if (!IsFileNameInDb(file))
            {
                _context.VideoFiles.Add(file);
                _context.SaveChanges();
            }
        }

        public void UpdateVideo(int fileid)
        {
            var fileInDb = _context.VideoFiles.SingleOrDefault(v => v.Id == fileid);
            if (fileInDb != null)
            {
                _context.Entry(fileInDb).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public bool IsFileNameInDb(VideoFile file)
        {
            var fileInDb = _context.VideoFiles.SingleOrDefault(v => v.FileName == file.FileName);
            return fileInDb != null;
        }

        public VideoFile GetNextVideoToEncode()
        {
            var video = _context.VideoFiles.Where(v => v.IsEncoded == false)
                .OrderBy(v => v.DateCreated).FirstOrDefault();
            return video;
        }

        public List<VideoFile> GetAllVideoFiles()
        {
            return _context.VideoFiles.ToList();
        }
    }
}