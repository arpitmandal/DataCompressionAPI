using DataCompressionAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataCompressionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly CompressionService _compressionService;
        public ProcessController(CompressionService compressionService)
        {
            _compressionService = compressionService;
        }

        [Authorize]
        [HttpPost("compress")]
        public IActionResult CompressFile()
        {
            // Placeholder logic for file compression
            return Ok("File compressed successfully");
        }

        [Authorize]
        [HttpGet("status")]
        public IActionResult GetCompressionStatus()
        {
            // Placeholder logic for checking compression status
            return Ok("Compression status retrieved successfully");
        }
    }
}