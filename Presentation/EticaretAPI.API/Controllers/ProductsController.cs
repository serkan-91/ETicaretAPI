using System.Net;
using Azure.Storage.Blobs;
using EticaretAPI.Application.Abstractions.Storage;
using EticaretAPI.Application.Repositories;
using EticaretAPI.Application.RequestParameters;
using EticaretAPI.Application.ViewModels;
using EticaretAPI.Application.ViewModels.Products;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EticaretAPI.Domain.Entities.File;

namespace EticaretAPI.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductsController(
	IProductWriteRepository _productWriteRepository ,
	IProductReadRepository _productReadRepository ,
	//IFileReadRepository _fileReadRepository,
	//IFileWriteRepository _fileWriteRepository,
	//IProductImageFileReadRepository _productImageFileReadRepository,
	IProductImageFileWriteRepository _productImageFileWriteRepository ,
	//IInvoiceFileReadRepository _invoiceFileReadRepository,
	//IInvoiceFileWriteRepository _invoiceFileWriteRepository
	IStorageService _storageService
) : ControllerBase
	{
	private readonly BlobServiceClient _blobServiceClient = new(Configurations.GetCurrentStorage);

	[HttpGet]
	public IActionResult GetAllProduct() => Ok(_productReadRepository.GetAll(false));

	[HttpGet("{id}")]
	public IActionResult GetByIdProduct(string id) =>
		Ok(_productReadRepository.GetByIdAsync(id , false));

	[HttpGet]
	public async Task<IActionResult> GetProduct(string id) =>
		Ok(await _productReadRepository.FindAsync(id));

	[HttpGet]
	public async Task<IActionResult> GetProductsPaging([FromQuery] Paginations pagination)
		{
		var totalProductCount = await _productReadRepository.GetAll(false).CountAsync();
		var products = await _productReadRepository
			.GetAll(false)
			.Select(p => new
				{
				p.Id,
				p.Name,
				p.Stock,
				p.Price,
				p.CreatedDate,
				p.UpdatedDate,
				})
			.Skip(pagination.Page * pagination.Size)
			.Take(pagination.Size)
			.ToListAsync();

		return Ok(new { products , totalProductCount });
		}

	[HttpPost]
	public async Task<IActionResult> CreateProduct(VM_Create_Product model)
		{
		await _productWriteRepository.AddAsync(
			new()
				{
				Name = model.Name ,
				Price = model.Price ,
				Stock = model.Stock ,
				}
		);
		await _productWriteRepository.SaveAsync();
		return StatusCode((int) HttpStatusCode.Created);
		}

	[HttpPut]
	public async Task<IActionResult> Put(VM_Update_Product model)
		{
		Product product = await _productReadRepository.GetByIdAsync(model.Id);
		product.Stock = model.Stock;
		product.Name = model.Name;
		product.Price = model.Price;
		await _productWriteRepository.SaveAsync();

		return Ok();
		}

	[HttpPost]
	public async Task<IActionResult> Upload(string id)
		{
		List<(string fileName, string pathOrContainerName)> result =
			await _storageService.UploadFilesAsync("photo-images", Request.Form.Files);
		Product products = await _productReadRepository.GetByIdAsync(id , true);

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
		await _productImageFileWriteRepository.SaveAsync();

		return Ok(result);
		}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteProduct(string id)
		{
		await _productWriteRepository.RemoveAsync(id);
		await _productWriteRepository.SaveAsync();
		return Ok();
		}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteProductImage(string id , string imageId)
		{
		Product product = await _productReadRepository
			.Table.Include(p => p.ProductImageFiles)
			.FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

		ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p =>
			p.Id == Guid.Parse(imageId)
		);

		product.ProductImageFiles.Remove(productImageFile);

		await _productWriteRepository.SaveAsync();
		return Ok();
		}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetProductImages(string id)
		{
		var product = await _productReadRepository
			.Table.Include(p => p.ProductImageFiles)
			.FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
		var baseAzureUri = _blobServiceClient.Uri.AbsoluteUri.ToString();

		return Ok(
			product.ProductImageFiles.Select(p => new
				{
				Path = baseAzureUri + p.Path ,
				p.FileName ,
				p.Id ,
				})
		);
		}
	}