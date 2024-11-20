using System;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using DataCompressionAPI.Data;

namespace DataCompressionAPI.Services
{
    public class CompressionService
    {
        private readonly AppDbContext _context;

        public CompressionService(AppDbContext context)
        {
            _context = context;
        }

        public string GetFileType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            return extension switch
            {
                ".jpg" or ".jpeg" or ".png" or ".gif" => "image",
                ".mp4" or ".avi" or ".mov" => "video",
                ".pdf" => "pdf",
                ".doc" or ".docx" => "word",
                _ => "Unknown"
            };
        }

        public byte[] ProcessFile(IFormFile file, out string fileType, out string errorMessage)
        {
            errorMessage = null;
            fileType = GetFileType(file.FileName);

            if (fileType == "Unknown")
            {
                errorMessage = "Unsupported file type. Allowed types are image, video, pdf, and word.";
                return null;
            }

            try
            {
                switch (fileType)
                {
                    case "image":
                        return CompressImage(file, out errorMessage);
                    case "video":
                        errorMessage = "Video compression is not implemented yet.";
                        return null; // Placeholder for video compression logic
                    case "pdf":
                        return CompressPdf(file, out errorMessage);
                    case "word":
                        return CompressWord(file, out errorMessage);
                    default:
                        errorMessage = "Unsupported file type.";
                        return null;
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"An error occurred while processing the file: {ex.Message}";
                return null;
            }
        }

        private byte[] CompressImage(IFormFile file, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var inputStream = file.OpenReadStream();
                using var image = Image.Load(inputStream);
                image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2)); // Example resizing (50% smaller)
                using var outputStream = new MemoryStream();
                image.SaveAsJpeg(outputStream); // Save as JPEG for compression
                return outputStream.ToArray();
            }
            catch (Exception ex)
            {
                errorMessage = $"Image compression failed: {ex.Message}";
                return null;
            }
        }

        private byte[] CompressPdf(IFormFile file, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var inputStream = file.OpenReadStream();
                using var outputStream = new MemoryStream();
                using var zipStream = new GZipStream(outputStream, CompressionMode.Compress, true);
                inputStream.CopyTo(zipStream);
                zipStream.Close();
                return outputStream.ToArray();
            }
            catch (Exception ex)
            {
                errorMessage = $"PDF compression failed: {ex.Message}";
                return null;
            }
        }

        private byte[] CompressWord(IFormFile file, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var inputStream = file.OpenReadStream();
                using var outputStream = new MemoryStream();
                using var zipStream = new GZipStream(outputStream, CompressionMode.Compress, true);
                inputStream.CopyTo(zipStream);
                zipStream.Close();
                return outputStream.ToArray();
            }
            catch (Exception ex)
            {
                errorMessage = $"Word document compression failed: {ex.Message}";
                return null;
            }
        }

        public bool SaveFileLog(string fileName, string fileType, string initialSize, string reducedSize, string finalSize, int createdBy, string error)
        {
            try
            {
                var fileLog = new FileLog
                {
                    FileName = string.IsNullOrEmpty(fileName) ? "N/A" : fileName,
                    FileType = string.IsNullOrEmpty(fileType) ? "N/A" : fileType,
                    InitialSize = string.IsNullOrEmpty(initialSize) ? "N/A" : initialSize,
                    ReducedSize = string.IsNullOrEmpty(reducedSize) ? "N/A" : reducedSize,
                    FinalSize = string.IsNullOrEmpty(finalSize) ? "N/A" : finalSize,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = createdBy,
                    Error = string.IsNullOrEmpty(error) ? "No errors" : error
                };

                _context.FileLogs.Add(fileLog);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string ConvertSizeToReadableFormat(long sizeInBytes)
        {
            double size = sizeInBytes;
            string[] units = { "B", "KB", "MB", "GB", "TB" };
            int unitIndex = 0;

            while (size >= 1024 && unitIndex < units.Length - 1)
            {
                size /= 1024;
                unitIndex++;
            }

            return $"{size:F2} {units[unitIndex]}";
        }
    }
}