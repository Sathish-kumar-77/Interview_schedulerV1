using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Contracts.Data.Entities;

public class Users
{

[Key]
        public int UserId { get; set; }

        
        public string ?Name { get; set; }

        public string ?Email { get; set; }

        public string ?Password { get; set; }

        public string Designation { get; set; }

        public string ReportingManager  { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role ?Role { get; set; }

        public ICollection<Slot> Slots { get; set; }
        public ICollection<Interview> Interviews { get; set; }
}
