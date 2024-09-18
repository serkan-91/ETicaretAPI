using AutoMapper;
using EticaretAPI.Application.Abstractions.Storage;
using EticaretAPI.Application.Operations;
using EticaretAPI.Application.RequestParameters;
using EticaretAPI.Application.ResponseParameters;
using EticaretAPI.Application.ViewModels;

namespace EticaretAPI.Persistence.Operations;

public class ProductService(
	IProductReadRepository _productReadRepository ,
	IProductWriteRepository _productWriteRepository ,
	IProductImageFileWriteRepository _productImageFileWriteRepository ,
	//IProductImageFileReadRepository _productImageFileReadRepository
	IStorageService _storageService ,
	IMapper _mapper
) : IProductService {
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

	public async Task DeleteProductAsync(string id) =>
		await _productWriteRepository.RemoveAsync(id);

	public async Task<List<ProductImages>> GetProductImages(string id) {


	var productId = Guid.Parse(id);

	var product = await _productReadRepository.GetWithIncludeAsync(
			data => data.Id == productId,
			false,
			data => data.ProductImageFiles
		);

	var images=  product.ProductImageFiles.Select(
		p => new ProductImages
			{
			Path = _storageService.GetBasePathOrContainer + p.Path ,
			FileName = p.FileName ,
			Id = p.Id.ToString()
			}).ToList();
	return images;
		}

	public async Task DeleteProductImageAsync(string id , string imageId) {
	var product = await _productReadRepository.GetByIdAsync(id);
	var image = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
	if(image is not null)
		_productImageFileWriteRepository.RemoveAsync(image);
		}




	}