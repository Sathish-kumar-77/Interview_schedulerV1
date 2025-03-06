using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Contracts.Data.Entities;

public class ReportingManager
{

       [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }  // Link to the User table
        public User User { get; set; }  

        public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>(); // Assigned candidates
        public ICollection<Slot> Slots { get; set; } = new List<Slot>(); // Interviews where the manager can confirm attendance 

       
    }

