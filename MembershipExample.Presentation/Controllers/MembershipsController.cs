using MediatR;
using MembershipExample.Application.DTOs;
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

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<MembershipDto>>> GetMembershipsByUser(int userId)
        {
            var query = new GetMembershipsByUserIdQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MembershipDto>> CreateMembership(CreateMembershipCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetMembershipsByUser), new { userId = result.UserId }, result);
        }

    }

}
