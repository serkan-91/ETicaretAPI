using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.MappingProfiles
	{
	public class MappingProfile : Profile
		{
		public MappingProfile()
			{
			CreateMap<Product , VM_Create_Product>().ReverseMap();
			CreateMap<Product , VM_Update_Product>().ReverseMap();
			}
		}
	}