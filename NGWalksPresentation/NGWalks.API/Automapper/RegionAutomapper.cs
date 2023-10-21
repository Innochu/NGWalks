using AutoMapper;
using NGWalksDomain.ModelDTO;
using NGWalksDomain.Models;

namespace NGWalks.Presentation.Automapper
{
	public class RegionAutomapper : Profile
	{
        public RegionAutomapper()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<CreateRegionDTO, Region>().ReverseMap();
			CreateMap<UpdateRegionDTO, Region>().ReverseMap();
		}
    }
}
