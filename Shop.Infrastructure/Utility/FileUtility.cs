using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shop.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Utility
{
    public class FileUtility
    {
        private readonly Configs options;
        public FileUtility(IOptions<Configs> options)
        {
            this.options = options.Value;
        }
        public string SaveFileInFolder<IEntity>(IFormFile file, IEntity entity)
        {
            var applicationExecutionRootPath = options.CurrentDirectory;
            var mediaRootPath = options.MediaPath;
            var folderRootPath = nameof(entity);
            var newFileName = $"{DateTime.Now.Ticks}{GetfileExtension(file.FileName)}";
            return "";
        }

        public void CheckAndCreateDirectory(string applicationExecutionRootPath, string mediaRootPath,string folderRootPath)
        {
            //if(!Directory.Exists())

        }
        public byte[] ConvertToByteArray(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public string GetfileExtension(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            return fileInfo.Extension;
        }
        //public static readonly string PROJECT_ROOT = @$"{Directory.GetCurrentDirectory()}\wwwroot\products";
        //public static SaveFile SaveAndGetFileModel(this IFormFile file, string path)
        //{
        //    var result = new SaveFile();
        //    using (var target = new MemoryStream())
        //    {
        //        file.CopyTo(target);
        //        var fileByteArray = target.ToArray();
        //        result.File = fileByteArray;
        //    }

        //    FileInfo fileInfo = new FileInfo(file.FileName);
        //    string FileName = file.FileName + fileInfo.Extension;

        //    result.FileExtension = fileInfo.Extension;
        //    result.FileName = fileInfo.Name;
        //    result.FileSize = file.Length;

        //    string fileNameWithPath = Path.Combine(path, FileName);

        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }


        //    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
        //    {
        //        file.CopyTo(stream);
        //    }

        //    return result;
        //}
    }
}
