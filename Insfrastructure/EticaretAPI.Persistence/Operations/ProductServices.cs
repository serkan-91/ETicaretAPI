using AutoMapper;
using EticaretAPI.Application.Abstractions.Storage;
using EticaretAPI.Application.Common.Dtos;
using EticaretAPI.Application.Operations;
using EticaretAPI.Application.RequestParameters;
using EticaretAPI.Application.ResponseParameters;
using EticaretAPI.Application.ViewModels;

namespace EticaretAPI.Persistence.Operations;

public class ProductServices(
    IProductReadRepository _productReadRepository,
    IProductWriteRepository _productWriteRepository,
    IProductImageFileWriteRepository _productImageFileWriteRepository,
    //IProductImageFileReadRepository _productImageFileReadRepository
    IStorageService _storageService,
    IMapper _mapper
) : IProductServices
{
    public Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken) =>
        Task.Run(() => _productReadRepository.GetAllAsync(cancellationToken, false));

    public Task<Product> GetProductByIdAsync(string id, CancellationToken cancellationToken) =>
        Task.Run(() => _productReadRepository.GetByIdAsync(cancellationToken, id));

    public Task<PagingResult<Product>> GetProductsPagingAsync(
        Paginations paginations,
        CancellationToken cancellationToken
    ) =>
        Task.Run(() => _productReadRepository.GetPagedAsync(cancellationToken, paginations, false));

    public Task CreateProductAsync(Product product, CancellationToken cancellationToken) =>
        Task.Run(() => _productWriteRepository.AddAsync(product, cancellationToken));

    public void UpdateProductAsync(VM_Update_Product product)
    {
        var mappedProduct = _mapper.Map<Product>(product);
        _productWriteRepository.UpdateAsync(mappedProduct);
    }

    public async Task<UpladImageResults> UploadProductFilesAsync(
        string id,
        List<FileDto> files,
        CancellationToken cancellationToken
    )
    {
        UpladImageResults result = await _storageService
            .UploadFilesAsync("photo-images", files, cancellationToken)
            .ConfigureAwait(false);

        var products = await _productReadRepository
            .GetByIdAsync(cancellationToken, id, true)
            .ConfigureAwait(false);

        await _productImageFileWriteRepository
            .AddRangeAsync(
                result
                    .UpladImages.Select(r => new ProductImageFile
                    {
                        FileName = r.FileName,
                        Path = r.PathOrContainerName,
                        StorageTypes = _storageService.StorageServiceType,
                        Products = [products],
                    })
                    .ToList(),
                cancellationToken
            )
            .ConfigureAwait(false);
        return result;
    }

    public Task DeleteProductAsync(string id, CancellationToken cancellationToken) =>
        Task.Run(
            () => _productWriteRepository.RemoveAsync(id, cancellationToken).ConfigureAwait(false)
        );

    public async Task<List<ProductImages>> GetProductImagesAsync(
        string id,
        CancellationToken cancellationToken
    )
    {
        var productId = Guid.Parse(id);

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
        return images;
    }

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
}
