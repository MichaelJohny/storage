using MediatR;
using Storage.Application.Common.Interfaces;
using Storage.Application.Features.Dtos;

namespace Storage.Application.Features.Queries
{
    public record DownloadFileQuery(string FileName) : IRequest<FileDownloadResultDto>;

    public class DownloadFileQueryHandler :   IRequestHandler<DownloadFileQuery, FileDownloadResultDto>
    {
        private readonly IStorageService _storageService;

        public DownloadFileQueryHandler(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<FileDownloadResultDto> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            return await _storageService.DownloadFileAsync(request.FileName);
        }
    }

}
