using Storage.Application.Features.Dtos;

namespace Storage.Application.Common.Interfaces;

public interface IStorageService
{
    Task<string> UploadFileAsync(Stream objectFile, string fileNameForStorage, string? contentType = null);
    Task DeleteFileAsync(string fileNameForStorage);

    Task<FileDownloadResultDto> DownloadFileAsync(string fileNameForStorage);
}