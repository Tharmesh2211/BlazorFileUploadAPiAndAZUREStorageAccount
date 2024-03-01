using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using StorageAccount.Application.IServices;
using StorageAccount.Domain.Model;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using Microsoft.VisualBasic.FileIO;
using Microsoft.EntityFrameworkCore;
using StorageAccount.Infrastructure.DataContext;

namespace StorageAccount.Infrastructure.Repositories
{
    public class AzureFileStoreRepository : IFileUpload
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        private readonly FileContext fileContext;

        private static IFormFile formFile;

        private static string uploadPath = "";

        private static string fileName = "";

        private static string fileType = "";

        private static Guid guidOfFile;

        private static string filePath = "";

        private static string uniqueFileName = "";
        public AzureFileStoreRepository(IWebHostEnvironment hostEnvironment, FileContext fileContext)
        {
            this.fileContext = fileContext;
            _hostEnvironment = hostEnvironment;
        }

        public bool CreateDirectory(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    Console.WriteLine("File is not provided or is empty.");
                    return false;
                }
                formFile = file;

                uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                return true;

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Internal server error: {ex.Message}");
                return false;
            }

        }
        public void FileDismantle()
        {
            fileName = formFile.FileName;

            fileType = Path.GetExtension(fileName);
            guidOfFile = Guid.NewGuid();

            uniqueFileName = guidOfFile + fileType;

        }

        public async Task<bool> UploadFileInDirectory()
        {
            uniqueFileName = guidOfFile + fileType;

            filePath = Path.Combine(uploadPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            Console.WriteLine($"File uploaded successfully. Path: {filePath}");

            return true;
        }


        public async Task<bool> UploadFileInAzureStorage()
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

                Console.WriteLine("Successfully stored in Azure Storage Account");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public Task<bool> UploadFileInAzureStorage(IFormFile formFile)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UploadFileInDB(IFormFile formFile)
        {
            FileOrBlobUpload fileOrBlob = new FileOrBlobUpload()
            {
                BlobFileId = 0,

                BlobFileGuid = guidOfFile,

                BlobFileType = fileType,

                FileName = fileName,

                isDeleted = false,
            };

            var insetValue = await fileContext.AddAsync(fileOrBlob);

            if (insetValue.State == EntityState.Added)
            {
                await fileContext.SaveChangesAsync();

                Console.WriteLine("DataBase Updated Successfully!");

                return true;
            }
            Console.WriteLine("Failed to add in Database");

            return false;
        }
    }


}