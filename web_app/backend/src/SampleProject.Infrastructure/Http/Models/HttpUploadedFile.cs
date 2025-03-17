using System;

namespace SampleProject.Infrastructure.Http.Models;

public class HttpUploadedFile
{
    public required Guid FileId { get; set; }
}