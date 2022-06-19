using Microsoft.AspNetCore.Http;
using Shop.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Utility
{
    public static class FileUtility
    {
        public static readonly string PROJECT_ROOT = @$"{Directory.GetCurrentDirectory()}\wwwroot\products";
        public static SaveFile SaveAndGetFileModel(this IFormFile file, string path)
        {
            var result = new SaveFile();
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                var fileByteArray = target.ToArray();
                result.File = fileByteArray;
            }

            FileInfo fileInfo = new FileInfo(file.FileName);
            string FileName = file.FileName + fileInfo.Extension;

            result.FileExtension = fileInfo.Extension;
            result.FileName = fileInfo.Name;
            result.FileSize = file.Length;
            
            string fileNameWithPath = Path.Combine(path, FileName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return result;
        }
    }
}
