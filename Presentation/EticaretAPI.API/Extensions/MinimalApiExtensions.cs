﻿using EticaretAPI.Application.Operations;
using EticaretAPI.Application.Repositories;
using EticaretAPI.Application.RequestParameters;
using EticaretAPI.Application.ViewModels;
using EticaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EticaretAPI.API.Extensions;

public static class MinimalApiExtensions
	{
	public static void MapProductsEndPoint(this IEndpointRouteBuilder app) {
		// Asenkron bir lambda fonksiyonu ve dependency injection kullanıyoruz
		app.MapPost("/api/products/CreateProduct" , async (VM_Create_Product model , IProductWriteRepository _productWriteRepository) =>
		{
			// Yeni bir Product oluşturuyoruz ve repository'ye ekliyoruz
			await _productWriteRepository.AddAsync(new Product
				{
				Name = model.Name ,
				Price = model.Price ,
				Stock = model.Stock
				});
			// Başarı durumunda 201 Created dönüyoruz
			return Results.StatusCode((int) HttpStatusCode.Created);
		});
		app.MapPut("/UpdateProduct" , (VM_Update_Product model , IProductService _productService) =>
		{
			_productService.UpdateProductAsync(model);
			return Results.StatusCode((int) HttpStatusCode.OK);
		});

		app.MapGet("/GetProduct" , async (string id , IProductService _productService) =>
		{
			Product product = await _productService.GetProductByIdAsync(id);
			return Results.Ok(product);
		});
		app.MapGet("/GetProducts" , async (IProductService _productService) =>
		{
			List<Product> products = await _productService.GetProductsAsync();
			return Results.Ok(products);
		});
		app.MapGet("/api/products/GetProductsPaging" , async ([FromQuery] int page , [FromQuery] int size , [FromServices] IProductService _productService) =>
		{
			var pagination = new Paginations { Page = page, Size = size }; // Parametrelerden Pagination nesnesi oluşturuyoruz

			PagingResult<Product> products = await _productService.GetProductsPagingAsync(pagination);
			return Results.Ok(products);
		});
		app.MapGet("/GetProductImages" , async (string id , IProductService _productService) =>
		{
			await _productService.GetProductImages(id);
			return Results.Ok();
		});
		app.MapDelete("/DeleteProductImage" , async (string id , string imageId , IProductService _productService) =>
		{
			await _productService.DeleteProductImageAsync(id , imageId);
			return Results.Ok();
		});
		app.MapPost("/UploadProductImage" , async (string id , List<FormFile> files , IProductService _productService) =>
		{
			ArgumentNullException.ThrowIfNull(files , nameof(files));

			List<string> fileNames = files.Select(file => file.FileName).ToList();
			var result =  await _productService.UploadProductFilesAsync(id , fileNames);
			return Results.Ok(result);
		});
		}
	}