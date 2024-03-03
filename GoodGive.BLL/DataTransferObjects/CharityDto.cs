namespace GoodGive.BLL.DataTransferObjects;

public class CharityDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Category { get; set; }
    public string? Website { get; set; }
}
