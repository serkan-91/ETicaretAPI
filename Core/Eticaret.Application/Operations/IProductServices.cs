using EticaretAPI.Application.Common.Dtos;
using EticaretAPI.Application.RequestParameters;
using EticaretAPI.Application.ResponseParameters;

namespace EticaretAPI.Application.Operations;

public interface IProductServices
{
    Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken);

    Task<Product> GetProductByIdAsync(string id, CancellationToken cancellationToken);

    Task<PagingResult<Product>> GetProductsPagingAsync(
        Paginations paginations,
        CancellationToken cancellationToken
    );

    Task<List<ProductImages>> GetProductImagesAsync(string id, CancellationToken cancellationToken);
    Task CreateProductAsync(Product product, CancellationToken cancellationToken);

    void UpdateProductAsync(VM_Update_Product product);

    Task DeleteProductAsync(string id, CancellationToken cancellationToken);

    Task DeleteProductImageAsync(string id, string imageId, CancellationToken cancellationToken);

    Task<UpladImageResults> UploadProductFilesAsync(
        string id,
        List<FileDto> files,
        CancellationToken cancellationToken
    );
}
