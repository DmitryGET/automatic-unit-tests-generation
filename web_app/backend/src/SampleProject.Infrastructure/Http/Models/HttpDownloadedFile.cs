namespace SampleProject.Infrastructure.Http.Models;

public record HttpDownloadedFile
{
    public string FileNameWithExtension { get; init; }
    
    public byte[] FileContent { get; init; }
}