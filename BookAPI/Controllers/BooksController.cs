using AutoMapper;
using BookAPI.Models;
using BookAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookAPI.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/books")]
    public class BooksController : ControllerBase
    {
        
        private readonly IBookInfoRepository _bookInfoRepositor;
        private readonly IMapper _mapper;
        const int maxBooksPageSize = 20;

        /*private readonly BooksDataStore _booksDataStore;
        public BooksController(BooksDataStore booksDataStore)
        {
            _booksDataStore=booksDataStore;
        }*///poprzednia wersja

        public BooksController(IBookInfoRepository bookInfoRepositor, IMapper mapper)
        {
            _bookInfoRepositor=bookInfoRepositor ?? throw new ArgumentNullException(nameof(bookInfoRepositor));
            _mapper=mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        //public ActionResult<IEnumerable<BookDto>> GetBooks()
        public async Task<ActionResult<IEnumerable<BookWithoutHeroesDto>>> GetBooks(
            /*[FromQuery]*/string? title,string? searchQuery, int pageNumber = 1 , int pageSize = 10)
        {
            if (pageSize > maxBooksPageSize)
            {
                pageSize = maxBooksPageSize;
            }

            var (bookEntities, paginationMetadata) = await _bookInfoRepositor.GetBooksAsync(title, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<BookWithoutHeroesDto>>(bookEntities));

            /*(2)var result = new List<BookWithoutHeroesDto>();
            foreach(var bookEntity in bookEntities)
            {
                result.Add(new BookWithoutHeroesDto
                {
                    Id = bookEntity.Id,
                    Description = bookEntity.Description,
                    Title = bookEntity.Title
                });
            }
            return Ok(result);*/
            //(1)return Ok(BooksDataStore.Current.Books);
        }

        /// <summary>
        /// Get a book by ID.
        /// </summary>
        /// <param name="id">The id of the book to get</param>
        /// <param name="includeHeroes">Where or not to include heroes</param>
        /// <returns>An IAction Result</returns>
        /// <response code="200">Returns the requested book to get</response>

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBook(int id, bool includeHeroes = false)
        {
            /*var bookToReturn = BooksDataStore.Current.Books.FirstOrDefault(c => c.Id ==id);

            if (bookToReturn == null)
            {
                return NotFound();
            }

            return Ok(bookToReturn);*/
            var book = await _bookInfoRepositor.GetBookAsync(id, includeHeroes);
            if (book == null)
            {
                return NotFound();
            }
            if(includeHeroes)
            {
                return Ok(_mapper.Map<BookDto>(book));
            }
            return Ok(_mapper.Map<BookWithoutHeroesDto>(book));

        }
    }
}
