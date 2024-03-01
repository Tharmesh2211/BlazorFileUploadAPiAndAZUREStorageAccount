using Microsoft.AspNetCore.Http;

namespace StorageAccount.Domain.Model
{
    public class AzureFileStore
    {
        public static string connectionString { get; set; } = "DefaultEndpointsProtocol=https;AccountName=tharmesh222112002;AccountKey=3nZPRLXyBxIToX+6UwpCs5S5fapQ7CgBmN564/XAL1QUlgwXYxkZjPhojPACj+E4YXuAwQEHaeG/+AStrDSIKA==;EndpointSuffix=core.windows.net";
        public static string containerName { get; set; } = "input";
        public IFormFile formFile {  get; set; }
    }
}
