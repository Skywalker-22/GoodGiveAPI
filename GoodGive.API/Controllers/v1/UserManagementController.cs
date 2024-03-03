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
public class UserManagementController(IUserManagementService userManagementService, ILogger<UserManagementController> logger) : ApiController
{
    private readonly IUserManagementService _userManagementService = userManagementService;
    private readonly ILogger<UserManagementController> _logger = logger;

    [HttpGet("GetAllUsers")]
    [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var result = await _userManagementService.GetUsersAsync();

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

    [HttpGet("GetUserById/{id}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        try
        {
            var result = await _userManagementService.GetUserByIdAsync(id);

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

    [HttpGet("GetUserByEmail/{email}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        try
        {
            var result = await _userManagementService.GetUserByEmailAsync(email);

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

    [HttpPost("CreateUser")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUser(UserDto userDto)
    {
        try
        {
            var result = await _userManagementService.CreateUserAsync(userDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            if (result.Data == null)
            {
                _logger.LogError("CreateUser: result.Data is null.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }

            return CreatedAtAction(nameof(GetUserById), new { id = result.Data.Id }, result.Data);
        }
        catch (Exception ex)
        {
            return ExceptionResultHandler<object>.ExceptionResult(_logger, ex).HandleExceptionResult(GetUserId());
        }
    }

    [HttpPut("UpdateUser/{id}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUser(Guid id, UserDto userDto)
    {
        try
        {
            var result = await _userManagementService.UpdateUserAsync(id, userDto);

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

    [HttpDelete("DeleteUser/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            var result = await _userManagementService.DeleteUserAsync(id);

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
}
