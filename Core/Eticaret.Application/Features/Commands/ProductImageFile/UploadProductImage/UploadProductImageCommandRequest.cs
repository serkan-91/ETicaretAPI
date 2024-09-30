using MediatR;

namespace EticaretAPI.Application.Features.Commands.ProductImageFile.Upload;

public class UploadProductImageCommandRequest : IRequest<List<UploadProductImageCommandResponse>>
{
	public string Id { get; set; }
	public List<FileData> Files { get; set; } = new();
}

public class FileData
{
	public string FileName { get; set; } = string.Empty;
	public byte[] Content { get; set; } = Array.Empty<byte>();
	public string ContentType { get; set; } = string.Empty;
}
