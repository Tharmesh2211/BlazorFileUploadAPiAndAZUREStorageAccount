using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAccount.Domain.Model
{
    public class FileUpload
    {
        public IFormFile FormFile {  get; set; }
    }
}
