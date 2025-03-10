using System;

namespace Assignment.Contracts.DTO;

public class LoginResponseDTO
{
     public string Token { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }

}
