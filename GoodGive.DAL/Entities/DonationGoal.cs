using GoodGive.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodGive.DAL.Entities;

public class DonationGoal : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public decimal GoalAmount { get; set; }

    [StringLength(255)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public bool IsAchieved { get; set; }

    #region Navigation Properties & Foreign Keys

    [ForeignKey("Charity")]
    public Guid CharityId { get; set; }
    public virtual Charity? Charity { get; set; }

    #endregion Navigation Properties & Foreign Keys
}
