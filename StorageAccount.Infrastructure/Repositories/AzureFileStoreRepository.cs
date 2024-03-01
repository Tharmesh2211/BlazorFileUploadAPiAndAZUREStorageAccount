using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using StorageAccount.Application.IServices;
using StorageAccount.Domain.Model;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace StorageAccount.Infrastructure.Repositories
{
    public class AzureFileStoreRepository : IAzureFileStore
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public AzureFileStoreRepository(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return "File is not provided or is empty.";
                }

                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var result = await UploadFileAsync(filePath, uniqueFileName);

                return ($"File uploaded successfully. Path: {filePath}");
            }
            catch (Exception ex)
            {
                return ($"Internal server error: {ex.Message}");
            }
        }



        public async Task<string> UploadFileAsync(string filePath, string uniqueFileName)
        {
            try
            {
                var blobServiceClient = new BlobServiceClient(AzureFileStore.connectionString);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(AzureFileStore.containerName);
                var blobClient = blobContainerClient.GetBlobClient(uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await blobClient.UploadAsync(stream, true);
                    Console.WriteLine(stream);
                }

                return "Successfully stored in Azure Storage Account";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

    }
}
