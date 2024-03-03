using GoodGive.DAL.Entities;

namespace GoodGive.DAL.Seeders.Interfaces;

public interface IDonationGoalSeeder
{
    List<DonationGoal> GetDonationGoalSeedData(DateTime currentDate, Guid charityId);
}
