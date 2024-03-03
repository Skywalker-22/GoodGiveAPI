using GoodGive.DAL.Entities;
using GoodGive.DAL.Seeders.Interfaces;

namespace GoodGive.DAL.Seeders.Implementations;

public class DonationSeeder : IDonationSeeder
{
    public List<Donation> GetDonationSeedData(DateTime currentDate, Guid charityId, Guid userId)
    {
        var userSeedId = Guid.NewGuid();

        var donations = new List<Donation>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Amount = 100m,
                Date = DateTime.UtcNow,
                Message = "Keep up the great work!",
                IsAnonymous = false,
                UserId = userId,
                CharityId = charityId,
                CreatedBy = userSeedId,
                CreatedDate = currentDate,
                IsDeleted = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                Amount = 50m,
                Date = DateTime.UtcNow.AddDays(-1),
                Message = "In memory of John Doe.",
                IsAnonymous = true,
                UserId = userId,
                CharityId = charityId,
                CreatedBy = userSeedId,
                CreatedDate = currentDate,
                IsDeleted = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                Amount = 200m,
                Date = DateTime.UtcNow.AddDays(-2),
                Message = "Every little helps.",
                IsAnonymous = false,
                UserId = userId,
                CharityId = charityId,
                CreatedBy = userSeedId,
                CreatedDate = currentDate,
                IsDeleted = false
            }
        };

        return donations;
    }
}

