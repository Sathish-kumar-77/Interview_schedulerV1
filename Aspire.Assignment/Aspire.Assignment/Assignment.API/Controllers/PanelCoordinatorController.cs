using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using Assignment.Core.Handlers.Commands;
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

        // Endpoint for POST /api/Pa    nelCoordinator/PostDate
        [HttpPost("AllocateDate")]
        // Action method to allocate dates
        //[Authorize(Roles = "Panel Coordinator")]
        public async Task<IActionResult> PostDate([FromBody] List<AllocateDateDTO> model)
        {
            try
            {
                var responses = new List<object>();
                var errors = new List<string>();
                foreach (var data in model)
                {
                    try
                    {
                        var command = new AllocateDateCommand(data);
                        var response = await _mediator.Send(command);
                        responses.Add(response);
                    }
                    catch (InvalidRequestBodyException exception)
                    {
                        errors.AddRange(exception.Errors);
                    }
                    
                }
                if (errors.Any())
                {
                    return BadRequest(
                        new BaseResponseDTO { IsSuccess = false, Errors = errors.ToArray() }
                    );
                }
                // Return a success message with a 200 OK status code
                return Ok(new { message = "Data successfully stored in the database." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
