using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using StorageAccount.Application.IServices;
using StorageAccount.Domain.Model;

namespace StorageAccount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureFileStoreController : ControllerBase
    {
       private readonly IAzureFileStore _fileStore;
        public static IWebHostEnvironment _hostEnvironment;
        public AzureFileStoreController(IAzureFileStore fileStore, IWebHostEnvironment hostEnvironment)
        {
            _fileStore = fileStore;
            _hostEnvironment = hostEnvironment;

        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var result = await _fileStore.UploadFile(file);
                return Ok(result);
              
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
    }
