using Microsoft.AspNetCore.Http;

namespace StorageAccount.Application.IServices.DataBaseService
{
    public interface IDataBaseService
    {
        public Task<bool> UploadFileInDB(IFormFile formFile, Guid guid);
        public Task<string> SearchFileInDB(string filename);


    }
}
