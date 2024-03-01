using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageAccount.Domain.Model;

namespace StorageAccount.Application.IServices
{
    public interface IFileUpload
    {
        public bool CreateDirectory(IFormFile formFile);
        //public Task<string> UploadFileAsync();
        public Task<bool> UploadFileInDirectory();
        public Task<bool> UploadFileInAzureStorage();
        public Task<bool> UploadFileInDB(IFormFile formFile);
        public void FileDismantle();

    }
}
