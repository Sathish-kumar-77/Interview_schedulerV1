using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Assignment.Contracts.DTO;
using Assignment.Core.Handlers.QueryHandlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }
   
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RoleDTO>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _mediator.Send(new GetAllRolesQuery());
        return Ok(roles);
    }
   [HttpGet("{id}")]
    [ProducesResponseType(typeof(RoleDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetRole(int id)
    {
        var role = await _mediator.Send(new GetRoleByIdQuery(id));
        if (role == null)
            return NotFound();

        return Ok(role);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO model)
    {
        var command = new CreateRoleCommand(model);
        var roleId = await _mediator.Send(command);
         return CreatedAtAction(nameof(GetRole), new { id = roleId }, roleId);
    }
    }
}
