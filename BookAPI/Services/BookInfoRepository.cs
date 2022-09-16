using BookAPI.DbContexts;
using BookAPI.Entities;
using CityInfo.API.Services;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Services
{
    public class BookInfoRepository : IBookInfoRepository
    {
        private readonly BookInfoContext _context;
        public BookInfoRepository(BookInfoContext context)
        {
            _context=context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> BookTitleMatchesBookId(string? bookTitle, int bookId)
        {
            return await _context.Books.AnyAsync( c => c.Id == bookId && c.Title == bookTitle);
        }
        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books.OrderBy(c =>c.Title).ToListAsync();
        }

        public async Task<(IEnumerable<Book>, PaginationMetadata)> GetBooksAsync(string? title, string? searchQuery, int pageNumber, int pageSize)
        {
           /* if (string.IsNullOrEmpty(title) && string.IsNullOrWhiteSpace(searchQuery))
            {
                return await GetBooksAsync();
            }*///(bo nie chcemy by w razie braku wyszukiwan wypisalo nam wszystko.

            //collection to start from
            var collection = _context.Books as IQueryable<Book>;

            if (!string.IsNullOrWhiteSpace(title))
            {
                title = title.Trim();
                collection = collection.Where(c => c.Title == title);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Title.Contains(searchQuery)
                    || (a.Description != null && a.Description.Contains(searchQuery)));
            }
            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.Title).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

            return (collectionToReturn, paginationMetadata);

           /* (code from filtring demo)title = title.Trim();
            return await _context.Books.Where(c => c.Title == title).OrderBy(c => c.Title).ToListAsync();*/
        }

        public async Task<Book?> GetBookAsync(int bookId, bool includeHeroes)
        {
            if(includeHeroes)
            {
                return await _context.Books.Include(c => c.Heroes).Where(c => c.Id == bookId).FirstOrDefaultAsync();
            }
            return await _context.Books.Where(c => c.Id == bookId).FirstOrDefaultAsync();
        }

        public async Task<bool> BookExistsAsync(int bookId)
        {
            return await _context.Books.AnyAsync(c => c.Id == bookId);
        }

        public async Task<IEnumerable<Hero>> GetHeroesAsync(int bookId)
        {
            return await _context.Heroes.Where(p => p.BookId == bookId).ToListAsync();
        }

        public async Task<Hero?> GetHeroAsync(int bookId, int heroId)
        {
            return await _context.Heroes.Where(p => p.BookId == bookId && p.Id == heroId).FirstOrDefaultAsync();
        }

        public async Task AddHeroForBookAsync(int bookId, Hero hero)
        {
            var book = await GetBookAsync(bookId, false);
            if (book != null)
            {
                book.Heroes.Add(hero);
            } 
        }

        public void DeleteHero(Hero hero)
        {
            _context.Heroes.Remove(hero);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
