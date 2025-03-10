using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using Assignment.Core.Handlers.Commands;
using Assignment.Core.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers;

public class PanelCoordinatorController : ControllerBase
{
    private readonly IMediator _mediator;

    // Constructor to initialize mediator and configuration
    public PanelCoordinatorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AllocateDate")]
    public async Task<IActionResult> AllocateDate([FromBody] AllocateDateDTO model)
    {
        if (model == null)
        {
            return BadRequest("Invalid request: Slot data is missing.");
        }
        try
        {
            var command = new AllocateDateCommand(model);
            var response = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetAllAllocatedate), new { userId = model.PanelMemberID }, new BaseResponseDTO
                {
                    IsSuccess = true,
                    Message = ["Slot created successfully."]
                });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("PanelAllocations")]
    public async Task<IActionResult> GetAllAllocatedate()
    {
        try
        {
            var query = new GetAllAllocateDateQuery();
            var dates = await _mediator.Send(query);
            return Ok(dates);
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(new BaseResponseDTO
            {
                IsSuccess = false,
                Errors = new string[] { ex.Message }
            });
        }

    }


    [HttpGet("PanelAllocationById")]
    public async Task<IActionResult> GetPanelAllocationById(int userId)
    {
        try
        {
            var query = new GetPanelAllocationByIdQuery(userId);

            var slot = await _mediator.Send(query);
            return Ok(slot);
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(new BaseResponseDTO
            {
                IsSuccess = false,
                Errors = new string[] { ex.Message }
            });
        }
    }
}
