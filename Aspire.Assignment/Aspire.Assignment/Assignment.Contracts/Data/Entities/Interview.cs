using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Contracts.Data.Entities;

public class Interview
{
    [Key]
    public int InterviewId { get; set; }


    [ForeignKey("SlotId")]
    public int SlotId { get; set; }
    public Slot Slot { get; set; }
   
    public int CandidateId { get; set; }
    [ForeignKey("UserId")]
    public Users Users { get; set; }

    
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

}
