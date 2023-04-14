using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ApiAppMusic.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name {get;set;}
        public string DateOfBirth {get;set;}
        public string Url {get;set;}
        public List<Playlist> Playlists;
    }
}