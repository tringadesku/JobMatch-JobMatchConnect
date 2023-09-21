using System.ComponentModel.DataAnnotations;

namespace ProfileMatching.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime Birthyear { get; set; }

    }
}
