using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.DTOs
{
    public record MembershipDto(int Id, int UserId, int PlanId, DateTime StartDate, DateTime EndDate, bool IsActive);

}
