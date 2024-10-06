using EticaretAPI.API.Helpers.Common;
using EticaretAPI.Application.Features.Commands.ApplicationUser.Login;
using EticaretAPI.Application.Features.Commands.ApplicationUser.Register;
using EticaretAPI.Application.Features.Commands.Product.Create;
using EticaretAPI.Application.Features.Commands.Product.Remove;
using EticaretAPI.Application.Features.Commands.Product.Update;
using EticaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using EticaretAPI.Application.Features.Commands.ProductImageFile.Upload;
using EticaretAPI.Application.Features.Queries.GetProductImage;
using EticaretAPI.Application.Features.Queries.Product.GetAllProduct;
using EticaretAPI.Application.Features.Queries.Product.GetByIdProduct;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.API.Extensions;

public static class MinimalApiExtensions
{
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

	public static void MapProductsEndPoint(this IEndpointRouteBuilder app)
	{
		app.MapGet(
			"/api/products/GetProductsPaging",
			async (
				[FromServices] IMediator _mediator,
				[AsParameters] GetAllProductQueryRequest request
			) =>
			{
				return Results.Ok(await _mediator.Send(request, cancellationToken: default));
			}
		);

		app.MapGet(
			"/api/products/GetProduct",
			async (
				[AsParameters] GetByIdProductQueryRequest request,
				[FromServices] IMediator _mediator,
				CancellationToken cancellationToken
			) =>
			{
				return Results.Ok(await _mediator.Send(request, cancellationToken));
			}
		);

		app.MapPost(
			"/api/products/CreateProduct",
			async (
				[FromServices] IMediator _mediator, // FromServices ile Mediator DI'dan çekilir
				[FromBody] CreateProductCommandRequest request, // Body'den bağlanır
				[FromServices] IValidator<CreateProductCommandRequest> validator
			) =>
			{
				var validationResult = await validator.ValidateAsync(request);
				if (!validationResult.IsValid)
				{
					return Results.BadRequest(validationResult.Errors);
				}
				return Results.Ok(await _mediator.Send(request));
			}
		);

		app.MapPut(
			"/api/products/UpdateProduct",
			async (
				[FromServices] IMediator _mediator,
				[FromBody] UpdateProductCommandRequest model
			) =>
			{
				var response = await _mediator.Send(model);
				return Results.StatusCode(response.StatusCode);
			}
		);

		app.MapDelete(
			"/api/products/DeleteProduct/{id}",
			async (
				[FromServices] IMediator _mediator,
				[AsParameters] RemoveProductCommandRequest request,
				CancellationToken cancellationToken
			) =>
			{
				var response = await _mediator.Send(request, cancellationToken);
				return Results.StatusCode(response.StatusCode);
			}
		);

		app.MapGet(
			"/api/products/GetProductImages/{Id}",
			async (
				[FromServices] IMediator _mediator,
				[AsParameters] GetProductImageQueryRequest request,
				CancellationToken cancellationToken
			) =>
			{
				return Results.Ok(await _mediator.Send(request));
			}
		);

		app.MapDelete(
			"/api/products/DeleteProductImage/{Id}",
			async (
				[FromServices] IMediator Mediator,
				[AsParameters] RemoveProductImageCommandRequest request,
				CancellationToken cancellationToken
			) =>
			{
				return Results.Ok(await Mediator.Send(request));
			}
		);

		app.MapPost(
			"/api/products/UploadProductImage",
			async (
				IMediator _mediator,
				[FromQuery] string id, // Route'dan Id alıyoruz
				[FromForm] IFormFileCollection files,
				CancellationToken cancellationToken
			) =>
			{
				var request = new UploadProductImageCommandRequest
				{
					Id = id,
					Files = FileService.ConvertToFileDtos(files),
				};
				return Results.Ok(await _mediator.Send(request, cancellationToken));
			}
		);
	}

	public static void MapUsersEndPoint(this IEndpointRouteBuilder app)
	{
		app.MapPost(
			"/api/users/CreateUser",
			async (
				[FromServices] IMediator _mediator,
				[FromBody] CreateUserCommandRequest request,
				[FromServices] IValidator<CreateUserCommandRequest> validator
			) =>
			{
				var validationResult = await validator.ValidateAsync(request);
				if (!validationResult.IsValid)
					return Results.BadRequest(validationResult.Errors);
				return Results.Ok(await _mediator.Send(request));
			}
		);

		app.MapPost(
			"/api/users/LoginUser",
			async (
				[FromServices] IMediator _mediator,
				[FromBody] LoginUserCommandRequest request
			) =>
			{
				return Results.Ok(await _mediator.Send(request));
			}
		);
	}
}
