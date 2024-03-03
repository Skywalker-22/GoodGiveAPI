using GoodGive.DAL.Entities;
using GoodGive.DAL.Seeders.Interfaces;

namespace GoodGive.DAL.Seeders.Implementations;

public class DonationGoalSeeder : IDonationGoalSeeder
{
    public List<DonationGoal> GetDonationGoalSeedData(DateTime currentDate, Guid charityId)
    {
        var userSeedId = Guid.NewGuid();

        var donationGoals = new List<DonationGoal>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                GoalAmount = 5000m,
                Description = "Winter relief fund.",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(1),
                IsAchieved = false,
                CharityId = charityId,
                CreatedBy = userSeedId,
                CreatedDate = currentDate,
                IsDeleted = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                GoalAmount = 10000m,
                Description = "Back to school campaign.",
                StartDate = DateTime.UtcNow.AddMonths(-2),
                EndDate = DateTime.UtcNow.AddMonths(-1),
                IsAchieved = true,
                CharityId = charityId,
                CreatedBy = userSeedId,
                CreatedDate = currentDate,
                IsDeleted = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                GoalAmount = 7500m,
                Description = "Healthcare for all initiative.",
                StartDate = DateTime.UtcNow.AddMonths(-4),
                EndDate = DateTime.UtcNow.AddMonths(-3),
                IsAchieved = true,
                CharityId = charityId,
                CreatedBy = userSeedId,
                CreatedDate = currentDate,
                IsDeleted = false
            }
        };

        return donationGoals;
    }
}
