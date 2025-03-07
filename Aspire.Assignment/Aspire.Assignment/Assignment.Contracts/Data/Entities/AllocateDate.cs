using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Contracts.Data.Entities;

public class AllocateDate
{
    [Key]
    public int Id { get; set; } // Primary Key
    public int PanelMemberID { get; set; }
    [ForeignKey("UserId")]

    
    public DateTime StartDate {get; set;}
   
    public DateTime EndDate {get; set;}
}
