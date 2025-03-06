using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Contracts.Data.Entities;

public class Role
{
    [Key]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? RoleName { get; set; }

}
