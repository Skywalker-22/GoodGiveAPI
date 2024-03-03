using GoodGive.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace GoodGive.DAL.Entities;

public class User : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(255)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Compare("Email", ErrorMessage = "The email addresses do not match.")]
    public string ConfirmEmail { get; set; } = string.Empty;

    [Phone]
    [StringLength(14)]
    public string Mobile { get; set; } = string.Empty;

    #region Navigation Properties & Foreign Keys

    public virtual ICollection<Donation> Donations { get; set; } = [];

    #endregion Navigation Properties & Foreign Keys
}
