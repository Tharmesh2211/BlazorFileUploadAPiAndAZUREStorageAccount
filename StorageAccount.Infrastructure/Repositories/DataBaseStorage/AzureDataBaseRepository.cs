using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StorageAccount.Application.IServices.DataBaseService;
using StorageAccount.Domain.Model;
using StorageAccount.Infrastructure.DataContext;

namespace StorageAccount.Infrastructure.Repositories.DataBaseStorage
{
    public class AzureDataBaseRepository : IDataBaseService
    {
        private readonly FileContext _FileContext;
        public AzureDataBaseRepository(FileContext fileContext)
        {
            _FileContext = fileContext;
        }
        public async Task<bool> UploadFileInDB(IFormFile file, Guid guid)
        {
            Console.WriteLine(file.FileName);
            FileOrBlobUpload fileOrBlob = new FileOrBlobUpload()
            {
                FileId = 0,

                FileGuid = guid,

                FileType = Path.GetExtension(file.FileName),

                FileName = file.FileName,

                isDeleted = false,
            };

            var insetValue = await _FileContext.AddAsync(fileOrBlob);

            if (insetValue.State == EntityState.Added)
            {
                await _FileContext.SaveChangesAsync();

                Console.WriteLine("DataBase Updated Successfully!");

                return true;
            }
            Console.WriteLine("Failed to add in Database");

            return false;
        }
        public async Task<string> SearchFileInDB(string filename)
        {
            if(filename != null)
            {
                var result = await _FileContext.FileDetails.FirstOrDefaultAsync(f => f.FileName == filename);

                if(result != null)
                {
                    return result.FileGuid + result.FileType;
                }
            }

            return null;
           
        }
    }
}
