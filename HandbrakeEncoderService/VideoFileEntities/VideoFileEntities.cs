namespace VideoFilesEntityModel
{
    using System.Data.Entity;

    public partial class VideoFileEntities : DbContext
    {
        public VideoFileEntities()
            : base("name=VideoFileEntities")
        {
        }

        public virtual DbSet<VideoFile> VideoFiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}