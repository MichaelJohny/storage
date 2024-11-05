using MassTransit;
using MediatR;
using Storage.Application.Features.Commands;

namespace Storage.Application.Common.Events;

public class SaveFileConsumer : IConsumer<SaveFileEvent>
{
    private readonly IMediator _mediator;

    public SaveFileConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<SaveFileEvent> context)
    {
        await _mediator.Send(new UploadFileCommand(context.Message.FileData, context.Message.FolderName));
    }
}