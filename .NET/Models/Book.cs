using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileMatching.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public int? AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        public string Title { get; set; }
        public DateTime PublicationYear { get; set; }

    }
}
