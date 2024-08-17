using MediatR;
using MembershipExample.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.Command.Membership
{
    public record UpdateMembershipCommand(int Id, int PlanId) : IRequest<MembershipDto>;
}
