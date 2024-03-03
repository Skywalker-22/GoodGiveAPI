using GoodGive.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodGive.DAL.Entities;

public class Donation : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [StringLength(255)]
    public string Message { get; set; } = string.Empty;

    [Required]
    public bool IsAnonymous { get; set; }

    #region Navigation Properties & Foreign Keys

    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }

    [ForeignKey("Charity")]
    public Guid CharityId { get; set; }
    public virtual Charity? Charity { get; set; }

    #endregion Navigation Properties & Foreign Keys
}
