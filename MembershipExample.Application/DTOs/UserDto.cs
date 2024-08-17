﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.DTOs
{
    public record UserDto(int Id, string Username, string Email, DateTime CreatedAt, DateTime? LastLoginAt);

}
