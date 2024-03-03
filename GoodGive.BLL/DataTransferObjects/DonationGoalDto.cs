namespace GoodGive.BLL.DataTransferObjects;

public class DonationGoalDto
{
    public Guid Id { get; set; }
    public decimal GoalAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsAchieved { get; set; }
}
