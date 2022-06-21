using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helpers
{
    public static class Extension
    {
        public static bool CheckFileSize(this IFormFile file,int kb)
        {
            return file.Length / 1024 <= 200;
        }
        public static bool CheckFileType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
        public async static Task< string >SaveFileAysnc(this IFormFile file, string root,params string[] folders)
        {

            var fileName = Guid.NewGuid().ToString() + file.FileName;
            var resultPath = Path.Combine(Helper.GetPath(root,folders), fileName);
            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);

            }
            return fileName;
        }
    }
}
