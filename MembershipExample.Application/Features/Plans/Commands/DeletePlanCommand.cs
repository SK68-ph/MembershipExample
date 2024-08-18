using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Plans.Commands
{
    public record DeletePlanCommand(int Id) : IRequest<bool>;
}
