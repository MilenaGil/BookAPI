using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookAPI.Entities
{
    public class Hero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public Book? Book { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }

        [ForeignKey("BookId")]
        public int BookId { get; set; }

        public Hero(string name)
        {
            Name=name;
        }
    }
}
