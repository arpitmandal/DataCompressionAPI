using DataCompressionAPI.Services;
using DataCompressionAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataCompressionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly CompressionService _compressionService;
        private readonly AppDbContext _context;

        public ProcessController(CompressionService compressionService, AppDbContext context)
        {
            _compressionService = compressionService;
            _context = context;
        }

        [Authorize]
        [HttpPost("upload-and-download")]
        public IActionResult UploadAndDownload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "No file selected. Please select a valid file to upload." });
            }

            // Extract username from the JWT token
            var username = User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User identity could not be determined from token.");
            }

            // Find the user ID based on the username
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            int createdBy = user.Id;

            string fileType;
            string errorMessage;

            // Process the file via CompressionService
            var fileData = _compressionService.ProcessFile(file, out fileType, out errorMessage);

            // Calculate size metrics using CompressionService
            var initialSizeReadable = _compressionService.ConvertSizeToReadableFormat(file.Length);
            var finalSizeReadable = fileData != null ? _compressionService.ConvertSizeToReadableFormat(fileData.Length) : "N/A";
            var reducedSizePercentage = file.Length > 0
                ? ((double)(file.Length - (fileData?.Length ?? file.Length)) / file.Length * 100).ToString("F2") + "%"
                : "N/A";

            // Log details to FileLog table
            var logSuccess = _compressionService.SaveFileLog(
                fileName: file.FileName,
                fileType: fileType,
                initialSize: initialSizeReadable,
                reducedSize: reducedSizePercentage,
                finalSize: finalSizeReadable,
                createdBy: createdBy,
                error: errorMessage
            );

            if (!logSuccess)
            {
                return StatusCode(500, "An error occurred while saving file log.");
            }

            // Handle errors during file processing
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return BadRequest(new { message = errorMessage });
            }

            // Return the file for download
            return File(fileData, "application/octet-stream", file.FileName);
        }
    }
}