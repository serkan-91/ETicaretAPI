using System.Net;
using EticaretAPI.Application.Operations;
using MediatR;

namespace EticaretAPI.Application.Features.Commands.Product.Remove;

public class RemoveProductCommandHandler(IProductServices _productService)
	: IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
{
	public async Task<RemoveProductCommandResponse> Handle(
		RemoveProductCommandRequest request,
		CancellationToken cancellationToken
	)
	{
		await _productService.DeleteProductAsync(request, cancellationToken).ConfigureAwait(false);
		return new RemoveProductCommandResponse() { StatusCode = (int)HttpStatusCode.OK };
	}
}
