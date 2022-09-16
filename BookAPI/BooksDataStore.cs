using BookAPI.Models;

namespace BookAPI
{
    public class BooksDataStore
    {
        public List<BookDto> Books { get; set; }
        public static BooksDataStore Current { get; } = new BooksDataStore();
        public BooksDataStore()
        {
            Books = new List<BookDto>()
            {
                new BookDto()
                {
                    Id = 1,
                    Title = "Tytul1",
                    Description = "Cos o ksiazce tytul1",
                    Heroes = new List<HeroDto>()
                    {
                        new HeroDto()
                        {
                            Id=1,
                            Name="Bohater1_ksiazki1",
                            Description="Mily"
                        },
                        new HeroDto()
                        {
                            Id=2,
                            Name="Bohater2_ksiazki1",
                            Description="Szarmancka"
                        }
                    }
                },
                new BookDto()
                {
                    Id = 2,
                    Title = "Tytul2",
                    Description = "Wiele o tytul2",
                    Heroes = new List<HeroDto>()
                    {
                        new HeroDto()
                        {
                            Id=3,
                            Name="Bohater3_ksiazki2",
                            Description="Wredna"
                        },
                        new HeroDto()
                        {
                            Id=4,
                            Name="Bohater4_ksiazki2",
                            Description="Pochmurna"
                        }
                    }
                },
                new BookDto()
                {
                    Id = 3,
                    Title = "Tytul3",
                    Description = "Notatka do tytul3",
                    Heroes = new List<HeroDto>()
                    {
                        new HeroDto()
                        {
                            Id=5,
                            Name="Bohater5_ksiazki3",
                            Description="Bajkowy"
                        },
                        new HeroDto()
                        {
                            Id=6,
                            Name="Bohater6_ksiazki3",
                            Description="Potezna"
                        }
                    }
                }
            };
        }
    }
}
