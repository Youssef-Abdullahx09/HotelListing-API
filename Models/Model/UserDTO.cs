using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)] 
        public string Email { get; set;}

        [Required]
        public string Password { get; set; }

        public ICollection<string> Roles { get; set; }

    }
}
