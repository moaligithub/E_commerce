using E_commerce.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Services
{
    public class FileHelper
    {
        // UpLoad
        public byte[] UpLoadeToDatabase(IFormFile file)
        {
            using MemoryStream memoryStream = new MemoryStream();
            
            file.CopyTo(memoryStream);
            
            return memoryStream.ToArray();
        }

        public string UpLoadeToServer(IFormFile file, string Folder, string WebRootPath)
        {
            string FileName = string.Empty;

            if (file != null)
            {
                string FolderPath = Path.Combine(WebRootPath, Folder);
                FileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string FullPath = Path.Combine(FolderPath, FileName);

                using FileStream fileStream = new FileStream(FullPath, FileMode.Create);
                file.CopyTo(fileStream);
            }

            return FileName;
        }
         
        // Delete
        public bool DeleteFromServer(string FileName, string Folder, string WebRootPath)
        {
            try
            {
                string FolderPath = Path.Combine(WebRootPath, Folder);
                string FullPath = Path.Combine(FolderPath, FileName);

                System.IO.File.Delete(FullPath);
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        // More Files Services
        public bool ValidExtension(IFormFile file, string[] Extensions)
            => Extensions.Contains(Path.GetExtension(file.FileName));

        public bool ValidSize(IFormFile file, long MaxSize)
            => file.Length <= MaxSize;
    }
}
