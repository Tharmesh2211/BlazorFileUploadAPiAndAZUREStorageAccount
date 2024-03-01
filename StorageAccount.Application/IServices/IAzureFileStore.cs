using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAccount.Application.IServices
{
    public interface IAzureFileStore
    {
        public Task<string> UploadFile(IFormFile file);
    }
}
