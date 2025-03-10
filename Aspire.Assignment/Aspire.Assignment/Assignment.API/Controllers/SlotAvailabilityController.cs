using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Assignment.Contracts.DTO; 
using Assignment.Core.Exceptions;
using Assignment.Core.Handlers.Commands;
using Assignment.Core.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotAvailabilityController : ControllerBase
    {

        private readonly IMediator _mediator;

        public SlotAvailabilityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateSlot")]
        public async Task<IActionResult> CreateSlot([FromBody] SlotDetailsDTO model)
        {
            
            var loggedInUser = User.Identity.Name;
            if (model == null)
            {
                return BadRequest("Invalid request: Slot data is missing.");
            }

            try
            {
                var command = new CreateSlotCommand(model);
                var response = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetSlotById), new { userId = model.UserId }, new BaseResponseDTO
                {
                    IsSuccess = true,
                    Errors = ["Slot created successfully."]
                });
            }
            catch (InvalidRequestBodyException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }

        }


        [HttpGet("AllSlots")]
        public async Task<IActionResult> GetAllSlot()
        {
            try
            {
                var query = new GetAllSlotQuery();
                var slots = await _mediator.Send(query);
                return Ok(slots);
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

        [HttpGet("SlotsById")]
        public async Task<IActionResult> GetSlotById(int userId)
        {
            try
            {
                var query = new GetSlotByIdQuery(userId);
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

        [HttpPatch("UpdateSlotStatus")]
        public async Task<IActionResult> UpdateSlotStatus(int slotId, string status)
        {
            try
            {
                var command = new UpdateSlotStatusCommand(slotId, status);
                var response = await _mediator.Send(command);
                return Ok(new { message = "Slot updated successfully." });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }
    }
}





