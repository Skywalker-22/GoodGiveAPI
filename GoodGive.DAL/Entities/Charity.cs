using GoodGive.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace GoodGive.DAL.Entities;

public class Charity : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Phone]
    [StringLength(14)]
    public string Phone { get; set; } = string.Empty;

    [StringLength(255)]
    public string Description { get; set; } = string.Empty;

    [StringLength(255)]
    public string Category { get; set; } = string.Empty;

    [StringLength(255)]
    public string Website { get; set; } = string.Empty;

    #region Navigation Properties & Foreign Keys

    public virtual ICollection<Donation> Donations { get; set; } = [];
    public virtual ICollection<DonationGoal> DonationGoals { get; set; } = [];

    #endregion Navigation Properties & Foreign Keys
}
