using AutoMapper;

namespace BookAPI.Profiles
{
    public class HeroProfile : Profile
    {
        public HeroProfile()
        {
            CreateMap<Entities.Hero, Models.HeroDto>();
            CreateMap<Models.HeroForCreationDto, Entities.Hero>();
            CreateMap<Models.HeroForUpdateDto, Entities.Hero>();
            CreateMap<Entities.Hero, Models.HeroForUpdateDto>();
        }
    }
}
