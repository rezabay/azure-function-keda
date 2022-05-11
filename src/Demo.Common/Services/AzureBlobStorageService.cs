using Azure.Storage.Blobs;
using Demo.Common.Abstracts;
using Microsoft.Extensions.Options;

namespace Demo.Common.Services;

public class AzureBlobStorageService : IBlobStorageService
{
    private readonly string _connectionString;

    public AzureBlobStorageService(IOptions<StorageConfig> configOptions)
    {
        _connectionString = configOptions.Value.ConnectionString;
    }
    
    public async Task<string> Upload(string containerName, Stream content, string fileName, bool overwrite = true)
    {
        var containerClient = await GetContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        
        await blobClient.UploadAsync(content, overwrite);

        return fileName;
    }

    public async Task<string> Download(string containerName, string fileName)
    {
        var containerClient = await GetContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        var response = await blobClient.DownloadContentAsync();
        return response.Value!.Content.ToString();
    }
    
    public async Task<List<string>?> ListBlobs(string containerName, string prefix)
    {
        var containerClient = await GetContainerClient(containerName);

        return containerClient.GetBlobs(prefix: prefix)
                              .Select(x => x.Name)
                              .ToList();
    }
    
    public async Task<Stream> DownloadStream(string containerName, string fileName)
    {
        var containerClient = await GetContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        return await blobClient.OpenReadAsync();
    }

    public async Task<bool> Exists(string containerName, string fileName)
    {
        var containerClient = await GetContainerClient(containerName);
        
        var blobClient = containerClient.GetBlobClient(fileName);

        var result = await blobClient.ExistsAsync();
        return result?.Value ?? false;
    }

    private async Task<BlobContainerClient> GetContainerClient(string containerName)
    {
        var blobServiceClient = new BlobServiceClient(_connectionString);

        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        return containerClient;
    }
}