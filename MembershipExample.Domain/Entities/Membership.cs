using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Domain.Entities
{
    public class Membership
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PlanId { get; set; }
        public Plan Plan { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }


}
