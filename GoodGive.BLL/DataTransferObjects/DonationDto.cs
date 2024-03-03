namespace GoodGive.BLL.DataTransferObjects;

public class DonationDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CharityId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Message { get; set; }
    public bool IsAnonymous { get; set; }
}
