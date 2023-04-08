using System.Collections.Generic;
using System.Linq;
using ApiAppMusic.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiAppMusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController:ControllerBase
    {
        private readonly MusicDBContext _dbContext;

        public MusicController(MusicDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public static List<Music> musics = new List<Music>();
        [HttpGet]
        public ActionResult<IEnumerable<Music>>  GetAll(){
            return _dbContext.musics.ToList();
        }
    }
}