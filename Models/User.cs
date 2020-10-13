using System.ComponentModel.DataAnnotations;
using System;
namespace Learn.Models
{
  public class User
  {
    [Key]
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Confirm { get; set; }
    public int Points { get; set; }
    public int Percentage { get; set; }
    public string Grade { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
  }
}