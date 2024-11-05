using MediatR;
using Microsoft.AspNetCore.Http;
using Storage.Application.Common.Interfaces;
using Storage.Application.Features.Dtos;
using Storage.Domain.Entities;

namespace Storage.Application.Features.Commands;

public record UploadFileCommand: IRequest<UploadFileResponse>
{
    public UploadFileCommand(IFormFile fileData, string folderName)
    {
        FileData = fileData;
        FolderName = folderName;
    }

    public IFormFile FileData { get; }
    public string FolderName { get; set; }
    
    public class Handler : IRequestHandler<UploadFileCommand, UploadFileResponse>
    {
        private readonly IStorageService _storageService;
        private readonly IStorageDbContext _dbContext;
        private readonly IDateTime _dateTime;
        public Handler(IStorageService storageService, IStorageDbContext dbContext, IDateTime dateTime)
        {
            _storageService = storageService;
            _dbContext = dbContext;
            _dateTime = dateTime;
        }

        public async Task<UploadFileResponse> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var stream = request.FileData.OpenReadStream();
            var contentType = $"image/{Path.GetExtension(request.FileData.FileName).Remove(0, 1)}";
            var fileNameForStorage = !string.IsNullOrWhiteSpace(request.FolderName)
                ? $"{request.FolderName}/{request.FileData.FileName}"
                : request.FileData.FileName;
            var imagePath = await _storageService.UploadFileAsync(stream, fileNameForStorage, contentType);

            _dbContext.Files.Add(new FileMetadata()
            {
                ContentType = contentType,
                FileName = fileNameForStorage,
                StoragePath = imagePath,
                UploadDate = _dateTime.Now
            });
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new UploadFileResponse(imagePath);
        }
    }
}