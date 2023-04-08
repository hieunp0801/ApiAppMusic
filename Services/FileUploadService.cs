using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAppMusic.Services
{
    public class FileUploadService
    {
        public string GenerateNameFile(){
             // Random name music
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToLower();
            Random random = new Random();
            string randomString = new string(
                Enumerable.Repeat(chars, 30)
                        .Select(s => s[random.Next(s.Length)])
                        .ToArray());
            // Close 
            return randomString;
        }
        public async Task<string> Upload(IFormFile file,string folder)
        { 

            if (file == null || file.Length == 0)
            {
                return "No file selected.";
            }
            string randomString = GenerateNameFile();
            var fileNameExtension = Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",folder, $"{randomString}{fileNameExtension}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"{randomString}{fileNameExtension}";

        }

    }
}