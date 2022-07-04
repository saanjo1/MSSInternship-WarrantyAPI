using AutoMapper;
using WarrantyAPI.Models;
namespace WarrantyAPI.MappingProfiles
{
    public class WarrantyProfile : Profile
    {
        public WarrantyProfile()
        {
            CreateMap<Warranty, WarrantyTableEntity>().ReverseMap();
        }
    }
}
