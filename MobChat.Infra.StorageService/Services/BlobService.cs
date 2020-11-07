using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MobChat.Infra.StorageService.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Infra.StorageService.Services
{
    public class BlobService : IBlobService
    {
        private string storageConnectionString;
        private BlobServiceClient client;
        private BlobContainerClient containerClient;
        private BlobClient blobClient;

        public BlobService()
        {
            storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=mobchatapp;AccountKey=9XzNMirOuJONILVaHPg8Ur/oTYTGiVnANM1ks0xPsPPxG84gFiJrpiVwLJUc6a1iYjIsMblufW2gjTMXpQG53Q==;EndpointSuffix=core.windows.net";
            client = new BlobServiceClient(storageConnectionString);           

        }
       
        public async Task<string> UploadMediaFileAsync(string container, string fileName, Stream fileStream, string contentType)
        {
            containerClient = new BlobContainerClient(storageConnectionString, container);
            containerClient.CreateIfNotExists();

            blobClient = containerClient.GetBlobClient(fileName);


            var result = blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType }).Result;
            return blobClient.Uri.ToString();
        }
    }
}
