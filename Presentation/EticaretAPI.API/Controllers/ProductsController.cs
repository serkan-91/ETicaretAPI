using System.Net;
using EticaretAPI.Application.Operations;
using EticaretAPI.Application.Repositories;
using EticaretAPI.Application.RequestParameters;
using EticaretAPI.Application.ViewModels;
using EticaretAPI.Application.ViewModels.Products;
using EticaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EticaretAPI.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductsController(
		IProductWriteRepository _productWriteRepository,
		IProductReadRepository _productReadRepository,
		IFileService _fileService,
		IFileReadRepository _fileReadRepository,
		IFileWriteRepository _fileWriteRepository,
		IProductImageFileReadRepository _productImageFileReadRepository,
		IProductImageFileWriteRepository _productImageFileWriteRepository,
		IInvoiceFileReadRepository _invoiceFileReadRepository,
		IInvoiceFileWriteRepository _invoiceFileWriteRepository
	) : ControllerBase
	{
		[HttpGet]
		public IActionResult GetAllProduct() => Ok(_productReadRepository.GetAll(false));

		[HttpGet("{id}")]
		public IActionResult GetByIdProduct(string id) =>
			Ok(_productReadRepository.GetByIdAsync(id, false));

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

			return Ok(new { products, totalProductCount });
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct(VM_Create_Product model)
		{
			await _productWriteRepository.AddAsync(
				new()
				{
					Name = model.Name,
					Price = model.Price,
					Stock = model.Stock,
				}
			);
			await _productWriteRepository.SaveAsync();
			return StatusCode((int)HttpStatusCode.Created);
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
		public async Task<IActionResult> Upload()
		{
			var result = await _fileService.UploadFilesAsync(
				"resource/product-images",
				Request.Form.Files
			);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(string id)
		{
			await _productWriteRepository.RemoveAsync(id);
			await _productWriteRepository.SaveAsync();
			return Ok();
		}
	}
}
