using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Contracts.Data.Entities;

public class Slot
{
    [Key]
        public int SlotId { get; set; }

        [Required]
        [ForeignKey("PanelMember")]
        public int PanelMemberId { get; set; }
        public PanelMember PanelMember { get; set; }

        [Required]
        public DateTime AvailableStartTime { get; set; }

        [Required]
        public DateTime AvailableEndTime { get; set; }

        public bool IsBooked { get; set; } = false; // True when TA Recruiter books it

        [ForeignKey("TARecruiter")]
        public int? BookedBy { get; set; } // Nullable, filled when booked
        public TARecruiter TARecruiter { get; set; }

        [ForeignKey("Candidate")]
        public int? CandidateId { get; set; } // Candidate assigned to the slot
        public Candidate Candidate { get; set; }

        [ForeignKey("ReportingManager")]
        public int? ReportingManagerId { get; set; } // Related Manager
        public ReportingManager ReportingManager { get; set; }

        public bool? IsManagerAttending { get; set; } // Null = No response, True = Attending, False = Not Attending

}
