using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAppMusic.Models;
using ApiAppMusic.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAppMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController: ControllerBase
    {
        MusicDBContext _dbContext;
        IWebHostEnvironment _env;
        private FileUploadService fileUploadService = new FileUploadService();
        public PlaylistController(MusicDBContext dBContext, IWebHostEnvironment env){
            _dbContext = dBContext;
            _env = env;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Playlist>>  GetAll(){
            return _dbContext.playlists.ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Music>> GetById([FromRoute] int id){
            var singers = _dbContext.singers;
            var musicPlaylists = _dbContext.musicPlaylists.Where(m => m.IdPlaylist == id)
            .Include( m => m.Music);
            // var musics = musicPlaylists.Select(m => m.Music).ToList();
            var musics = musicPlaylists.Select( m => m.Music).ToList();
            return musics;
        }
        [HttpGet("{id}/musics")]
        public ActionResult<IEnumerable<Music>> GetAllMusic ([FromRoute] int id){
           var res = _dbContext.musicPlaylists
            .Where(mp => mp.IdPlaylist == id)
            .Include(mp => mp.Music)
            .ThenInclude(m => m.Singer);
            return res.Select(m => m.Music).ToList();

        }

        [HttpPost]
        public async Task<ActionResult> AddPlayList([FromForm] string name,[FromForm] IFormFile file){
            string filepath= await fileUploadService.Upload(file,"playlist");
            Playlist playlist = new Playlist(){
                Name = name,
                Url = filepath
            };
            _dbContext.Add(playlist);
            _dbContext.SaveChanges();
            return Ok("Insert successfully");
        }
        [HttpPost("add")]
        public ActionResult AddMusicSongToPlaylist([FromForm] int IdMusic,[FromForm ]int IdPlaylist){
            Playlist playlist = _dbContext.playlists.FirstOrDefault(p => p.Id == IdPlaylist);
            if(playlist == null)
                return NotFound();
            Music music = _dbContext.musics.FirstOrDefault(m => m.Id == IdMusic);
            if(music == null)
                return NotFound();
            MusicPlaylist musicPlaylist = new MusicPlaylist(){
                IdMusic = IdMusic,
                IdPlaylist = IdPlaylist,
                Music = music,
                Playlist = playlist
            };
            _dbContext.Add(musicPlaylist);
            _dbContext.SaveChanges();

            return Ok("Insert successfully");
        }
        [HttpDelete("{idPlaylist}/music/{idMusic}")]
        public ActionResult DeleteMusicFromPlaylist([FromRoute] int IdMusic,[FromRoute] int IdPlaylist){
            var playlist = _dbContext.musicPlaylists.FirstOrDefault(mp => mp.IdPlaylist == IdPlaylist && mp.IdMusic == IdMusic);
            if(playlist == null)
                return NotFound();
            _dbContext.musicPlaylists.Remove(playlist);
            _dbContext.SaveChanges();
            return Ok("Delete Successfully");
        }

    }
}