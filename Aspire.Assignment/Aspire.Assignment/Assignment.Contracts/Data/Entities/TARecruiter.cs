using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Contracts.Data.Entities;

public class TARecruiter
{

        [Key]
        public int TARecruiterId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

}
