using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageAccount.Domain.Model;

namespace StorageAccount.Application.IServices
{
    public interface IAzureFileStore
    {
        public Task<string> UploadFile(IFormFile file);
    }
}
