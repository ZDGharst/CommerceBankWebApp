using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using Commerce.Data;

namespace Commerce.Models
{
  public class Financial_Transaction
  {
    public int Id { get; set; }
    public int Account_Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance_After { get; set; }
    public string Description { get; set; }
  }
}