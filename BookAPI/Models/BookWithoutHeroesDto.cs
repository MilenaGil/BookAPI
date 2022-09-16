namespace BookAPI.Models
{
    /// <summary>
    /// A DTO for a book without heroes
    /// </summary>
    public class BookWithoutHeroesDto
    {
        /// <summary>
        /// The ID of book
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The book's title
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// The description of this book
        /// </summary>
        public string? Description { get; set; }
    }
}
 