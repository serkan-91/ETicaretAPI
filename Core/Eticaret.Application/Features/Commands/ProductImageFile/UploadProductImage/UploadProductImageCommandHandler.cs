using EticaretAPI.Application.Operations;
using MediatR;

namespace EticaretAPI.Application.Features.Commands.ProductImageFile.Upload;

public class UploadProductImageCommandHandler(IProductServices ProductService)
	: IRequestHandler<UploadProductImageCommandRequest, List<UploadProductImageCommandResponse>>
{
	public async Task<List<UploadProductImageCommandResponse>> Handle(
		UploadProductImageCommandRequest request,
		CancellationToken cancellationToken
	)
	{
		return await ProductService.UploadProductFilesAsync(request, cancellationToken);
	}
}
