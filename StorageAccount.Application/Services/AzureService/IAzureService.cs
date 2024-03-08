using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageAccount.Domain.Model;
namespace StorageAccount.Application.IServices.AzureService
{
    public interface IAzureService
    {
        public Task<Guid> UploadFileInAzureStorage(IFormFile file, string azureConnectionString, string azureContainerName);
        public Task<byte[]> GetBlobFile(string Name, string azureConnectionString, string azureContainerName);
        public Task<string> UploadFileShares(IFormFile formFile, string azureConnectionString, string azureContainerName, string dirName);
        public Task<string> PostQueue(FileOrBlobUpload fileOrBlobUpload, string azureConnectionString, string azureContainerName);

    }
}
