using System.ComponentModel.DataAnnotations;

namespace Assignment.Contracts.DTO
{
    public class UpdateUsersDTO
    {
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? ReportingManager { get; set; }
        public string? Designation { get; set; }

        public int? RoleId { get; set; }  // Nullable, so it can be skipped

        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string? Password { get; set; } // Only update if provided
    }
}
