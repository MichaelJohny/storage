using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using Storage.Application.Common.Constant;
using Storage.Application.Common.Interfaces;
using Storage.Application.Features.Dtos;

namespace Storage.Infrastructure.Services;

public class GoogleStorageService : IStorageService
{
    private readonly StorageClient _storageClient;
    private readonly IConfiguration _configuration;

    public GoogleStorageService(IConfiguration configuration)
    {
        _configuration = configuration;
        _storageClient = StorageClient.Create(InitClient());
    }

    public async Task<string> UploadFileAsync(Stream objectFile, string fileNameForStorage, string? contentType = null)
    {

        var bucketName = _configuration["GoogleCloudStorage:GoogleCloudStorageBucket"];
        using var memoryStream = new MemoryStream();
        var dataObject =
            await _storageClient.UploadObjectAsync(bucketName, fileNameForStorage, contentType, objectFile);
        string publicUrl = $"{Constants.BucketUrl}/{bucketName}/{fileNameForStorage}";
        return publicUrl;
    }

    public async Task DeleteFileAsync(string fileNameForStorage)
    {
        var bucketName = _configuration["GoogleCloudStorage:GoogleCloudStorageBucket"];
        await _storageClient.DeleteObjectAsync(bucketName, fileNameForStorage);
    }

    public async Task<FileDownloadResultDto> DownloadFileAsync(string fileNameForStorage)
    {
        var bucketName = _configuration["GoogleCloudStorage:GoogleCloudStorageBucket"];
        using var memoryStream = new MemoryStream();
        var fileObject = await _storageClient.DownloadObjectAsync(bucketName, fileNameForStorage, memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new FileDownloadResultDto
        {
            Name = fileObject.Name,
            ContentType = fileObject.ContentType,
            FileData = memoryStream.ToArray()
        };
    }

    private GoogleCredential InitClient()
    {
        var credentials = new JsonCredentialParameters()
        {
            ClientEmail = _configuration["CredentialParameters:client_email"],
            ClientId = _configuration["CredentialParameters:client_id"],
            PrivateKey = _configuration["CredentialParameters:private_key"],
            PrivateKeyId = _configuration["CredentialParameters:private_key_id"],
            ProjectId = _configuration["CredentialParameters:project_id"],
            Type = _configuration["CredentialParameters:type"],
        };
        return GoogleCredential.FromJsonParameters(credentials);
    }

}