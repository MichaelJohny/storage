namespace Storage.Application.Features.Dtos;

public class UploadFileResponse
{
    public UploadFileResponse(string imagePath)
    {
        ImagePath = imagePath;
    }

    public string ImagePath { get; }
}