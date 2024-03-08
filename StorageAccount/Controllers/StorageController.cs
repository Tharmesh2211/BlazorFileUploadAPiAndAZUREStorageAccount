using Microsoft.AspNetCore.Mvc;
using StorageAccount.Application.IServices.AzureService;
using StorageAccount.Application.IServices.DataBaseService;
using StorageAccount.Domain.Model;


namespace StorageAccount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IAzureService _FileStore;
        private readonly IDataBaseService _DatabaseStore;
        private readonly IConfiguration _Configuration;

        public StorageController(IAzureService fileStore, IConfiguration configuration, IDataBaseService databaseStore)
        {
            _FileStore = fileStore;

            _Configuration = configuration;
            
            _DatabaseStore = databaseStore;

        }

        [HttpPost("UploadBlobFile")]
        public async Task<IActionResult> UploadBlobFiles(IFormFile file)
        {
            try
            {
                var azureConnectionString = _Configuration.GetConnectionString("AzureStorageConnection");

                var azureContainerName = _Configuration["AzureStorageSettings:ContainerName"];

                Guid guidName = await _FileStore.UploadFileInAzureStorage(file, azureConnectionString, azureContainerName);

                if (guidName != Guid.Empty)
                {
                    if (await _DatabaseStore.UploadFileInDB(file, guidName))
                    {
                        return Ok("File Uploaded Successfully in Azure and Database!");
                    }
                }

                return BadRequest("Failed to upload file!");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetBlobFile")]
        public async Task<byte[]> GetBlobFile(string Name)
        {
            try
            {
                var azureConnectionString = _Configuration.GetConnectionString("AzureStorageConnection");

                var azureContainerName = _Configuration["AzureStorageSettings:ContainerName"];

                string fileGuid = await _DatabaseStore.SearchFileInDB(Name);

                if (fileGuid.Any())
                {
                    return await _FileStore.GetBlobFile(fileGuid, azureConnectionString, azureContainerName);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost("UploadQueue")]
        public async Task<string> PostQueue(FileOrBlobUpload fileOrBlobUpload)
        {
            var azureConnectionString = _Configuration.GetConnectionString("AzureStorageConnection");

            var azureContainerName = _Configuration["AzureStorageSettings:QueueName"];

            return await _FileStore.PostQueue(fileOrBlobUpload, azureConnectionString, azureContainerName); 

        }

        [HttpPost("UploadFileShares")]
        public async Task<IActionResult> UploadFileShares(IFormFile formFile)
        {

            var azureConnectionString = _Configuration.GetConnectionString("AzureStorageConnection");

            var azureContainerName = _Configuration["AzureStorageSettings:FileSharesName"];

            string dirName = _Configuration["AzureStorageSettings:dirName"];

            var result = await _FileStore.UploadFileShares(formFile, azureConnectionString, azureContainerName, dirName);                      

            if(result.GetHashCode() == 0) 
            { 
                return BadRequest("Failed to Upload File");
            }
            return Ok(result);
        }
    }
}
