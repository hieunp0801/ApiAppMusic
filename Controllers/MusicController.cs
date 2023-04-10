using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiAppMusic.Models;
using ApiAppMusic.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using static System.Net.WebRequestMethods;

namespace ApiAppMusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController:ControllerBase
    {
        private readonly MusicDBContext _dbContext;
        private readonly IWebHostEnvironment _env;
        private FileUploadService fileUploadService = new FileUploadService();

        public MusicController(MusicDBContext dbContext,IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }
        //  Get all music song
        [HttpGet]
        public ActionResult<IEnumerable<Music>>  GetAll(){
            return _dbContext.musics.Include(m => m.Singer).ToList();
            /*
            Khi lấy dữ liệu từ _dbContext về thì sẽ ko trả về singer ( Đối tượng singer sẽ bị gán bằng null)
            sử dụng tính năng Eager Loading của Entity Framework để lấy Singer  
            sử dụng phương thức Include() trong câu lệnh truy vấn _dbContext.musics.Include(m => m.Singer).ToList();
            */
        }
        // Get music song by id
        [HttpGet("{id}")]
        public ActionResult<Music> GetById(int id) {
            var music = _dbContext.musics.Include(m => m.Singer).FirstOrDefault(m => id == m.Id );
            if(music == null){
                return NotFound();
            }
            return music;
        }
        // Add new music song
        [HttpPost]
        public async Task<IActionResult> AddMusicSong([FromForm] string musicName,[FromForm] IFormFile file,[FromForm] int idSinger ){
           
            Singer singer = _dbContext.singers.FirstOrDefault(s => idSinger == s.Id);
            if(singer == null){
                return NotFound();
            }
            string fileName = await fileUploadService.Upload(file,"music") ;
            Music music = new Music(){
                NameMusic = musicName,
                FileMusic = fileName,
                Singer = singer
            };
            _dbContext.musics.Add(music);
            _dbContext.SaveChanges();
            return Ok("Insert sucessfully");
        }
        [HttpPut("{idMusic}")]
        public async Task<IActionResult> ModifyMusicSong([FromRoute] int idMusic,[FromForm] string musicName,[FromForm] IFormFile file,[FromForm] int idSinger ){
           
            Singer singer = _dbContext.singers.FirstOrDefault(s => idSinger == s.Id);
            if(singer == null){
                return NotFound();
            }
            
            
            Music music = _dbContext.musics.FirstOrDefault(m => m.Id == idMusic );
            if(file != null){
                string fileName = await fileUploadService.Upload(file,"music");
                music.FileMusic = fileName;
            }
            music.NameMusic = musicName;
            music.Singer = singer;
            _dbContext.SaveChanges();
            return Ok("Update sucessfully");
        }
        [HttpDelete("{idMusic}")]
        public ActionResult DeleteMusicSong([FromRoute] int idMusic){
            var music = _dbContext.musics.FirstOrDefault(m => idMusic == m.Id );
            if(music == null){
                return NotFound();
            }
            _dbContext.musics.Remove(music);
            _dbContext.SaveChanges();
            return Ok("Delete Sucessfully");
        }
        // Upload
        // [HttpPost("/upload")]
        // public async Task<IActionResult> Upload(IFormFile file)
        // {
        //     string res = await fileUploadService.Upload(file,"uploads");
        //     return Ok(res);
        // }
        // End upload

        // GET IMAGE
            // [HttpGet("/image/{adr}")]
            // public ActionResult<byte[]> GetImage([FromRoute] string adr)
            // {
            //     var filePath = Path.Combine(_env.WebRootPath, "uploads",adr);
            //     // return Ok(filePath);
            //     return File(System.IO.File.ReadAllBytes(filePath), "image/jpg");
            // }
        // END// 
    
    }
}