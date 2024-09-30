using EticaretAPI.Application.ResponseParameters;

namespace EticaretAPI.Application.Features.Commands.ProductImageFile.Upload;

public class UploadProductImageCommandResponse
{
	public string FileName { get; set; }
	public string Path { get; set; }
	public string Id { get; set; }
}
