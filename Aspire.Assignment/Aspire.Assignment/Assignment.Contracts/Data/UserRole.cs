using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assignment.Contracts.Data.Entities;

namespace Assignment.Contracts.Data;

public class UserRole
{
         [Key]
        public int UserRoleId { get; set; }
        // Declaring a property named "UserRoleId" of type Guid, which represents the primary key for the UserRole entity. The [Key] attribute specifies that this property is the primary key.
        
        public int UserId { get; set; } // Foreign Key
        // Declaring a property named "UserId" of type Guid, which represents the foreign key referencing the Users entity.

        [ForeignKey("UserId")]
        public Users Users { get; set; }
        // Declaring a navigation property named "Users" of type Users, which represents the reference to the Users entity. The [ForeignKey] attribute specifies that this property is a foreign key referencing the Users entity.

        public Guid RoleId { get; set; } // Foreign Key
        // Declaring a property named "RoleId" of type Guid, which represents the foreign key referencing the Roles entity.

        [ForeignKey("RoleId")]
        public Role Roles { get; set; }
        // Declaring a navigation property named "Roles" of type Roles, which represents the reference to the Roles entity. The [ForeignKey] attribute specifies that this property is a foreign key referencing the Roles entity.

        public DateTime LastChanges { get; set; }
        // Declaring a property named "LastChanges" of type DateTime, which represents the timestamp of the last changes made to the UserRole entity.

        public string LastChangedBy { get; set; }
        // Declaring a property named "LastChangedBy" of type string, which represents the user who last changed the UserRole entity.
    }

