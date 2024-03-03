using GoodGive.DAL.Entities;

namespace GoodGive.DAL.Seeders.Interfaces;

public interface IDonationSeeder
{
    List<Donation> GetDonationSeedData(DateTime currentDate, Guid charityId, Guid userId);
}
