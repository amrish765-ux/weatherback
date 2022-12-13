
using FavouriteApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FavouriteApi.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        //[Required(ErrorMessage = "First Name is Required")]
        //[StringLength(2, ErrorMessage = "Name must not be more than 2 char")]
        public string FirstName { get; set; }


        //[Required(ErrorMessage = "First Name is Required")]
        //[Range(2,10)]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Email is Required")]
        //[RegularExpression(@"^\w + ([\.-] ?\w +)*@\w+([\.-]?\w+)*(\.\w{2,3})+$",ErrorMessage ="check Email")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "User Name is Required")]
        //[Range(6, 10, ErrorMessage = "Please Provide correct range. It should be minimum 6 and not more than 10 ")]
        public string Username { get; set; }
        public string Token { get; set; }

        public string Role { get; set; }

        //[Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        public ICollection<Favourite> favourites { get; set; }

    }
}
