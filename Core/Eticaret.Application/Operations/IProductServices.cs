using EticaretAPI.Application.RequestParameters;

namespace EticaretAPI.Application.Operations;

public interface IProductService
	{
	Task<List<Product>> GetProductsAsync();

	Task<Product> GetProductByIdAsync(string id);

	Task<PagingResult<Product>> GetProductsPagingAsync(Paginations paginations);

	Task GetProductImages(string id);

	Task CreateProductAsync(Product product);

	void UpdateProductAsync(VM_Update_Product product);

	void DeleteProductAsync(string id);

	Task DeleteProductImageAsync(string id , string imageId);

	Task<List<(string fileName, string pathOrContainerName)>> UploadProductFilesAsync(string id , List<string> fileName);
	}