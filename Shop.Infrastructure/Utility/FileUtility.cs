using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shop.Core.Entites;
using Shop.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Utility
{
    public class FileUtility
    {
        private readonly Configs options;
        private readonly IHttpContextAccessor httpContextAccessor;
        public FileUtility(IOptions<Configs> options, IHttpContextAccessor httpContextAccessor)
        {
            this.options = options.Value;
            this.httpContextAccessor = httpContextAccessor;
        }
        public string SaveFileInFolder<IEntity>(IFormFile file)
        {
            string path = CheckAndCreateDirectory(typeof(IEntity).Name);
            var newFileName = $"{DateTime.Now.Ticks}{GetfileExtension(file.FileName)}";
            string newFilePath = Path.Combine(path, newFileName);
            var byteArray = EncryptFile(ConvertToByteArray(file));
            using var writer = new BinaryWriter(File.OpenWrite(newFilePath));
            writer.Write(byteArray);
            return newFileName;
        }

        public string GetFileFullPath(string fileName, string entity)
        {
            var appRootPath = options.CurrentDirectory;
            var mediaRootPath = options.MediaPath;
            return Path.Combine(appRootPath, mediaRootPath, entity, fileName);

        }

        public string GetFileUrl<IEntity>(string thumbnailFileName)
        {
            string site = httpContextAccessor.HttpContext.Request.Host.Value;
            var customMediaPath = options.MediaPath.Replace("\\", "/");
            return $"https://{site}/{customMediaPath}/{typeof(IEntity).Name}/{thumbnailFileName}";
        }

        private string CheckAndCreateDirectory(string entityName)
        {
            //options.MediaPath
            var location = string.IsNullOrEmpty(options.CurrentDirectory) ? Directory.GetCurrentDirectory() : options.CurrentDirectory;
            var path = Path.Combine(location, options.MediaPath, entityName);
            var exist = !Directory.Exists(path);
            if (exist)
                Directory.CreateDirectory(path);
            return path;
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


        public byte[] EncryptFile(byte[] fileContent)
        {
            string EncryptionKey = options.FileEncriptionKey;
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(fileContent, 0, fileContent.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }
        public byte[] DecryptFile(byte[] fileContent)
        {
            string EncryptionKey = options.FileEncriptionKey;
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);


                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(fileContent, 0, fileContent.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        public string ConvertToBase64(byte[] data)
        {
            return Convert.ToBase64String(data);
        }
    }
}
