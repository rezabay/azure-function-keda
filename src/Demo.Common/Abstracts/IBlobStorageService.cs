namespace Demo.Common.Abstracts;

public interface IBlobStorageService
{
    Task<List<string>?> ListBlobs(string containerName, string prefix);
    Task<string> Upload(string containerName, Stream content, string fileName, bool overwrite = true);
    Task<string> Download(string containerName, string fileName);
    Task<Stream> DownloadStream(string containerName, string fileName);
    Task<bool> Exists(string containerName, string fileName);
}