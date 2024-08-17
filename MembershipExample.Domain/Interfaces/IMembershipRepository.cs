using MembershipExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Domain.Interfaces
{
    public interface IMembershipRepository
    {
        Task<Membership> GetByIdAsync(int id);
        Task<List<Membership>> GetByUserIdAsync(int userId);
        Task<Membership> CreateAsync(Membership membership);
        Task UpdateAsync(Membership membership);
        Task DeleteAsync(int id);
    }


}
