using GoodGive.BLL.DataTransferObjects;
using GoodGive.BLL.Utilities;

namespace GoodGive.BLL.Services.Interfaces;

public interface IUserManagementService
{
    Task<OperationResult<List<UserDto>>> GetUsersAsync();
    Task<OperationResult<UserDto>> GetUserByIdAsync(Guid id);
    Task<OperationResult<UserDto>> GetUserByEmailAsync(string email);
    Task<OperationResult<UserDto>> CreateUserAsync(UserDto userDto);
    Task<OperationResult<UserDto>> UpdateUserAsync(Guid id, UserDto userDto);
    Task<OperationResult<UserDto>> DeleteUserAsync(Guid id);
}
