using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Memberships.Commands
{
    public record DeleteMembershipCommand(int Id) : IRequest<bool>;
}
