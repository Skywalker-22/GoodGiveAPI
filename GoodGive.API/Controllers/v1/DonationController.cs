using GoodGive.API.Helpers;
using GoodGive.API.Helpers.ExceptionHelper;
using GoodGive.BLL.DataTransferObjects;
using GoodGive.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoodGive.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class DonationController(IDonationService donationService, ILogger<DonationController> logger) : ApiController
{
    private readonly IDonationService _donationService = donationService;
    private readonly ILogger<DonationController> _logger = logger;

    #region Donations

    [HttpGet("GetAllDonations")]
    [ProducesResponseType(typeof(List<DonationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllDonations()
    {
        try
        {
            var result = await _donationService.GetDonationsAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpGet("GetDonationById/{id}")]
    [ProducesResponseType(typeof(DonationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDonationById(Guid id)
    {
        try
        {
            var result = await _donationService.GetDonationByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpGet("GetDonationsByDateRange/{startDate}/{endDate}")]
    [ProducesResponseType(typeof(List<DonationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDonationsByDateRange(DateTime startDate, DateTime endDate)
    {
        try
        {
            var result = await _donationService.GetDonationsByDateRangeAsync(startDate, endDate);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpGet("GetDonationsByAmountRange/{minAmount}/{maxAmount}")]
    [ProducesResponseType(typeof(List<DonationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDonationsByAmountRange(decimal minAmount, decimal maxAmount)
    {
        try
        {
            var result = await _donationService.GetDonationsByAmountRangeAsync(minAmount, maxAmount);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpGet("GetDonationsByDateAndAmountRange/{startDate}/{endDate}/{minAmount}/{maxAmount}")]
    [ProducesResponseType(typeof(List<DonationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDonationsByDateAndAmountRange(DateTime startDate, DateTime endDate, decimal minAmount, decimal maxAmount)
    {
        try
        {
            var result = await _donationService.GetDonationsByDateAndAmountRangeAsync(startDate, endDate, minAmount, maxAmount);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpPost("CreateDonation")]
    [ProducesResponseType(typeof(DonationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDonation(DonationDto donationDto)
    {
        try
        {
            var result = await _donationService.CreateDonationAsync(donationDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            if (result.Data == null)
            {
                _logger.LogError("CreateDonation: result.Data is null.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }

            return CreatedAtAction(nameof(GetDonationById), new { id = result.Data.Id }, result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpPut("UpdateDonation/{id}")]
    [ProducesResponseType(typeof(DonationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateDonation(Guid id, DonationDto donationDto)
    {
        try
        {
            var result = await _donationService.UpdateDonationAsync(id, donationDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpDelete("DeleteDonation/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDonation(Guid id)
    {
        try
        {
            var result = await _donationService.DeleteDonationAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    #endregion Donations

    #region User Donations

    [HttpGet("GetUserDonations/{userId}")]
    [ProducesResponseType(typeof(List<DonationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserDonations(Guid userId)
    {
        try
        {
            var result = await _donationService.GetUserDonationsAsync(userId);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpGet("GetUserDonationById/{userId}/{donationId}")]
    [ProducesResponseType(typeof(DonationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserDonationById(Guid userId, Guid donationId)
    {
        try
        {
            var result = await _donationService.GetUserDonationByIdAsync(userId, donationId);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpPost("CreateUserDonation/{userId}")]
    [ProducesResponseType(typeof(DonationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUserDonation(Guid userId, DonationDto donationDto)
    {
        try
        {
            var result = await _donationService.CreateUserDonationAsync(userId, donationDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            if (result.Data == null)
            {
                _logger.LogError("CreateUserDonation: result.Data is null.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }

            return CreatedAtAction(nameof(GetUserDonationById), new { userId, donationId = result.Data.Id }, result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpPut("UpdateUserDonation/{userId}/{donationId}")]
    [ProducesResponseType(typeof(DonationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUserDonation(Guid userId, Guid donationId, DonationDto donationDto)
    {
        try
        {
            var result = await _donationService.UpdateUserDonationAsync(userId, donationId, donationDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpDelete("DeleteUserDonation/{userId}/{donationId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUserDonation(Guid userId, Guid donationId)
    {
        try
        {
            var result = await _donationService.DeleteUserDonationAsync(userId, donationId);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    #endregion User Donations

    #region Charity Donations

    [HttpGet("GetCharityDonations/{charityId}")]
    [ProducesResponseType(typeof(List<DonationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCharityDonations(Guid charityId)
    {
        try
        {
            var result = await _donationService.GetCharityDonationsAsync(charityId);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpGet("GetCharityDonationById/{charityId}/{donationId}")]
    [ProducesResponseType(typeof(DonationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCharityDonationById(Guid charityId, Guid donationId)
    {
        try
        {
            var result = await _donationService.GetCharityDonationByIdAsync(charityId, donationId);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpPost("CreateCharityDonation/{charityId}")]
    [ProducesResponseType(typeof(DonationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCharityDonation(Guid charityId, DonationDto donationDto)
    {
        try
        {
            var result = await _donationService.CreateCharityDonationAsync(charityId, donationDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            if (result.Data == null)
            {
                _logger.LogError("CreateCharityDonation: result.Data is null.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }

            return CreatedAtAction(nameof(GetCharityDonationById), new { charityId, donationId = result.Data.Id }, result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpPut("UpdateCharityDonation/{charityId}/{donationId}")]
    [ProducesResponseType(typeof(DonationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCharityDonation(Guid charityId, Guid donationId, DonationDto donationDto)
    {
        try
        {
            var result = await _donationService.UpdateCharityDonationAsync(charityId, donationId, donationDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpDelete("DeleteCharityDonation/{charityId}/{donationId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCharityDonation(Guid charityId, Guid donationId)
    {
        try
        {
            var result = await _donationService.DeleteCharityDonationAsync(charityId, donationId);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    #endregion Charity Donations
}
