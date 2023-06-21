using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.Configuration.Entity
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Sandel Resort And Spa",
                    Address = "Negril",
                    CountryId = 1,
                    Rating = 4.5,
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Comfort Suites",
                    Address = "George Town",
                    CountryId = 3,
                    Rating = 4.3,
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Grand Paledium",
                    Address = "Naussa",
                    CountryId = 2,
                    Rating = 4.0,
                });
        }
    }
}
