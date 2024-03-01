using Microsoft.AspNetCore.Mvc;
using StorageAccount.Application.IServices;

namespace StorageAccount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureFileStoreController : ControllerBase
    {
       private readonly IFileUpload _fileStore;
        public static IWebHostEnvironment _hostEnvironment;
        public AzureFileStoreController(IFileUpload fileStore, IWebHostEnvironment hostEnvironment)
        {
            _fileStore = fileStore;
            _hostEnvironment = hostEnvironment;

        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (_fileStore.CreateDirectory(file))
                {
                    _fileStore.FileDismantle();

                    if (await _fileStore.UploadFileInDirectory())
                    {
                        if (await _fileStore.UploadFileInAzureStorage())
                        {
                            if (await _fileStore.UploadFileInDB(file))
                            {
                                return Ok("File Uploaded in everywhere!");
                            }
                        }

                    }

                }
                return Ok("Failed to upload file!");
              
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
    }
