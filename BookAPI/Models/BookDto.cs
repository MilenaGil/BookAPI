namespace BookAPI.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int NumberOfHeroes
        {
            get
            {
                return Heroes.Count;
            }
        }

        public ICollection<HeroDto> Heroes { get; set; } = new List<HeroDto>();
    }
}
