using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Commerce_WebApp.Models
{
  public class Notification_Rule
  {
    public int Id { get; set; }
    public string Customer_Id { get; set; }
    public string Type { get; set; }
    public string Condition { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Value { get; set; }
  }
}
