using MediatR;
using MembershipExample.Application.DTOs;
using MembershipExample.Application.Features.Users.Commands;
using MembershipExample.Application.Features.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MembershipExample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var query = new GetUsersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUser), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateUser(UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUser), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var command = new DeleteUserCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }

}
