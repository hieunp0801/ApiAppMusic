using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiAppMusic.Models
{
    public class Playlist
    {
        [Key]
        public int Id {get;set;}
        public string Name {get;set;}
        public string Url {get;set;}
        
        public IEnumerable<MusicPlaylist> MusicPlaylists{get;set;}

        public ApplicationUser user {get;set;}
    }
}