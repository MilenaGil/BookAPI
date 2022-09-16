using BookAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.DbContexts
{
    public class BookInfoContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Hero> Heroes { get; set; } = null!;

        public BookInfoContext(DbContextOptions<BookInfoContext> options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite($"Data Source=BookInfo.db");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book("tytul1")
                {
                    Id = 1,
                    Description = "ksiazka nr 1"
                },
                new Book("tytul2")
                {
                    Id = 2,
                    Description = "ksiazka nr 2"
                },
                new Book("tytul3")
                {
                    Id = 3,
                    Description = "ksiazka nr 3"
                }
                );

            modelBuilder.Entity<Hero>().HasData(
                new Hero("postac1")
                {
                    Id = 1,
                    BookId = 1,
                    Description = "bohater nr 1"
                },
                new Hero("postac2")
                {
                    Id = 2,
                    BookId = 1,
                    Description = "bohater nr 2"
                },
                new Hero("postac3")
                {
                    Id = 3,
                    BookId = 2,
                    Description = "bohater nr 3"
                }
                );

            base.OnModelCreating(modelBuilder);
        }



        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("connectionstring");
            base.OnConfiguring(optionsBuilder);
        }*/
    }
}
