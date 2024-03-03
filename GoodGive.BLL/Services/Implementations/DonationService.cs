using GoodGive.BLL.DataTransferObjects;
using GoodGive.BLL.Services.Interfaces;
using GoodGive.BLL.Utilities;
using GoodGive.BLL.Utilities.Responses;
using GoodGive.DAL.Contexts;
using GoodGive.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GoodGive.BLL.Services.Implementations;

public class DonationService(AppDbContext dbContext, ILogger<DonationService> logger) : IDonationService
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<DonationService> _logger = logger;

    #region Donations

    public async Task<OperationResult<List<DonationDto>>> GetDonationsAsync()
    {
        try
        {
            var donations = await _dbContext.Donations.ToListAsync();

            var donationDtos = donations.Select(d => new DonationDto
            {
                Id = d.Id,
                Amount = d.Amount,
                Date = d.Date,
                Message = d.Message,
                IsAnonymous = d.IsAnonymous
            }).ToList();

            return new SuccessResult<List<DonationDto>>(donationDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all donations.");

            return new FailureResult<List<DonationDto>>(ex.Message);
        }
    }

    public async Task<OperationResult<DonationDto>> GetDonationByIdAsync(Guid id)
    {
        try
        {
            var donation = await _dbContext.Donations.FirstOrDefaultAsync(d => d.Id == id);

            if (donation == null)
            {
                return new FailureResult<DonationDto>("Donation not found.");
            }

            var donationDto = new DonationDto
            {
                Id = donation.Id,
                Amount = donation.Amount,
                Date = donation.Date,
                Message = donation.Message,
                IsAnonymous = donation.IsAnonymous
            };

            return new SuccessResult<DonationDto>(donationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving donation by id.");

            return new FailureResult<DonationDto>(ex.Message);
        }
    }

    public async Task<OperationResult<List<DonationDto>>> GetDonationsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            var donations = await _dbContext.Donations
                .Where(d => d.Date >= startDate && d.Date <= endDate)
                .ToListAsync();

            var donationDtos = donations.Select(d => new DonationDto
            {
                Id = d.Id,
                Amount = d.Amount,
                Date = d.Date,
                Message = d.Message,
                IsAnonymous = d.IsAnonymous
            }).ToList();

            return new SuccessResult<List<DonationDto>>(donationDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving donations by date range.");

            return new FailureResult<List<DonationDto>>(ex.Message);
        }
    }

    public async Task<OperationResult<List<DonationDto>>> GetDonationsByAmountRangeAsync(decimal minAmount, decimal maxAmount)
    {
        try
        {
            var donations = await _dbContext.Donations
                .Where(d => d.Amount >= minAmount && d.Amount <= maxAmount)
                .ToListAsync();

            var donationDtos = donations.Select(d => new DonationDto
            {
                Id = d.Id,
                Amount = d.Amount,
                Date = d.Date,
                Message = d.Message,
                IsAnonymous = d.IsAnonymous
            }).ToList();

            return new SuccessResult<List<DonationDto>>(donationDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving donations by amount range.");

            return new FailureResult<List<DonationDto>>(ex.Message);
        }
    }

    public async Task<OperationResult<List<DonationDto>>> GetDonationsByDateAndAmountRangeAsync(DateTime startDate, DateTime endDate, decimal minAmount, decimal maxAmount)
    {
        try
        {
            var donations = await _dbContext.Donations
                .Where(d => d.Date >= startDate && d.Date <= endDate && d.Amount >= minAmount && d.Amount <= maxAmount)
                .ToListAsync();

            var donationDtos = donations.Select(d => new DonationDto
            {
                Id = d.Id,
                Amount = d.Amount,
                Date = d.Date,
                Message = d.Message,
                IsAnonymous = d.IsAnonymous
            }).ToList();

            return new SuccessResult<List<DonationDto>>(donationDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving donations by date and amount range.");

            return new FailureResult<List<DonationDto>>(ex.Message);
        }
    }

    public async Task<OperationResult<DonationDto>> CreateDonationAsync(DonationDto donationDto)
    {
        try
        {
            var donation = new Donation
            {
                Id = Guid.NewGuid(),
                Amount = donationDto.Amount,
                Date = donationDto.Date,
                Message = donationDto.Message,
                IsAnonymous = donationDto.IsAnonymous
            };

            await _dbContext.Donations.AddAsync(donation);
            await _dbContext.SaveChangesAsync();

            return new SuccessResult<DonationDto>(donationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating donation.");

            return new FailureResult<DonationDto>(ex.Message);
        }
    }

    public async Task<OperationResult<DonationDto>> UpdateDonationAsync(Guid id, DonationDto donationDto)
    {
        try
        {
            var donation = await _dbContext.Donations.FirstOrDefaultAsync(d => d.Id == id);

            if (donation == null)
            {
                return new FailureResult<DonationDto>("Donation not found.");
            }

            donation.Amount = donationDto.Amount;
            donation.Date = donationDto.Date;
            donation.Message = donationDto.Message;
            donation.IsAnonymous = donationDto.IsAnonymous;

            _dbContext.Donations.Update(donation);
            await _dbContext.SaveChangesAsync();

            return new SuccessResult<DonationDto>(donationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating donation.");

            return new FailureResult<DonationDto>(ex.Message);
        }
    }

    public async Task<OperationResult<DonationDto>> DeleteDonationAsync(Guid id)
    {
        try
        {
            var donation = await _dbContext.Donations.FirstOrDefaultAsync(d => d.Id == id);

            if (donation == null)
            {
                return new FailureResult<DonationDto>("Donation not found.");
            }

            _dbContext.Donations.Remove(donation);
            await _dbContext.SaveChangesAsync();

            var donationDto = new DonationDto
            {
                Id = donation.Id,
                Amount = donation.Amount,
                Date = donation.Date,
                Message = donation.Message,
                IsAnonymous = donation.IsAnonymous
            };

            return new SuccessResult<DonationDto>(donationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting donation.");

            return new FailureResult<DonationDto>(ex.Message);
        }
    }

    #endregion Donations

    #region UserDonations

    public async Task<OperationResult<List<DonationDto>>> GetUserDonationsAsync(Guid userId)
    {
        try
        {
            var donations = await _dbContext.Donations.Where(d => d.UserId == userId).ToListAsync();

            var donationDtos = donations.Select(d => new DonationDto
            {
                Id = d.Id,
                Amount = d.Amount,
                Date = d.Date,
                Message = d.Message,
                IsAnonymous = d.IsAnonymous
            }).ToList();

            return new SuccessResult<List<DonationDto>>(donationDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user donations.");

            return new FailureResult<List<DonationDto>>(ex.Message);
        }
    }

    public async Task<OperationResult<DonationDto>> GetUserDonationByIdAsync(Guid userId, Guid donationId)
    {
        try
        {
            var donation = await _dbContext.Donations.FirstOrDefaultAsync(d => d.UserId == userId && d.Id == donationId);

            if (donation == null)
            {
                return new FailureResult<DonationDto>("Donation not found.");
            }

            var donationDto = new DonationDto
            {
                Id = donation.Id,
                Amount = donation.Amount,
                Date = donation.Date,
                Message = donation.Message,
                IsAnonymous = donation.IsAnonymous
            };

            return new SuccessResult<DonationDto>(donationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user donation by id.");

            return new FailureResult<DonationDto>(ex.Message);
        }
    }

    public async Task<OperationResult<DonationDto>> CreateUserDonationAsync(Guid userId, DonationDto donationDto)
    {
        try
        {
            var donation = new Donation
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CharityId = donationDto.CharityId,
                Amount = donationDto.Amount,
                Date = donationDto.Date,
                Message = donationDto.Message,
                IsAnonymous = donationDto.IsAnonymous
            };

            await _dbContext.Donations.AddAsync(donation);
            await _dbContext.SaveChangesAsync();

            return new SuccessResult<DonationDto>(donationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user donation.");

            return new FailureResult<DonationDto>(ex.Message);
        }
    }

    public async Task<OperationResult<DonationDto>> UpdateUserDonationAsync(Guid userId, Guid donationId, DonationDto donationDto)
    {
        try
        {
            var donation = await _dbContext.Donations.FirstOrDefaultAsync(d => d.UserId == userId && d.Id == donationId);

            if (donation == null)
            {
                return new FailureResult<DonationDto>("Donation not found.");
            }

            donation.Amount = donationDto.Amount;
            donation.Date = donationDto.Date;
            donation.Message = donationDto.Message;
            donation.IsAnonymous = donationDto.IsAnonymous;

            _dbContext.Donations.Update(donation);
            await _dbContext.SaveChangesAsync();

            return new SuccessResult<DonationDto>(donationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user donation.");

            return new FailureResult<DonationDto>(ex.Message);
        }
    }

    public async Task<OperationResult<DonationDto>> DeleteUserDonationAsync(Guid userId, Guid donationId)
    {
        try
        {
            var donation = await _dbContext.Donations.FirstOrDefaultAsync(d => d.UserId == userId && d.Id == donationId);

            if (donation == null)
            {
                return new FailureResult<DonationDto>("Donation not found.");
            }

            _dbContext.Donations.Remove(donation);
            await _dbContext.SaveChangesAsync();

            var donationDto = new DonationDto
            {
                Id = donation.Id,
                Amount = donation.Amount,
                Date = donation.Date,
                Message = donation.Message,
                IsAnonymous = donation.IsAnonymous
            };

            return new SuccessResult<DonationDto>(donationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user donation.");

            return new FailureResult<DonationDto>(ex.Message);
        }
    }

    #endregion UserDonations

    #region CharityDonations

    public async Task<OperationResult<List<DonationDto>>> GetCharityDonationsAsync(Guid charityId)
    {
        try
        {
            var donations = await _dbContext.Donations.Where(d => d.CharityId == charityId).ToListAsync();

            var donationDtos = donations.Select(d => new DonationDto
            {
                Id = d.Id,
                Amount = d.Amount,
                Date = d.Date,
                Message = d.Message,
                IsAnonymous = d.IsAnonymous
            }).ToList();

            return new SuccessResult<List<DonationDto>>(donationDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving charity donations.");

            return new FailureResult<List<DonationDto>>(ex.Message);
        }
    }

    public async Task<OperationResult<DonationDto>> GetCharityDonationByIdAsync(Guid charityId, Guid donationId)
    {
        try
        {
            var donation = await _dbContext.Donations.FirstOrDefaultAsync(d => d.CharityId == charityId && d.Id == donationId);

            if (donation == null)
            {
                return new FailureResult<DonationDto>("Donation not found.");
            }

            var donationDto = new DonationDto
            {
                Id = donation.Id,
                Amount = donation.Amount,
                Date = donation.Date,
                Message = donation.Message,
                IsAnonymous = donation.IsAnonymous
            };

            return new SuccessResult<DonationDto>(donationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving charity donation by id.");

            return new FailureResult<DonationDto>(ex.Message);
        }
    }

    public async Task<OperationResult<DonationDto>> CreateCharityDonationAsync(Guid charityId, DonationDto donationDto)
    {
        try
        {
            var donation = new Donation
            {
                Id = Guid.NewGuid(),
                CharityId = charityId,
                Amount = donationDto.Amount,
                Date = donationDto.Date,
                Message = donationDto.Message,
                IsAnonymous = donationDto.IsAnonymous
            };

            await _dbContext.Donations.AddAsync(donation);
            await _dbContext.SaveChangesAsync();

            return new SuccessResult<DonationDto>(donationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating charity donation.");

            return new FailureResult<DonationDto>(ex.Message);
        }
    }

    public async Task<OperationResult<DonationDto>> UpdateCharityDonationAsync(Guid charityId, Guid donationId, DonationDto donationDto)
    {
        try
        {
            var donation = await _dbContext.Donations.FirstOrDefaultAsync(d => d.CharityId == charityId && d.Id == donationId);

            if (donation == null)
            {
                return new FailureResult<DonationDto>("Donation not found.");
            }

            donation.Amount = donationDto.Amount;
            donation.Date = donationDto.Date;
            donation.Message = donationDto.Message;
            donation.IsAnonymous = donationDto.IsAnonymous;

            _dbContext.Donations.Update(donation);
            await _dbContext.SaveChangesAsync();

            return new SuccessResult<DonationDto>(donationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating charity donation.");

            return new FailureResult<DonationDto>(ex.Message);
        }
    }

    public async Task<OperationResult<DonationDto>> DeleteCharityDonationAsync(Guid charityId, Guid donationId)
    {
        try
        {
            var donation = await _dbContext.Donations.FirstOrDefaultAsync(d => d.CharityId == charityId && d.Id == donationId);

            if (donation == null)
            {
                return new FailureResult<DonationDto>("Donation not found.");
            }

            _dbContext.Donations.Remove(donation);
            await _dbContext.SaveChangesAsync();

            var donationDto = new DonationDto
            {
                Id = donation.Id,
                Amount = donation.Amount,
                Date = donation.Date,
                Message = donation.Message,
                IsAnonymous = donation.IsAnonymous
            };

            return new SuccessResult<DonationDto>(donationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting charity donation.");

            return new FailureResult<DonationDto>(ex.Message);
        }
    }

    #endregion CharityDonations
}
