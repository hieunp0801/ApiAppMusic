using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAppMusic.Models;
using ApiAppMusic.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAppMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SingerController:ControllerBase
    {
        private readonly MusicDBContext _dbContext;
        private readonly IWebHostEnvironment _env;
        private FileUploadService fileUploadService = new FileUploadService();
        public SingerController(MusicDBContext dbContext,IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Singer>>  GetAll(){
            return _dbContext.singers.ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<Singer> GetById(int id){
            var singer = _dbContext.singers.FirstOrDefault(s => s.Id == id);
            if(singer == null){
                return NotFound();
            }
            return singer;
        }
        [HttpPost]
        public async Task<IActionResult> AddSinger([FromForm] IFormFile file, [FromForm] string name, [FromForm] string dob){
            string path = await fileUploadService.Upload(file,"image_singer");
            Singer singer = new Singer {
                NameSinger = name,
                ImageSinger = path,
                DateofBirth = dob
            };
            _dbContext.singers.Add(singer);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = singer.Id }, singer);
           
            // return Ok("upload thanh cong");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifySinger([FromRoute] int id,[FromForm] IFormFile file, [FromForm] string name, [FromForm] string dob){
            var singer =  _dbContext.singers.FirstOrDefault(s => s.Id == id);
            if(singer == null){
                return NotFound();
            }
            if(file != null){
                string path = await fileUploadService.Upload(file,"image_singer");
                singer.ImageSinger = path;
            }
            singer.NameSinger = name;
            singer.DateofBirth = dob;
            _dbContext.SaveChanges();
            return Ok("Update sucessfully");
        }
        [HttpDelete("{id}")]
        public ActionResult deleteSinger([FromRoute] int id){
            var singer =  _dbContext.singers.FirstOrDefault(s => s.Id == id);
            if(singer == null){
                return NotFound();
            }
            _dbContext.singers.Remove(singer);
            _dbContext.SaveChanges();
            return Ok("Delete sucessfully");
        }
    }
}