using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shop.Core.Entites;
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
        private readonly IHttpContextAccessor httpContextAccessor;

        public FileUtility(IOptions<Configs> options,IHttpContextAccessor httpContextAccessor)
        {
            this.options = options.Value;
            this.httpContextAccessor = httpContextAccessor;
        }
        public string SaveFileInFolder<IEntity>(IFormFile file, IEntity entity)
        {
            var applicationExecutionRootPath = options.CurrentDirectory;
            var mediaRootPath = options.MediaPath;
            var folderRootPath = nameof(entity);

            CheckAndCreateDirectory(applicationExecutionRootPath, mediaRootPath, folderRootPath);
            var newFileName = $"{DateTime.Now.Ticks}{GetfileExtension(file.FileName)}";

            string newFilePath = Path.Combine(applicationExecutionRootPath, mediaRootPath, folderRootPath, newFileName);
            using var writer = new BinaryWriter(File.OpenWrite(newFilePath));
            writer.Write(ConvertToByteArray(file));

            return newFileName;
        }

        private void CheckAndCreateDirectory(string applicationExecutionRootPath, string mediaRootPath, string folderRootPath)
        {
            string fullPath = Path.Combine(applicationExecutionRootPath, mediaRootPath);
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            string entityRootPath = Path.Combine(fullPath, folderRootPath);
            if (!Directory.Exists(entityRootPath))
                Directory.CreateDirectory(entityRootPath);
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

        public string GetFileUrl<IEntity>(string thumbnailFileName, IEntity entity)
        {
            string site = httpContextAccessor.HttpContext.Request.Host.Value;
        }

        public string ConvertToBase64(byte[] data)
        {
            return Convert.ToBase64String(data);
        }
    }
}
