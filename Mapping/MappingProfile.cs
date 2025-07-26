using AutoMapper;
using WalksAPI.Models.Domain;
using WalksAPI.Models.DTOs.Difficultys;
using WalksAPI.Models.DTOs.Regions;
using WalksAPI.Models.DTOs.WalksDTO;

namespace WalksAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, AddRegionRequestDTO>().ReverseMap();
            CreateMap<Region, EditRegionREquestDTO>().ReverseMap();

            CreateMap<Walks, WalksDTO>();
            CreateMap<Region, RegionDTO>();
            CreateMap<Difficulty, DifficultyDTO>();
            CreateMap<Walks, CreateWalksDTO>().ReverseMap();

        }
    }
}
