using AutoMapper;
using EticaretAPI.Application.Features.Commands.ApplicationUser.Register;
using EticaretAPI.Application.Features.Commands.Product.Create;
using EticaretAPI.Application.Features.Commands.Product.Update;
using EticaretAPI.Application.Features.Commands.ProductImageFile.Upload;
using EticaretAPI.Application.Features.Queries.GetProductImage;
using EticaretAPI.Application.Features.Queries.Product.GetByIdProduct;
using EticaretAPI.Application.ResponseParameters;
using EticaretAPI.Domain.Entities.Identity;

namespace EticaretAPI.Application.Common.MappingProfiles {
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, CreateProductCommandRequest>().ReverseMap();

			CreateMap<CreateProductCommandResponse, Product>().ReverseMap();

			CreateMap<GetByIdProductQueryResponse, Product>().ReverseMap();

			CreateMap<UpdateProductCommandRequest, Product>().ReverseMap();

			CreateMap<ProductImages, GetProductImageQueryResponse>().ReverseMap();

			CreateMap<UploadProductImageCommandResponse, ProductImageFile>().ReverseMap();
			CreateMap<GetProductImageQueryResponse, ProductImages>().ReverseMap();
			CreateMap<ProductImageFile, ProductImages>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
			CreateMap<ApplicationUser, CreateUserCommandRequest>().ReverseMap();
		}
	}
}
