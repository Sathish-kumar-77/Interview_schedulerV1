using System;

namespace Assignment.Contracts.DTO;

public class CreateUsersDTO
{
        public int UserId { get; set; }  
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Designation { get; set; }

        public string ReportingManager  { get; set; }
        public int RoleId { get; set; }
}
