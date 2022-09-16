using AutoMapper;
using BookAPI.Models;
using BookAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/books/{bookId}/heroes")]  
    public class HeroesController : ControllerBase
    {
        private readonly ILogger<HeroesController> _logger;
        private readonly IMailService _mailService;
        //private readonly BooksDataStore _booksDataStore;
        private readonly IMapper _mapper;
        private readonly IBookInfoRepository _bookInfoRepository;

        //public HeroesController(ILogger<HeroesController> logger, IMailService mailService, BooksDataStore booksDataStore)
        public HeroesController(ILogger<HeroesController> logger, IMailService mailService,IBookInfoRepository bookInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            //_booksDataStore=booksDataStore ?? throw new ArgumentNullException(nameof(booksDataStore)); 
            _bookInfoRepository = bookInfoRepository ?? throw new ArgumentNullException(nameof(bookInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeroDto>>> GetHeroes(int bookId)
        {
            //var bookTitle = User.Claims.FirstOrDefault(c => c.Type == "book")?.Value;
            // if(!(await _bookInfoRepository.BookTitleMatchesBookId(bookTitle, bookId)))
            //{
            //    return Forbid(bookTitle);
            //}

            if(!await _bookInfoRepository.BookExistsAsync(bookId))
            {
                _logger.LogInformation($"Book with id {bookId} wasn't found when accessing heroes");
                return NotFound();
            }

            var heroesForBook = await _bookInfoRepository.GetHeroesAsync(bookId);

            return Ok(_mapper.Map<IEnumerable<HeroDto>>(heroesForBook));

            /*(1)try 
            {
                var book = _booksDataStore.Books.FirstOrDefault(c => c.Id == bookId);

                if (book == null)
                {
                    _logger.LogInformation($"Book with id {bookId} wasn't found when accessing heroes");
                    return NotFound();
                }

                return Ok(book.Heroes);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting heroes book with id {bookId}.", ex);
                return StatusCode(500, "A problem happend while handing your request");
            }*/
        }

        [HttpGet("{heroId}", Name = "GetHero")]
        public async Task<ActionResult<HeroDto>> GetHero(int bookId, int heroId)
        //public ActionResult<HeroDto> GetHero(int bookId, int heroId)
        {
            if (!await _bookInfoRepository.BookExistsAsync(bookId))
            {
                return NotFound();
            }
            var hero = await _bookInfoRepository.GetHeroAsync(bookId, heroId);

            if (hero == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<HeroDto>(hero));

            /*var book = _booksDataStore.Books.FirstOrDefault(c => c.Id == bookId);

            if (book == null)
            {
                return NotFound();
            }

            var hero = book.Heroes.FirstOrDefault(c => c.Id == heroId);

            if (hero == null)
            {
                return NotFound();
            }

            return Ok(hero);*/
        }

        /*        [HttpPost]
                public ActionResult<HeroDto> CreateHero(int bookId, HeroForCreationDto hero)
                {

                    var book = _booksDataStore.Books.FirstOrDefault(c => c.Id == bookId);

                    if (book == null)
                    {
                        return NotFound();
                    }
                    var maxHeroId = _booksDataStore.Books.SelectMany(c => c.Heroes).Max(p => p.Id);

                    var finalHero = new HeroDto()
                    {
                        Id = ++maxHeroId,
                        Name = hero.Name,
                        Description = hero.Description

                    };

                    book.Heroes.Add(finalHero);

                    return CreatedAtRoute("GetHero",
                        new
                        {
                            bookId = bookId,
                            heroId = finalHero.Id
                        },
                        finalHero);
                }

                [HttpPut("{heroid}")]
                public ActionResult UpdateHero(int bookId, int heroId, HeroForUpdateDto hero)
                {
                    var book = _booksDataStore.Books.FirstOrDefault(c => c.Id == bookId);
                    if (book == null)
                    {
                        return NotFound();
                    }

                    var heroFromStore = book.Heroes.FirstOrDefault(c => c.Id == heroId);
                    if (heroFromStore == null)
                    {
                        return NotFound();
                    }

                    heroFromStore.Name = hero.Name;
                    heroFromStore.Description = hero.Description;

                    return NoContent();
                }

                [HttpPatch("{heroid}")]
                public ActionResult PartiallyUpdateHero(int bookId, int heroId, JsonPatchDocument<HeroForUpdateDto> patchDocument)
                {
                    var book = _booksDataStore.Books.FirstOrDefault(c => c.Id == bookId);
                    if (book == null)
                    {
                        return NotFound();
                    }

                    var heroFromStore = book.Heroes.FirstOrDefault(c => c.Id == heroId);
                    if (heroFromStore == null)
                    {
                        return NotFound();
                    }

                    var heroToPatch =
                        new HeroForUpdateDto()
                        {
                            Name = heroFromStore.Name,
                            Description = heroFromStore.Description
                        };

                    patchDocument.ApplyTo(heroToPatch, ModelState);

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    if (!TryValidateModel(heroToPatch))
                    {
                        return BadRequest(ModelState);
                    }

                    heroFromStore.Name = heroToPatch.Name;
                    heroFromStore.Description = heroToPatch.Description;

                    return NoContent();
                }

                [HttpDelete("{heroId}")]
                public ActionResult DeleteHero(int bookId, int heroId)
                {
                    var book = _booksDataStore.Books.FirstOrDefault(c => c.Id == bookId);
                    if (book == null)
                    {
                        return NotFound();
                    }

                    var heroFromStore = book.Heroes.FirstOrDefault(c => c.Id == heroId);

                    if (heroFromStore == null)
                    {
                        return NotFound();
                    }

                    book.Heroes.Remove(heroFromStore);
                    _mailService.Send("Hero deleted.", $"Hero {heroFromStore.Name} with id {heroFromStore.Id} was deleted.");
                    return NoContent();
                }*/

        //Rozwiązanie async (czyli 2) od POSTa w dol

        [HttpPost]
        public async Task<ActionResult<HeroDto>> CreateHero(int bookId, HeroForCreationDto hero)
        {
            if (!await _bookInfoRepository.BookExistsAsync(bookId))
            {
                return NotFound();
            }

            var finalHero = _mapper.Map<Entities.Hero>(hero);

            await _bookInfoRepository.AddHeroForBookAsync(bookId, finalHero);

            await _bookInfoRepository.SaveChangesAsync();

            var createdHeroToReturn =_mapper.Map<Models.HeroDto>(finalHero);

            return CreatedAtRoute("GetHero",
                 new
                 {
                     bookId = bookId,
                     heroId = createdHeroToReturn.Id
                 },
                 createdHeroToReturn);
        }

        [HttpPut("{heroId}")]
        public async Task<ActionResult> UpdateHero(int bookId, int heroId, HeroForUpdateDto hero)
        {
            if (!await _bookInfoRepository.BookExistsAsync(bookId))
            {
                return NotFound();
            }

            var heroEntity = await _bookInfoRepository.GetHeroAsync(bookId, heroId);
            if (heroEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(hero, heroEntity);

            await _bookInfoRepository.SaveChangesAsync();

            return NoContent();
        }


        [HttpPatch("{heroId}")]
        public async Task<ActionResult> PartiallyUpdateHero(int bookId, int heroId,JsonPatchDocument<HeroForUpdateDto> patchDocument)
        {
            if (!await _bookInfoRepository.BookExistsAsync(bookId))
            {
                return NotFound();
            }
            
            var heroEntity = await _bookInfoRepository.GetHeroAsync(bookId, heroId);
               if (heroEntity == null)
               {
                   return NotFound();
               }
               
            var heroToPatch = _mapper.Map<HeroForUpdateDto>(heroEntity);
            
            patchDocument.ApplyTo(heroToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(heroToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(heroToPatch, heroEntity);
            await _bookInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{heroId}")]
        public async Task<ActionResult> DeleteHero(int bookId, int heroId)
        {
            if (!await _bookInfoRepository.BookExistsAsync(bookId))
            {
                return NotFound();
            }

            var heroEntity = await _bookInfoRepository.GetHeroAsync(bookId, heroId);
            if (heroEntity == null)
            {
                return NotFound();
            }

            _bookInfoRepository.DeleteHero(heroEntity);
            await _bookInfoRepository.SaveChangesAsync();

            _mailService.Send("Hero deleted.", $"Hero {heroEntity.Name} with id {heroEntity.Id} was deleted.");
            return NoContent();
        }
    }

}
