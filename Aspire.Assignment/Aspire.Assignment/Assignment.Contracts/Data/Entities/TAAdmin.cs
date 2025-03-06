using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Contracts.Data.Entities;

public class TAAdmin
{
    [Key]
        public int TAAdminId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

}
