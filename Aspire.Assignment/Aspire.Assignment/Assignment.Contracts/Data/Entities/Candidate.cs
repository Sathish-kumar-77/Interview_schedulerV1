using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Contracts.Data.Entities;

public class Candidate
{
    [Key]
        public int CandidateId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [ForeignKey("ReportingManager")]
        public int ReportingManagerId { get; set; } // Assigned Reporting Manager
        public ReportingManager ReportingManager { get; set; }

        public ICollection<Slot> Slots { get; set; } // Candidate's booked slots

}
