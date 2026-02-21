using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {

        private readonly string[] AllowedExtentions = {".jpg" ,".jpeg",".png" };
        private readonly long MaxFileSize = 5 * 1024 * 1024;
        private readonly IWebHostEnvironment _webHostEnvironment; 

        public AttachmentService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment; 
        }

        public string Upload(string FolderName, IFormFile File)
        {
            try
            {
                if (FolderName is null || File is null || File.Length == 0) return null!;

                if (File.Length > MaxFileSize) return null!;

                var Extention = Path.GetExtension(File.FileName).ToLower();

                if (!AllowedExtentions.Contains(Extention)) return null!;

                //D:\backend engineering\MVC CATEGORY\GymManagementSystemSolution\GymManagementPL\wwwroot\Images\

                var FolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", FolderName);

                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                var FileName = Guid.NewGuid().ToString() + Extention;

                var FilePath = Path.Combine(FolderPath, FileName);

                using var FileStraem = new FileStream(FilePath, FileMode.Create);

                File.CopyTo(FileStraem);


                return FileName;
            }
            catch (Exception ex )
            {
                Console.WriteLine($"Failed To Upload File To Folder ={FolderName} : {ex}");
                return null!; 
            }


        }


        public bool Delete(string FileName, string FolderName)
        {
            try
            {
                if (string.IsNullOrEmpty(FileName) || string.IsNullOrEmpty(FolderName)) return false;
                var FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", FileName, FolderName); 
                if(Directory.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    return true;
                    
                }
                return false; 

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed To Delete File With  Name ={FileName} : {ex}");
                return false; 
            }
        }
    }
}
