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
            return _dbContext.musics.ToList();
        }
        // Get music song by id
        [HttpGet("{id}")]
        public ActionResult<Music> GetById(int id) {
            var music = _dbContext.musics.FirstOrDefault(m => id == m.Id );
            if(music == null){
                return NotFound();
            }
            return music;
        }
        // Add new music song
        [HttpPost]
        public IActionResult AddMusicSong(Music music){
            _dbContext.musics.Add(music);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = music.Id }, music);
        }
        // Upload
        [HttpPost("/upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string res = await fileUploadService.Upload(file,"uploads");
            return Ok(res);
        }
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