using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Storage.Application.Common.Interfaces;
using Storage.Application.Features.Dtos;

namespace Storage.Application.Features.Queries;

public class GetFileQueryById : IRequest<FileMetadataDto>
{
    public GetFileQueryById(Guid fileId)
    {
        FileId = fileId;
    }
    private Guid FileId { get; }


    public class Handler: IRequestHandler<GetFileQueryById, FileMetadataDto>
    {
        private readonly IStorageDbContext _dbContext;
        private readonly IMapper _mapper;

        public Handler(IStorageDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<FileMetadataDto> Handle(GetFileQueryById request, CancellationToken cancellationToken)
        {
            var file =  await _dbContext.Files.Where(e => e.Id == request.FileId).SingleOrDefaultAsync(cancellationToken);
            if (file == null)
                throw new Exception("Not Found");
            return _mapper.Map<FileMetadataDto>(file);
        }
    }
}