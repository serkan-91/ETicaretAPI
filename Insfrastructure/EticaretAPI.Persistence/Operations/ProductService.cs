using AutoMapper;
using EticaretAPI.Application.Abstractions.Storage;
using EticaretAPI.Application.Abstractions.Storage.Azure;
using EticaretAPI.Application.Operations;
using EticaretAPI.Application.RequestParameters;
using EticaretAPI.Application.ViewModels;

namespace EticaretAPI.Persistence.Operations;

public class ProductService(
	IProductReadRepository _productReadRepository ,
	IProductWriteRepository _productWriteRepository ,
	IProductImageFileWriteRepository _productImageFileWriteRepository ,
	//IProductImageFileReadRepository _productImageFileReadRepository
	IStorageService _storageService ,
	IMapper _mapper
) : IProductService
	{
	public async Task<List<Product>> GetProductsAsync() =>
		await _productReadRepository.GetAllAsync(false);

	public async Task<Product> GetProductByIdAsync(string id) =>
		await _productReadRepository.GetByIdAsync(id);

	public async Task<PagingResult<Product>> GetProductsPagingAsync(Paginations paginations) =>
	await _productReadRepository.GetPagedAsync(paginations , false);

	public async Task CreateProductAsync(Product product) =>
	await _productWriteRepository.AddAsync(product);

	public void UpdateProductAsync(VM_Update_Product product) {
		var mappedProduct = _mapper.Map<Product>(product);
		_productWriteRepository.UpdateAsync(mappedProduct);
		}

	public async Task<List<(string fileName, string pathOrContainerName)>> UploadProductFilesAsync(string id , List<string> fileNames) {
		List<(string fileName, string pathOrContainerName)> result  =
		 await _storageService.UploadFilesAsync("photo-images" , fileNames);
		var products = await _productReadRepository.GetByIdAsync(id,true)  ;

		await _productImageFileWriteRepository.AddRangeAsync(
	  result
		  .Select(r => new ProductImageFile
			  {
			  FileName = r.fileName ,
			  Path = r.pathOrContainerName ,
			  StorageTypes = _storageService.StorageServiceType ,
			  Products = [products] ,
			  })
		  .ToList()
	  );
		return result;
		}

	public void DeleteProductAsync(string id) =>
		_productWriteRepository.RemoveAsync(id);

	public async Task GetProductImages(string id) {
		var basePath = _storageService is IAzureStorage azureStorage
			? azureStorage.GetBlobClient().Uri.AbsoluteUri
			: "";
		var productId = Guid.Parse(id);

		var product = await _productReadRepository.GetWithIncludeAsync(
			data => data.Id == productId,
			false,
			data => data.ProductImageFiles
		);

		var items=  product.ProductImageFiles.Select(p => new
			{
			Path = basePath + p.Path ,
			p.FileName ,
			p.Id ,
			});
		}

	public async Task DeleteProductImageAsync(string id , string imageId) {
		var product = await _productReadRepository.GetByIdAsync(id);
		var image = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
		if(image is not null)
			_productImageFileWriteRepository.RemoveAsync(image);
		}
	}