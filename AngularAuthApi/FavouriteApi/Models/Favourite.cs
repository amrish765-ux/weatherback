using FavouriteApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FavouriteApi.Models
{
    public class Favourite
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FavId { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string country { get; set; }
        [Required]
        public string temp { get; set; }
        [Required]
        public int UserID { get; set; }
        public User User { get; set; }


    }
}
