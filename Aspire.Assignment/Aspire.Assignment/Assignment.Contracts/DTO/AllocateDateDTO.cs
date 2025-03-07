using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Contracts.DTO;

public class AllocateDateDTO
{
    [Key]
    public int Id { get; set; } // Primary Key
    public int PanelMemberID { get; set; }

    public DateTime StartDate {get; set;}
   
    public DateTime EndDate {get; set;}
}
