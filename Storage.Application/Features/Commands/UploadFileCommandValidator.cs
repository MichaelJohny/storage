using FluentValidation;

namespace Storage.Application.Features.Commands;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(e => e.FileData).NotNull();
    }
}