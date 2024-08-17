using MembershipExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Domain.Services
{
    public class MembershipService
    {
        public bool IsMembershipActive(Membership membership)
        {
            return membership.EndDate == null || membership.EndDate > DateTime.Now;
        }
    }
}
