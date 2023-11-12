using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MVCHamburgerciOtomasyonu.Entity.Enums;
using MVCHamburgerciOtomasyonu.Service.DTOs.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.Helpers.Images
{
    public class ImageHelper : IImageHelper
    {
        private readonly string wwwroot;
        private readonly IWebHostEnvironment _env;
        private const string imgFolder = "images";
        private const string menuImagesFolder = "menu-images";
        private const string extraImagesFolder = "extra-images";
        private const string userImagesFolder = "user-images";

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            wwwroot = env.WebRootPath;
        }
        public void Delete(string imageName)
        {
            var fileToDelete = Path.Combine($"{wwwroot}/{imgFolder}/{imageName}");
            if (File.Exists(fileToDelete))
            {
                File.Delete(fileToDelete);
            }
        }

        public async Task<ImageUploadedDto> Upload(string name, IFormFile imageFile, ImageType imageType, string folderName = null)
        {
            if (imageType == ImageType.User)
            {
                folderName = userImagesFolder;
            }
            else if (imageType == ImageType.Menu)
            {
                folderName = menuImagesFolder;
            }
            else
            {
                folderName = extraImagesFolder;
            }


            if (!Directory.Exists($"{wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{wwwroot}/{imgFolder}/{folderName}");
            }


            string oldFileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            string firstThreeCharacters = oldFileName.Substring(0, 3);
            string fileExtension = Path.GetExtension(imageFile.FileName);

            name = ReplaceInvalidChars(name);

            DateTime dateTime = DateTime.Now;

            string newFileName = $"{name}_{dateTime.Millisecond}_{firstThreeCharacters}{fileExtension}";

            var path = Path.Combine($"{wwwroot}/{imgFolder}/{folderName}", newFileName);

            await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
            await imageFile.CopyToAsync(stream);
            await stream.FlushAsync();

            if (imageType == ImageType.User)
            {
                string message = $"{newFileName} isimli kullanıcı resmi başarı ile eklenmiştir.";
            }
            else if (imageType == ImageType.Menu)
            {
                string message = $"{newFileName} isimli Menu resmi başarı ile eklenmiştir.";
            }
            else
            {
                string message = $"{newFileName} isimli Ekstra Malzeme resmi başarı ile eklenmiştir.";
            }



            return new ImageUploadedDto()
            {
                FullName = $"{folderName}/{newFileName}"
            };
        }


        private string ReplaceInvalidChars(string fileName)
        {
            return fileName.Replace("İ", "I")
                 .Replace("ı", "i")
                 .Replace("Ğ", "G")
                 .Replace("ğ", "g")
                 .Replace("Ü", "U")
                 .Replace("ü", "u")
                 .Replace("ş", "s")
                 .Replace("Ş", "S")
                 .Replace("Ö", "O")
                 .Replace("ö", "o")
                 .Replace("Ç", "C")
                 .Replace("ç", "c")
                 .Replace("é", "")
                 .Replace("!", "")
                 .Replace("'", "")
                 .Replace("^", "")
                 .Replace("+", "")
                 .Replace("%", "")
                 .Replace("/", "")
                 .Replace("(", "")
                 .Replace(")", "")
                 .Replace("=", "")
                 .Replace("?", "")
                 .Replace("_", "")
                 .Replace("*", "")
                 .Replace("æ", "")
                 .Replace("ß", "")
                 .Replace("@", "")
                 .Replace("€", "")
                 .Replace("<", "")
                 .Replace(">", "")
                 .Replace("#", "")
                 .Replace("$", "")
                 .Replace("½", "")
                 .Replace("{", "")
                 .Replace("[", "")
                 .Replace("]", "")
                 .Replace("}", "")
                 .Replace(@"\", "")
                 .Replace("|", "")
                 .Replace("~", "")
                 .Replace("¨", "")
                 .Replace(",", "")
                 .Replace(";", "")
                 .Replace("`", "")
                 .Replace(".", "")
                 .Replace(":", "")
                 .Replace(" ", "");
        }
    }
}
