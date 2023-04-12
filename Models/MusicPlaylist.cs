using System.ComponentModel.DataAnnotations;

namespace ApiAppMusic.Models
{
    public class MusicPlaylist
    {
        [Key]
        public int Id {get;set;}
        public int IdMusic {get;set;}
        public Music Music {get;set;}
        public int IdPlaylist {get;set;}
        public Playlist Playlist {get;set;}
    }
}