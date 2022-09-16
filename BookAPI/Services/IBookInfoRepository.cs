using BookAPI.Entities;
using CityInfo.API.Services;

namespace BookAPI.Services
{
    public interface IBookInfoRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<(IEnumerable<Book>, PaginationMetadata)> GetBooksAsync(string? title, string? searchQuery, int pageNumber, int pageSize);
        Task<Book?> GetBookAsync(int bookId, bool includeHeroes);
        Task<bool> BookExistsAsync(int bookId);
        Task<IEnumerable<Hero>> GetHeroesAsync(int bookId);
        Task<Hero?> GetHeroAsync(int bookId, int heroId);
        Task AddHeroForBookAsync(int bookId, Hero hero);
        void DeleteHero(Hero hero);
        Task<bool> BookTitleMatchesBookId (string? bookTitle, int bookId);
        Task<bool> SaveChangesAsync();
    }
}
