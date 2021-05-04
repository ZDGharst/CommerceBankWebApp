using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Commerce_WebApp.Models
{
  public class Notification_Triggered
  {
    public int Id { get; set; }
    public string Type { get; set; }
    public string Condition { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Value { get; set; }

    public int Count { get; set; }
  }
}
