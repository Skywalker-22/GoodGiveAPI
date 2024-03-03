using GoodGive.BLL.DataTransferObjects;
using GoodGive.BLL.Services.Interfaces;
using GoodGive.BLL.Utilities;
using GoodGive.BLL.Utilities.Responses;
using GoodGive.DAL.Contexts;
using GoodGive.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GoodGive.BLL.Services.Implementations;

public class UserManagementService(AppDbContext dbContext, ILogger<UserManagementService> logger) : IUserManagementService
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<UserManagementService> _logger = logger;

    public async Task<OperationResult<List<UserDto>>> GetUsersAsync()
    {
        try
        {
            var users = await _dbContext.Users.ToListAsync();

            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                ConfirmEmail = u.ConfirmEmail,
                Mobile = u.Mobile
            }).ToList();

            return new SuccessResult<List<UserDto>>(userDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all users.");

            return new FailureResult<List<UserDto>>(ex.Message);
        }
    }

    public async Task<OperationResult<UserDto>> GetUserByIdAsync(Guid id)
    {
        try
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return new FailureResult<UserDto>("User not found.");
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ConfirmEmail = user.ConfirmEmail,
                Mobile = user.Mobile
            };

            return new SuccessResult<UserDto>(userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user by id.");

            return new FailureResult<UserDto>(ex.Message);
        }
    }

    public async Task<OperationResult<UserDto>> GetUserByEmailAsync(string email)
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return new FailureResult<UserDto>("User not found.");
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ConfirmEmail = user.ConfirmEmail,
                Mobile = user.Mobile
            };

            return new SuccessResult<UserDto>(userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user by email.");

            return new FailureResult<UserDto>(ex.Message);
        }
    }

    public async Task<OperationResult<UserDto>> CreateUserAsync(UserDto userDto)
    {
        try
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = userDto.FirstName ?? string.Empty,
                LastName = userDto.LastName ?? string.Empty,
                Email = userDto.Email ?? string.Empty,
                ConfirmEmail = userDto.ConfirmEmail ?? string.Empty,
                Mobile = userDto.Mobile ?? string.Empty
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return new SuccessResult<UserDto>(userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user.");

            return new FailureResult<UserDto>(ex.Message);
        }
    }

    public async Task<OperationResult<UserDto>> UpdateUserAsync(Guid id, UserDto userDto)
    {
        try
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return new FailureResult<UserDto>("User not found.");
            }

            user.FirstName = userDto.FirstName ?? string.Empty;
            user.LastName = userDto.LastName ?? string.Empty;
            user.Email = userDto.Email ?? string.Empty;
            user.ConfirmEmail = userDto.ConfirmEmail ?? string.Empty;
            user.Mobile = userDto.Mobile ?? string.Empty;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return new SuccessResult<UserDto>(userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user.");

            return new FailureResult<UserDto>(ex.Message);
        }
    }

    public async Task<OperationResult<UserDto>> DeleteUserAsync(Guid id)
    {
        try
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return new FailureResult<UserDto>("User not found.");
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ConfirmEmail = user.ConfirmEmail,
                Mobile = user.Mobile
            };

            return new SuccessResult<UserDto>(userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user.");

            return new FailureResult<UserDto>(ex.Message);
        }
    }
}
