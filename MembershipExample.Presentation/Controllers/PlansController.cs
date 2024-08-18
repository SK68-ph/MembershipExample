using MediatR;
using MembershipExample.Application.DTOs;
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
            var query = new GetAllPlansQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }


    }

}
