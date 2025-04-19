using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Helpers
{
    public static class UploadFiles
    {
        public static async Task<string> UploadFile(IFormFile file, string folderName,string wwwroot)
        {

            // Unique Name
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString() + extension;

            // Path Image 
            var imagePath = Path.Combine(wwwroot, "Images/" + folderName);

            // Check if folder Exist
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            // Full Path 
            var filePath = Path.Combine(imagePath, fileName);

            try
            {
                await using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Uploading The File Error : " + ex.Message;
            }
            return "Images/" + folderName + "/" + fileName;
        }
    }
}
