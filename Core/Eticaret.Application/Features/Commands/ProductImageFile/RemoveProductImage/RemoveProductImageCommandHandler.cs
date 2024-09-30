using EticaretAPI.Application.Operations;
using MediatR;

namespace EticaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;

public class RemoveProductImageCommandHandler(IProductServices ProductService)
	: IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
{
	public async Task<RemoveProductImageCommandResponse> Handle(
		RemoveProductImageCommandRequest request,
		CancellationToken cancellationToken
	)
	{
		await ProductService
			.DeleteProductImageAsync(request.Id, request.ImageId, cancellationToken)
			.ConfigureAwait(false);
		return new RemoveProductImageCommandResponse();
	}
}
