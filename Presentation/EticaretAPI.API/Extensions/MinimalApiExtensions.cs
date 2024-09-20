using System.Net;
using EticaretAPI.API.Helpers.Common;
using EticaretAPI.Application.Common.Dtos;
using EticaretAPI.Application.Operations;
using EticaretAPI.Application.Repositories;
using EticaretAPI.Application.RequestParameters;
using EticaretAPI.Application.ResponseParameters;
using EticaretAPI.Application.ViewModels;
using EticaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.API.Extensions;

public static class MinimalApiExtensions
{
    public static void MapProductsEndPoint(this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/products/CreateProduct",
            async (VM_Create_Product model, IProductWriteRepository _productWriteRepository) =>
            {
                await _productWriteRepository
                    .AddAsync(
                        new Product
                        {
                            Name = model.Name,
                            Price = model.Price,
                            Stock = model.Stock,
                        },
                        cancellationToken: default
                    )
                    .ConfigureAwait(false);

                return Results.StatusCode((int)HttpStatusCode.Created);
            }
        );

        app.MapPut(
            "/api/products/UpdateProduct",
            (VM_Update_Product model, IProductServices _productService) =>
            {
                _productService.UpdateProductAsync(model);
                return Results.StatusCode((int)HttpStatusCode.OK);
            }
        );

        app.MapDelete(
            "/api/products/DeleteProduct/{id}",
            async (
                string id,
                IProductServices _productService,
                CancellationToken cancellationToken
            ) =>
            {
                await _productService
                    .DeleteProductAsync(id, cancellationToken)
                    .ConfigureAwait(false);
                return Results.StatusCode((int)HttpStatusCode.OK);
            }
        );

        app.MapGet(
            "/api/products/GetProduct",
            async (
                string id,
                IProductServices _productService,
                CancellationToken cancellationToken
            ) =>
            {
                Product product = await _productService
                    .GetProductByIdAsync(id, cancellationToken)
                    .ConfigureAwait(false);
                return Results.Ok(product);
            }
        );

        app.MapGet(
            "/api/products/GetProducts",
            async (IProductServices _productService, CancellationToken cancellationToken) =>
            {
                List<Product> products = await _productService
                    .GetProductsAsync(cancellationToken)
                    .ConfigureAwait(false);
                return Results.Ok(products);
            }
        );

        app.MapGet(
            "/api/products/GetProductsPaging",
            async (
                [FromQuery] int page,
                [FromQuery] int size,
                [FromServices] IProductServices _productService,
                CancellationToken cancellationToken
            ) =>
            {
                var pagination = new Paginations { Page = page, Size = size }; // Parametrelerden Pagination nesnesi oluşturuyoruz

                PagingResult<Product> products = await _productService
                    .GetProductsPagingAsync(pagination, cancellationToken)
                    .ConfigureAwait(false);
                return Results.Ok(products);
            }
        );

        app.MapGet(
            "/api/products/GetProductImages/{id}",
            static async (
                string id,
                IProductServices _productService,
                CancellationToken cancellationToken
            ) =>
            {
                var images = await _productService
                    .GetProductImagesAsync(id, cancellationToken)
                    .ConfigureAwait(false);
                return Results.Ok(images);
            }
        );

        app.MapDelete(
            "/api/products/DeleteProductImage/{id}",
            async (
                string id,
                string imageId,
                IProductServices _productService,
                CancellationToken cancellationToken
            ) =>
            {
                await _productService
                    .DeleteProductImageAsync(id, imageId, cancellationToken)
                    .ConfigureAwait(false);
                return Results.Ok();
            }
        );

        app.MapPost(
            "/api/products/UploadProductImage",
            async (
                HttpContext context,
                string id,
                [FromForm] IFormFileCollection files,
                IProductServices _productService,
                CancellationToken cancellationToken
            ) =>
            {
                ArgumentNullException.ThrowIfNull(files, nameof(files));

                UpladImageResults result = await _productService
                    .UploadProductFilesAsync(
                        id,
                        FileService.ConvertToFileDtos(files),
                        cancellationToken
                    )
                    .ConfigureAwait(false);
                return Results.Ok(result);
            }
        );
    }

    public static void MapCrfTokenEndPoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/csrf-token",
            (IAntiforgery antiforgery, HttpContext httpContext) =>
            {
                AntiforgeryTokenSet tokens = antiforgery.GetAndStoreTokens(httpContext);
                return Results.Ok(new { csrfToken = tokens.RequestToken });
            }
        );
    }
}
