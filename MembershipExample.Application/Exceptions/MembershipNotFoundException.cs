using System;

namespace MembershipExample.Application.Exceptions
{
    public class MembershipNotFoundException : Exception
    {
        public MembershipNotFoundException(string message) : base(message)
        {
        }
    }
}
