using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularAuthApi.Models
{
    public class Favourite
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FavId { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string country { get; set; }
        [Required]
        public string temp { get; set; }
        public ICollection<User> favourites { get; set; }

    }
}
