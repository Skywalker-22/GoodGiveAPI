using GoodGive.DAL.Entities;
using GoodGive.DAL.Seeders.Interfaces;

namespace GoodGive.DAL.Seeders.Implementations;

public class CharitySeeder : ICharitySeeder
{
    public List<Charity> GetCharitySeedData(DateTime currentDate)
    {
        var userSeedId = Guid.NewGuid();

        var charities = new List<Charity>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Global Relief",
                Description = "Providing global disaster relief.",
                Email = "contact@globalrelief.example.com",
                Phone = "111-222-3333",
                Category = "Disaster Relief",
                Website = "https://www.globalrelief.example.com",
                CreatedBy = userSeedId,
                CreatedDate = currentDate,
                IsDeleted = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Education for All",
                Description = "Promoting education worldwide.",
                Email = "info@educationforall.example.com",
                Phone = "222-333-4444",
                Category = "Education",
                Website = "https://www.educationforall.example.com",
                CreatedBy = userSeedId,
                CreatedDate = currentDate,
                IsDeleted = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "HealthBridge",
                Description = "Bridging the gap in healthcare.",
                Email = "support@healthbridge.example.com",
                Phone = "333-444-5555",
                Category = "Healthcare",
                Website = "https://www.healthbridge.example.com",
                CreatedBy = userSeedId,
                CreatedDate = currentDate,
                IsDeleted = false
            }
        };

        return charities;
    }
}
