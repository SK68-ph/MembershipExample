using FluentValidation;
using MediatR;
using MembershipExample.Application.DTOs;
using MembershipExample.Application.Exceptions;
using MembershipExample.Application.Features.Plans.Commands;
using MembershipExample.Application.Features.Plans.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MembershipExample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlansController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanDto>>> GetAllPlans()
        {
            try
            {
                var query = new GetAllPlansQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanDto>> GetPlanById(int id)
        {
            try
            {
                var query = new GetPlanByIdQuery(id);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PlanDto>> CreatePlan(CreatePlanCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetPlanById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PlanDto>> UpdatePlan(int id, UpdatePlanCommand command)
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
        public async Task<ActionResult> DeletePlan(int id)
        {
            try
            {
                var command = new DeletePlanCommand(id);
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
                PlanNotFoundException => NotFound(new { error = ex.Message }),
                ValidationException => BadRequest(new { error = ex.Message }),
                _ => StatusCode(500, new { error = ex.Message })
            };
        }
    }

}
