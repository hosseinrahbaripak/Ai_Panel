using Microsoft.AspNetCore.Http;
using System.IO.Compression;

namespace Ai_Panel.Infrastructure.Tools
{
    internal static class StaticFileHelper
    {
        public static async Task<bool> SaveFile(string folderPath, string fileName, IFormFile file)
        {
            try
            {
                if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(fileName) || file == null)
                {
                    return false;
                }
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string filepath = Path.Combine(folderPath, fileName);
                await using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool DeleteFile(string filePath = "")
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    return false;
                }
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static async Task<bool> WriteOnTextFile(string folderPath, string filename, string content)
        {
            try
            {
                if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(content))
                {
                    return false;
                }

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fullPath = Path.Combine(folderPath, filename);
                if (!File.Exists(fullPath))
                {
                    var fs = File.Create(fullPath);
                    await fs.DisposeAsync();
                }
                content = content + " \n ";
                await File.AppendAllTextAsync(fullPath, content);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static void CheckDirectoryExistAndCrete(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public static bool CheckDirectoryExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return false;
            }
            return true;
        }
        public static async Task CopyFile(string path, IFormFile file)
        {
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
        public static async Task<bool> ZipFile(string sourcePath, string destinationPath)
        {
            try
            {
                if (!File.Exists(sourcePath))
                {
                    return false;
                }
                await using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
                {
                    await using (FileStream zipStream = new FileStream(destinationPath, FileMode.Create))
                    {
                        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
                        {
                            ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(sourcePath));
                            using (Stream entryStream = entry.Open())
                            {
                                await sourceStream.CopyToAsync(entryStream);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");
                var logFileName = "backuplogs.txt";
                await WriteOnTextFile(logFilePath, logFileName, e.Message + " : " + DateTime.UtcNow.AddHours(3.5));
                return false;
            }
        }
    }

}
