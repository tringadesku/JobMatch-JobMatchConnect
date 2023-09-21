using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileMatching.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        public int? TeamId { get; set; }

        [ForeignKey("TeamId")]
        public Author Team { get; set; }
        public string PlayerName { get; set; }
        public int Number { get; set; }

    }
}