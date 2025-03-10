using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using Assignment.Core.Handlers.Commands;
using Assignment.Providers.Handlers.Commands;
using Assignment.Providers.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public UsersController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Post([FromBody] CreateUsersDTO model)
        {
            try
            {
                var command = new CreateUsersCommand(model);
                var response = await _mediator.Send(command);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authentication:Jwt:Secret"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("userId", model.UserId.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                
                return Ok(new
                {
                    user = model,
                    token = tokenHandler.WriteToken(token)
                });
                
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _mediator.Send(new DeleteUserCommand(userId));

            if (!result)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(new { message = "User deleted successfully." });
        }
    
    [HttpPut("{userId}")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUsersDTO model)
        {
            var result = await _mediator.Send(new UpdateUserCommand(userId, model));

            if (!result)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(new { message = "User updated successfully." });
        }


        [HttpGet]
         [Authorize(Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> GetUsers(CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
            if (users == null)
                return NotFound("No users found.");

            return Ok(users);
        }
    

    }
}
    
