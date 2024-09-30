using AutoMapper;
using EticaretAPI.Application.Abstractions.Storage;
using EticaretAPI.Application.Features.Commands.Product.Remove;
using EticaretAPI.Application.Features.Commands.Product.Update;
using EticaretAPI.Application.Features.Commands.ProductImageFile.Upload;
using EticaretAPI.Application.Features.Queries.GetProductImage;
using EticaretAPI.Application.Operations;
using EticaretAPI.Application.RequestParameters;
using EticaretAPI.Application.ResponseParameters;

namespace EticaretAPI.Persistence.Operations;

public class ProductServices(
	IProductReadRepository _productReadRepository,
	IProductWriteRepository _productWriteRepository,
	IProductImageFileWriteRepository _productImageFileWriteRepository,
	IStorageService _storageService,
	IMapper _mapper
) : IProductServices
{
	public Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken) =>
		Task.Run(() => _productReadRepository.GetAllAsync(cancellationToken, false));

	public Task<Product> GetProductByIdAsync(string id, CancellationToken cancellationToken) =>
		Task.Run(() => _productReadRepository.GetByIdAsync(cancellationToken, id));

	public Task<PagingResult<Product>> GetProductsPagingAsync(
		Pagination paginations,
		CancellationToken cancellationToken
	) =>
		Task.Run(() => _productReadRepository.GetPagedAsync(cancellationToken, paginations, false));

	public async void CreateProductAsync(Product product) =>
		await _productWriteRepository.AddAsync(product);

	public async Task<List<UploadProductImageCommandResponse>> UploadProductFilesAsync(
		UploadProductImageCommandRequest request,
		CancellationToken cancellationToken
	)
	{
		List<UploadProductImageCommandResponse> result = await _storageService
			.UploadFilesAsync("photo-images", request.Files, cancellationToken)
			.ConfigureAwait(false);

		Product products = await _productReadRepository
			.GetByIdAsync(cancellationToken, request.Id, true)
			.ConfigureAwait(false);

		var newImages = result
			.Select(r => new ProductImageFile
			{
				FileName = r.FileName,
				Path = r.Path,
				StorageTypes = _storageService.StorageServiceType,
				Products = new List<Product> { products },
			})
			.ToList();

		var fullImagePaths = newImages
			.Select(img => Path.Combine(_storageService.GetBasePathOrContainer, img.Path)) // Burada tam yol oluşturuluyor
			.ToList();

		await _productImageFileWriteRepository
			.AddRangeAsync(newImages, cancellationToken)
			.ConfigureAwait(false);
		var uploadedImage = _mapper
			.Map<List<UploadProductImageCommandResponse>>(newImages)
			.Select(image =>
			{
				image.Path = Path.Combine(_storageService.GetBasePathOrContainer, image.Path); // Full path ile güncelleniyor
				return image;
			})
			.ToList();
		return uploadedImage;
	}

	public Task DeleteProductAsync(
		RemoveProductCommandRequest request,
		CancellationToken cancellationToken
	) => Task.Run(() => _productWriteRepository.RemoveAsync(request.Id, cancellationToken));

	public async Task DeleteProductImageAsync(
		string id,
		string imageId,
		CancellationToken cancellationToken
	)
	{
		var product = await _productReadRepository
			.GetByIdAsync(cancellationToken, id)
			.ConfigureAwait(false);

		var image = await _productReadRepository
			.Table.Include(p => p.ProductImageFiles)
			.FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
		if (image is not null)
			await _productImageFileWriteRepository
				.RemoveAsync(imageId, cancellationToken)
				.ConfigureAwait(false);
	}

	public async Task UpdateProductAsync(UpdateProductCommandRequest product)
	{
		await _productWriteRepository.UpdateAsync(_mapper.Map<Product>(product));
	}

	public async Task<List<GetProductImageQueryResponse>> GetProductImagesAsync(
		string Id,
		CancellationToken cancellationToken
	)
	{
		var productId = Guid.Parse(Id);

		var product = await _productReadRepository
			.GetWithIncludeAsync(
				data => data.Id == productId,
				cancellationToken,
				false,
				data => data.ProductImageFiles
			)
			.ConfigureAwait(false);

		var images = product
			.ProductImageFiles.Select(p => new ProductImages
			{
				Path = $"{_storageService.GetBasePathOrContainer}{p.Path}",
				FileName = p.FileName,
				Id = p.Id.ToString(),
			})
			.ToList();

		return _mapper.Map<List<GetProductImageQueryResponse>>(images);
		;
	}
}
