using Microsoft.EntityFrameworkCore;

namespace ApiAppMusic.Models
{
    public class MusicDBContext: DbContext
    {
        public MusicDBContext(DbContextOptions<MusicDBContext> options)
        : base(options){}
        protected override void OnConfiguring(DbContextOptionsBuilder builder){
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MusicPlaylist>().HasKey( mp => new {mp.IdMusic,mp.IdPlaylist});
           
            modelBuilder.Entity<MusicPlaylist>().HasOne(mp => mp.Music)
            .WithMany(m => m.MusicPlaylists)
            .HasForeignKey(mp => mp.IdMusic);

            modelBuilder.Entity<MusicPlaylist>()
            .HasOne(mp => mp.Playlist)

            .WithMany(p => p.MusicPlaylists)
            .HasForeignKey(mp => mp.IdPlaylist);
        }
        public DbSet<Music> musics {get;set;}
        public DbSet<Singer> singers {get;set;}

        public DbSet<Playlist> playlists {get;set;}

        public DbSet<MusicPlaylist> musicPlaylists {get;set;}
    }
}