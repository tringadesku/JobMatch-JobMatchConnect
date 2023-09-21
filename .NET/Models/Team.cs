using System.ComponentModel.DataAnnotations;

namespace ProfileMatching.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string TeamName { get; set; }

    }
}