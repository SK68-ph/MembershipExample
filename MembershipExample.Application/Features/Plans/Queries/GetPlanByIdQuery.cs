﻿using MediatR;
using MembershipExample.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Plans.Queries
{
    public record GetPlanByIdQuery(int Id) : IRequest<PlanDto>;
}
