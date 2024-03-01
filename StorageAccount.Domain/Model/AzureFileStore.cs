using Microsoft.AspNetCore.Http;

namespace StorageAccount.Domain.Model
{
    public class AzureFileStore
    {
        public static string connectionString { get; set; } = "";
        public static string containerName { get; set; } = "input";
        public IFormFile formFile {  get; set; }
    }
}
