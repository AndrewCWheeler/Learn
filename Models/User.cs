using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Learn.Models
{
  public class User
  {
    [Key]
    public int UserId { get; set; }
    [Required(ErrorMessage = "Username is required.")]
    [MinLength(2, ErrorMessage = "First name must be at least 2 characters.")]
    [MaxLength(20, ErrorMessage = "Please keep username to 20 characters or less.")]
    [Display(Name = "Username: ")]
    public string Username { get; set; }

    [Required(ErrorMessage = "A valid email address is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [Display(Name = "Email: ")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$", ErrorMessage = "Password must be 8 characters long and contain at least 1 letter, 1 number and a special character(@$!%*#?&)")]
    [Display(Name = "Password: ")]
    [DataType(DataType.Password)]
    [Compare("Confirm", ErrorMessage = "Passwords do not match.")]
    public string Password { get; set; }

    [NotMapped]
    [Display(Name = "Confirm: ")]
    [DataType(DataType.Password)]
    public string Confirm { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
  }
}