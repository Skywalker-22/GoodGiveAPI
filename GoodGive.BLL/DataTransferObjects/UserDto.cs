namespace GoodGive.BLL.DataTransferObjects;

public class UserDto
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? ConfirmEmail { get; set; }
    public string? Mobile { get; set; }
}
