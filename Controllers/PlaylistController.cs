using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiAppMusic.Models;
using ApiAppMusic.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiAppMusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController: ControllerBase
    {
        MusicDBContext _dbContext;
        IWebHostEnvironment _env;
        IConfiguration _configuration;
        UserManager<ApplicationUser> _manageUser;
        UserService _userService;
        private FileUploadService fileUploadService = new FileUploadService();
        public PlaylistController(UserService userService,MusicDBContext dBContext, IWebHostEnvironment env, IConfiguration configuration,UserManager<ApplicationUser> manageUser){
            _dbContext = dBContext;
            _env = env;
            _configuration = configuration;
            _manageUser = manageUser;
            _userService = userService;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Playlist>>>  GetAll(){
            var user = await _userService.getUserLogin(_manageUser,_configuration,HttpContext);
            return _dbContext.playlists.Where(p => p.user == user).ToList();
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
        [Authorize]
        public ActionResult<IEnumerable<Music>> GetAllMusicByIdPlaylist ([FromRoute] int id){
           var res = _dbContext.musicPlaylists
            .Where(mp => mp.IdPlaylist == id)
            .Include(mp => mp.Music)
            .ThenInclude(m => m.Singer);
            return res.Select(m => m.Music).ToList();

        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddPlayList([FromForm] string name,[FromForm] IFormFile file){
            string filepath= await fileUploadService.Upload(file,"playlist");
            var user = await _userService.getUserLogin(_manageUser,_configuration,HttpContext);
            Playlist playlist = new Playlist(){
                Name = name,
                Url = filepath,
                user = user
            };
            _dbContext.Add(playlist);
            _dbContext.SaveChanges();
            return Ok("Insert successfully");
        }
        [HttpPost("add")]
        [Authorize]
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
        [Authorize]
        public ActionResult DeleteMusicFromPlaylist([FromRoute] int IdMusic,[FromRoute] int IdPlaylist){
            var playlist = _dbContext.musicPlaylists.FirstOrDefault(mp => mp.IdPlaylist == IdPlaylist && mp.IdMusic == IdMusic);
            if(playlist == null)
                return NotFound();
            _dbContext.musicPlaylists.Remove(playlist);
            _dbContext.SaveChanges();
            return Ok("Delete Successfully");
        }
        [Authorize]
        [HttpGet("/test")]
        public async Task<ActionResult> TestAuthorize(){
            
            // var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            // var tokenHandler = new JwtSecurityTokenHandler();
            // var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            // var tokenDescriptor = new SecurityTokenDescriptor
            // {
            //     Subject = new ClaimsIdentity(new Claim[]
            //     {
            //         new Claim(ClaimTypes.Name, "username")
            //     }),
            //     Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["Jwt:ExpiresInMinutes"])),
            //     SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            // };

            // try
            // {
            //     var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
            //     {
            //         ValidateIssuer = true,
            //         ValidateAudience = true,
            //         ValidateLifetime = true,
            //         ValidateIssuerSigningKey = true,
            //         ValidIssuer = _configuration["Jwt:Issuer"],
            //         ValidAudience = _configuration["Jwt:Audience"],
            //         IssuerSigningKey = new SymmetricSecurityKey(key)
            //     }, out SecurityToken validatedToken);

            //     var username = claims.FindFirst(ClaimTypes.Name)?.Value;
            //     var user = await _manageUser.FindByNameAsync(username);
            //     // var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //     return Ok(user);
            //     // sử dụng username để lấy thông tin user trong database hoặc nơi khác
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine(ex.Message);
            //     // xử lý nếu token không hợp lệ
            // }
            // return Ok("truy cap thanh cong");
            var user = await _userService.getUserLogin(_manageUser,_configuration,HttpContext);
            return Ok(user);
            
            
        }

    }
}