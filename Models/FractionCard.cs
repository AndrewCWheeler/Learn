using System.ComponentModel.DataAnnotations;
using System;
namespace Learn.Models
{
  public class FractionCard
  {
    [Key]
    public int FractionCardId { get; set; }
    public int Dividend { get; set; }
    public int Divisor { get; set; }
    public int? Whole { get; set; }
    public string Answer { get; set; }
    public int Modulus { get; set; }
    public double Decimal { get; set; }
    public string Category { get; set; } = "Fractions Basic";
    public string Level { get; set; } = "Practice";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
  }
}

