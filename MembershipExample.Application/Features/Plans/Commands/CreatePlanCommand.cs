using MediatR;
using MembershipExample.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Plans.Commands
{

    public record CreatePlanCommand(string Name, string Description, decimal Price, int DurationInDays) : IRequest<PlanDto>;
}
