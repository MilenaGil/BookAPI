using AutoMapper;

namespace BookAPI.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Entities.Book, Models.BookWithoutHeroesDto>();
            CreateMap<Entities.Book, Models.BookDto>();
        }
    }
}
