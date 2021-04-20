using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Commerce_WebApp.Models
{
  public class Customer_Info
  {
    public string Id { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public DateTime Date_Of_Birth { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int Zip { get; set; }
  }
}
