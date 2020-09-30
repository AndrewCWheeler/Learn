using System.ComponentModel.DataAnnotations;
using System;
namespace Learn.Models
{
  public class FlashCard
  {
    [Key]
    public int FlashCardId { get; set; }

    public int VarOne { get; set; }

    public int VarTwo { get; set; }

    public int VarThree { get; set; }

    public int Answer { get; set; }

    public string Category { get; set; }

    public string Level { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;



  }
}