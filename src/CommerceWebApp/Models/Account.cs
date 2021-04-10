using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Commerce_WebApp.Models
{
  public class Account
  {
    public int Id { get; set; }
    public string Account_Type { get; set; }
    public string Nickname { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Interest_rate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; }
  }
}
