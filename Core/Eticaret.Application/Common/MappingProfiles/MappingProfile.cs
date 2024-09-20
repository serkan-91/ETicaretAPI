using AutoMapper;

namespace EticaretAPI.Application.Common.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, VM_Create_Product>().ReverseMap();
            CreateMap<Product, VM_Update_Product>().ReverseMap();
        }
    }
}
