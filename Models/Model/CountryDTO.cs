using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class CreateCountryDTO
    {

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country Name is too long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 2, ErrorMessage = "Country Short Name is too long")]
        public string ShortName { get; set; }
        
        [Required]
        public int HotelId { get; set; }
    }
    public class CountryDTO : CreateCountryDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public IList<HotelDTO> Hotel { get; set; }
    }
}
