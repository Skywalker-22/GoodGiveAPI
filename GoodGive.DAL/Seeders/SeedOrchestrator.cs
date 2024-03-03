using GoodGive.DAL.Contexts;
using GoodGive.DAL.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GoodGive.DAL.Seeders;

public class SeedOrchestrator(
    AppDbContext dbContext,
    ILogger<SeedOrchestrator> logger,
    IUserSeeder userSeeder,
    ICharitySeeder charitySeeder,
    IDonationSeeder donationSeeder,
    IDonationGoalSeeder donationGoalSeeder)
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<SeedOrchestrator> _logger = logger;
    private readonly IUserSeeder _userSeeder = userSeeder;
    private readonly ICharitySeeder _charitySeeder = charitySeeder;
    private readonly IDonationSeeder _donationSeeder = donationSeeder;
    private readonly IDonationGoalSeeder _donationGoalSeeder = donationGoalSeeder;

    public async Task SeedAllAsync()
    {
        try
        {
            await SeedUsersAsync();
            await SeedCharitiesAsync();
            await SeedDonationsAsync();
            await SeedDonationGoalsAsync();
            _logger.LogInformation("All seeding operations completed.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during the seeding process: {ErrorMessage}", ex.Message);
        }
    }

    private async Task SeedUsersAsync()
    {
        if (_dbContext.Users == null)
        {
            throw new InvalidOperationException("Database set 'Users' is null.");
        }

        var currentDate = DateTime.Now;
        var seedUsers = _userSeeder.GetUserSeedData(currentDate);
        var existingUserEmails = await _dbContext.Users.Select(u => u.Email).ToListAsync();

        var usersToAdd = seedUsers.Where(user => !existingUserEmails.Contains(user.Email)).ToList();

        if (usersToAdd.Count != 0)
        {
            _dbContext.Users.AddRange(usersToAdd);
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedCharitiesAsync()
    {
        if (_dbContext.Charities == null)
        {
            throw new InvalidOperationException("Database set 'Charities' is null.");
        }

        var currentDate = DateTime.Now;
        var seedCharities = _charitySeeder.GetCharitySeedData(currentDate);
        var existingCharityEmails = await _dbContext.Charities.Select(c => c.Email).ToListAsync();

        var charitiesToAdd = seedCharities.Where(charity => !existingCharityEmails.Contains(charity.Email)).ToList();

        if (charitiesToAdd.Count != 0)
        {
            _dbContext.Charities.AddRange(charitiesToAdd);
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedDonationsAsync()
    {
        if (_dbContext.Donations == null)
        {
            throw new InvalidOperationException("Database set 'Donations' is null.");
        }

        var currentDate = DateTime.Now;
        var charityIds = await _dbContext.Charities.Select(c => c.Id).ToListAsync();
        var userIds = await _dbContext.Users.Select(u => u.Id).ToListAsync();

        if (charityIds.Count == 0 || userIds.Count == 0)
        {
            _logger.LogInformation("No charities or users found for donation seeding.");
            return;
        }

        foreach (var charityId in charityIds)
        {
            var random = new Random();
            var userId = userIds[random.Next(userIds.Count)];

            var seedDonations = _donationSeeder.GetDonationSeedData(currentDate, charityId, userId);

            var existingDonations = await _dbContext.Donations
                .Where(d => d.CharityId == charityId && d.Date >= currentDate.AddDays(-30))
                .ToListAsync();

            var donationsToAdd = seedDonations
                .Where(newDonation => !existingDonations
                .Any(existingDonation => existingDonation.Date == newDonation.Date && existingDonation.Amount == newDonation.Amount))
                .ToList();

            if (donationsToAdd.Count != 0)
            {
                _dbContext.Donations.AddRange(donationsToAdd);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

    private async Task SeedDonationGoalsAsync()
    {
        if (_dbContext.DonationGoals == null)
        {
            throw new InvalidOperationException("Database set 'DonationGoals' is null.");
        }

        var currentDate = DateTime.Now;
        var charityIds = await _dbContext.Charities.Select(c => c.Id).ToListAsync();

        if (charityIds.Count == 0)
        {
            _logger.LogInformation("No charities found for donation goal seeding.");
            return;
        }

        foreach (var charityId in charityIds)
        {
            var seedDonationGoals = _donationGoalSeeder.GetDonationGoalSeedData(currentDate, charityId);

            var existingDonationGoals = await _dbContext.DonationGoals
                .Where(dg => dg.CharityId == charityId && dg.StartDate >= currentDate.AddMonths(-3))
                .ToListAsync();

            var donationGoalsToAdd = seedDonationGoals
                .Where(newDonationGoal => !existingDonationGoals
                .Any(existingDonationGoal => existingDonationGoal.StartDate == newDonationGoal.StartDate && existingDonationGoal.GoalAmount == newDonationGoal.GoalAmount))
                .ToList();

            if (donationGoalsToAdd.Count != 0)
            {
                _dbContext.DonationGoals.AddRange(donationGoalsToAdd);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
