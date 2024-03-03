using GoodGive.BLL.DataTransferObjects;
using GoodGive.BLL.Utilities;

namespace GoodGive.BLL.Services.Interfaces;

public interface IDonationService
{
    #region Donations

    Task<OperationResult<List<DonationDto>>> GetDonationsAsync();
    Task<OperationResult<DonationDto>> GetDonationByIdAsync(Guid id);
    Task<OperationResult<List<DonationDto>>> GetDonationsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<OperationResult<List<DonationDto>>> GetDonationsByAmountRangeAsync(decimal minAmount, decimal maxAmount);
    Task<OperationResult<List<DonationDto>>> GetDonationsByDateAndAmountRangeAsync(DateTime startDate, DateTime endDate, decimal minAmount, decimal maxAmount);
    Task<OperationResult<DonationDto>> CreateDonationAsync(DonationDto donationDto);
    Task<OperationResult<DonationDto>> UpdateDonationAsync(Guid id, DonationDto donationDto);
    Task<OperationResult<DonationDto>> DeleteDonationAsync(Guid id);

    #endregion Donations

    #region User Donations

    Task<OperationResult<List<DonationDto>>> GetUserDonationsAsync(Guid userId);
    Task<OperationResult<DonationDto>> GetUserDonationByIdAsync(Guid userId, Guid donationId);
    Task<OperationResult<DonationDto>> CreateUserDonationAsync(Guid userId, DonationDto donationDto);
    Task<OperationResult<DonationDto>> UpdateUserDonationAsync(Guid userId, Guid donationId, DonationDto donationDto);
    Task<OperationResult<DonationDto>> DeleteUserDonationAsync(Guid userId, Guid donationId);

    #endregion User Donations

    #region Charity Donations

    Task<OperationResult<List<DonationDto>>> GetCharityDonationsAsync(Guid charityId);
    Task<OperationResult<DonationDto>> GetCharityDonationByIdAsync(Guid charityId, Guid donationId);
    Task<OperationResult<DonationDto>> CreateCharityDonationAsync(Guid charityId, DonationDto donationDto);
    Task<OperationResult<DonationDto>> UpdateCharityDonationAsync(Guid charityId, Guid donationId, DonationDto donationDto);
    Task<OperationResult<DonationDto>> DeleteCharityDonationAsync(Guid charityId, Guid donationId);

    #endregion Charity Donations
}
