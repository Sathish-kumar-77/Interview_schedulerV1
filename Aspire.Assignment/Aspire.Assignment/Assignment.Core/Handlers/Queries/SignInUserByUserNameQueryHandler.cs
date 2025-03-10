using MediatR;
using Assignment.Contracts.DTO;
using Assignment.Contracts.Data;
using Assignment.Core.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Assignment.Contracts.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Assignment.Providers.Handlers.Queries
{
    public class SignInUserByUserNameQuery : IRequest<string>
    {
        public string UserName { get; }
        public string PassWord { get; }
        public SignInUserByUserNameQuery(string userName, string password)
        {
            UserName = userName;
            PassWord = password;
        }
    }

    public class SignInUserByUserNameQueryHandler : IRequestHandler<SignInUserByUserNameQuery, string>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly IConfiguration _configuration;////Ioption to be changes 
   


        public SignInUserByUserNameQueryHandler(IUnitOfWork repository, IMapper mapper, IPasswordHasher<Users> passwordHasher, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            
        }

       public async Task<string> Handle(SignInUserByUserNameQuery request, CancellationToken cancellationToken)
{
    var user = _repository.Users.GetAll().FirstOrDefault(con => con.Name == request.UserName);
    if (user == null)
    {
        throw new EntityNotFoundException($"No User found for {request.UserName}");
    }

    if (user.RoleId == null) 
    {
        throw new EntityNotFoundException($"User {request.UserName} does not have a role assigned.");
    }

    var role = _repository.Roles.GetAll().FirstOrDefault(r => r.RoleId == user.RoleId);
    if (role == null)
    {
        throw new EntityNotFoundException($"Role not found for RoleId {user.RoleId}");
    }

    PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.Password, request.PassWord);
    if (result != PasswordVerificationResult.Success)
    {
        throw new InvalidcredentialsException("Invalid credentials");
    }

    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authentication:Jwt:Secret"));
    
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, request.UserName),
            new Claim(ClaimTypes.Role, role.RoleName)  
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}
    }
}