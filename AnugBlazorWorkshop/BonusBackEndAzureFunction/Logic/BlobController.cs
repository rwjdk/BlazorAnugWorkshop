using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using SharedModels.Model;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Azure.Storage.Blobs;
using System;
using Azure;
using Azure.Storage.Blobs.Models;

namespace X_BonusBackEndAzureFunction.Logic;

public class BlobController
{
    private readonly string _userId;
    private readonly BlobContainerClient _container;

    public BlobController(string userId)
    {
        _userId = userId;
        var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        var blobServiceClient = new BlobServiceClient(connectionString);
        _container = blobServiceClient.GetBlobContainerClient("userfiles");
        _container.CreateIfNotExists(PublicAccessType.BlobContainer);
    }

    public string GetBlobContent()
    {
        var blob = _container.GetBlobClient($"{_userId}.json");
        if (!blob.Exists())
        {
            return JsonConvert.SerializeObject(new List<Bookmark>(), Formatting.Indented);
        }

        BlobDownloadResult downloadResult = blob.DownloadContent();
        return downloadResult.Content.ToString();
    }

    public void UpdateBlob(string json)
    {
        var blob = _container.GetBlobClient($"{_userId}.json");
        var content = Encoding.UTF8.GetBytes(json);
        using var ms = new MemoryStream(content);
        blob.Upload(ms, overwrite: true);
    }

    public List<Bookmark> GetBookmarks()
    {
        var json = GetBlobContent();
        return JsonConvert.DeserializeObject<List<Bookmark>>(json);
    }
}