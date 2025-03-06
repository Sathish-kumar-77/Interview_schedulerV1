using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Contracts.Data.Entities;

public class PanelCoordinator
{

    [Key]
        public int PanelCoordinatorId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }  // PanelCoordinator is a user
        public User User { get; set; }

        [Required]
        [ForeignKey("PanelMember")]
        public int PanelMemberId { get; set; } // Assigning to a specific Panel Member
        public PanelMember PanelMember { get; set; }

        [Required]
        public DateTime AllocationStartDate { get; set; } // Example: December 1

        [Required]
        public DateTime AllocationEndDate { get; set; }   // Example: December 31

    }

