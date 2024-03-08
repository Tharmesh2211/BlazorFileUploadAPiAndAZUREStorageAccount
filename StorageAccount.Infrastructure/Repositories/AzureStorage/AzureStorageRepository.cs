using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using StorageAccount.Application.IServices.AzureService;
using StorageAccount.Domain.Model;
using System.Text.Json;

namespace StorageAccount.Infrastructure.Repositories.AzureStorage
{
    public class AzureStorageRepository : IAzureService
    {

        public async Task<Guid> UploadFileInAzureStorage(IFormFile file, string azureConnectionString, string azureContainerName)
        {
            try
            {

                if (file.Equals(null) || azureConnectionString.Equals(null) || azureContainerName.Equals(null))
                {
                    return Guid.Empty;
                }
                var blobServiceClient = new BlobServiceClient(azureConnectionString);

                var blobContainerClient = blobServiceClient.GetBlobContainerClient(azureContainerName);

                var generateGuid = Guid.NewGuid();

                string guidFileName = generateGuid + Path.GetExtension(file.FileName);

                var blobClient = blobContainerClient.GetBlobClient(guidFileName);

                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);

                    Console.WriteLine("File uploaded to Azure Storage");

                }
                return generateGuid;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return Guid.Empty;
            }
        }

        public async Task<byte[]> GetBlobFile(string GuidName, string azureConnectionString, string azureContainerName)
        {
            try
            {
                if (GuidName.Any() || azureConnectionString.Any() || azureContainerName.Any())
                {
                    var blobServiceClient = new BlobServiceClient(azureConnectionString);

                    var blobContainerClient = blobServiceClient.GetBlobContainerClient(azureContainerName);

                    var blobClient = blobContainerClient.GetBlobClient(GuidName);

                    var response = await blobClient.OpenReadAsync();

                    using (var memoryStream = new MemoryStream())
                    {
                        blobClient.DownloadTo(memoryStream);

                        return memoryStream.ToArray();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> UploadFileShares(IFormFile formFile, string azureConnectionString, string azureContainerName, string dirName)
        {
            try
            {
                ShareClient share = new ShareClient(azureConnectionString, azureContainerName);

                if (!share.Exists())

                    share.Create();

                ShareDirectoryClient directory = share.GetDirectoryClient(dirName);

                if (!directory.Exists())

                    directory.Create();

                string fileName = formFile.FileName;

                ShareFileClient file = directory.GetFileClient(fileName);

                await using (Stream stream = formFile.OpenReadStream())
                {
                    file.Create(stream.Length);
                    file.UploadRange(
                        new HttpRange(0, stream.Length),
                        stream);
                }

                return "File Uploaded Successfully";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<string> PostQueue(FileOrBlobUpload fileOrBlobUpload, string azureConnectionString, string azureContainerName)
        {
            var queueClient = new QueueClient(azureConnectionString, azureContainerName);

            var jsonFileUpload = JsonSerializer.Serialize(fileOrBlobUpload);

             await queueClient.SendMessageAsync(jsonFileUpload);

            return "Queue Posted Successfully!";

        }
    }
}
