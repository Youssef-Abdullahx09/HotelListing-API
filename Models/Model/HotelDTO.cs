using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class CreateHotelDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Name Is Too Long")]
        public string Name { get; set; }


        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Address Is Too Long")]
        public string Address { get; set; }

        [Required]
        [Range(0, 5)]
        public double Rating { get; set; }

        public int CountryId { get; set; }

    }
    public class HotelDTO : CreateHotelDTO
    {
        [Required]
        public int Id { get; set; }
        public CountryDTO Country { get; set; }
    }
}
