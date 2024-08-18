using FluentValidation;
using MediatR;
using MembershipExample.Application.DTOs;
using MembershipExample.Application.Exceptions;
using MembershipExample.Application.Features.Memberships.Commands;
using MembershipExample.Application.Features.Memberships.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MembershipExample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MembershipsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MembershipDto>> GetMembershipById(int id)
        {
            try
            {
                var query = new GetMembershipByIdQuery(id);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<MembershipDto>>> GetMembershipsByUser(int userId)
        {
            try
            {
                var query = new GetMembershipsByUserIdQuery(userId);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MembershipDto>> CreateMembership(CreateMembershipCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetMembershipsByUser), new { userId = result.UserId }, result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MembershipDto>> UpdateMembership(int id, UpdateMembershipCommand command)
        {
            try
            {
                if (id != command.Id)
                    throw new ValidationException("Id in the body does not match the Id in the URL");

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMembership(int id)
        {
            try
            {
                var command = new DeleteMembershipCommand(id);
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private ActionResult HandleException(Exception ex)
        {
            return ex switch
            {
                MembershipNotFoundException => NotFound(new { error = ex.Message }),
                ValidationException => BadRequest(new { error = ex.Message }),
                _ => StatusCode(500, new { error = ex.Message })
            };
        }
    }

}
