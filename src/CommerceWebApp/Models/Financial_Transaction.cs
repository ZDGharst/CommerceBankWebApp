using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using Commerce_WebApp.Data;

namespace Commerce_WebApp.Models
{
  public class Financial_Transaction
  {
    public int Id { get; set; }
    public int Account_Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance_After { get; set; }
  }
}