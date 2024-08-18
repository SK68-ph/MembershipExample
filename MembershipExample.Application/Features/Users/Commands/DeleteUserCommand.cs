using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Users.Commands
{
    public record DeleteUserCommand(int Id) : IRequest<bool>;
}
