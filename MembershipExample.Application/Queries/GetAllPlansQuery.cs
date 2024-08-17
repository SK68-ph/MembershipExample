using MediatR;
using MembershipExample.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.Queries
{
    public record GetAllPlansQuery() : IRequest<IEnumerable<PlanDto>>;
}
