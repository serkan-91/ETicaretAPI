using EticaretAPI.Application.Features.Commands.Product.Remove;
using EticaretAPI.Application.Features.Commands.Product.Update;
using EticaretAPI.Application.Features.Commands.ProductImageFile.Upload;
using EticaretAPI.Application.Features.Queries.GetProductImage;
using EticaretAPI.Application.RequestParameters;
using EticaretAPI.Application.ResponseParameters;

namespace EticaretAPI.Application.Operations;

public interface IProductServices
{
	Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken);

	Task<Product> GetProductByIdAsync(string id, CancellationToken cancellationToken);

	Task<PagingResult<Product>> GetProductsPagingAsync(
		Pagination paginations,
		CancellationToken cancellationToken
	);

	Task<List<GetProductImageQueryResponse>> GetProductImagesAsync(
		string Id,
		CancellationToken cancellationToken
	);

	void CreateProductAsync(Product product);

	Task UpdateProductAsync(UpdateProductCommandRequest product);

	Task DeleteProductAsync(
		RemoveProductCommandRequest request,
		CancellationToken cancellationToken
	);

	Task DeleteProductImageAsync(string id, string imageId, CancellationToken cancellationToken);

	Task<List<UploadProductImageCommandResponse>> UploadProductFilesAsync(
		UploadProductImageCommandRequest request,
		CancellationToken cancellationToken
	);
}
