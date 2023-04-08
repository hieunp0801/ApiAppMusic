using Microsoft.EntityFrameworkCore;

namespace ApiAppMusic.Models
{
    public class MusicDBContext: DbContext
    {
        public MusicDBContext(DbContextOptions<MusicDBContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder){
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Music> musics {get;set;}
    }
}