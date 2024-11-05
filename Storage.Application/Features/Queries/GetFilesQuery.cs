using MediatR;
using Microsoft.EntityFrameworkCore;
using Storage.Application.Common.Interfaces;
using Storage.Application.Features.Common;
using Storage.Application.Features.Dtos;

namespace Storage.Application.Features.Queries;

public class GetFilesQuery :PaginationModel , IRequest<GetFilesOutput>
{
    
    public class Handler : IRequestHandler<GetFilesQuery, GetFilesOutput>
    {
        private readonly IStorageDbContext _dbContext;

        public Handler(IStorageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetFilesOutput> Handle(GetFilesQuery request, CancellationToken cancellationToken)
        {
            
            var query = _dbContext.Files.OrderByDescending(x => x.UploadDate)
                .Select(f => new FileMetadataDto()
                {
                    Id = f.Id,
                    FileName = f.FileName,
                    Category = f.Category,
                    UploadDate = f.UploadDate,
                    StoragePath = f.StoragePath,
                    Size = f.Size,
                    Version = f.Version,
                    ContentType = f.ContentType,
                    UploadedBy = f.UploadedBy
                });
            
            var count = await query.CountAsync(cancellationToken);
            var data = await query.Skip((request.Current_page - 1) * request.Page_size)
                .Take(request.Page_size)
                .ToListAsync(cancellationToken);

            return new GetFilesOutput()
            {
                Files = data,
                CurrentPage = request.Current_page,
                PageSize = request.Page_size,
                TotalItems = count
            };

        }
    }
    
}

public class GetFilesOutput
{
    public List<FileMetadataDto> Files { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public long TotalItems { get; set; }
}