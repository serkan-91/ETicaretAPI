using System.Net;
using EticaretAPI.Application.Operations;
using MediatR;

namespace EticaretAPI.Application.Features.Commands.Product.Update;

public class UpdateProductCommandHandler(IProductServices _productServices)
	: IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
	public async Task<UpdateProductCommandResponse> Handle(
		UpdateProductCommandRequest request,
		CancellationToken cancellationToken
	)
	{
		await _productServices.UpdateProductAsync(request);
		return new UpdateProductCommandResponse() { StatusCode = (int)HttpStatusCode.OK };
	}
}
