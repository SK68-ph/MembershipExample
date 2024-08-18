using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.Exceptions
{

    public class PlanNotFoundException : Exception
    {
        public PlanNotFoundException(string message) : base(message)
        {
        }
    }
}
