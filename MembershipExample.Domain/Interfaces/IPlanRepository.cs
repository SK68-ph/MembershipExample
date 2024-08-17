using MembershipExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Domain.Interfaces
{
    public interface IPlanRepository
    {
        Task<Plan> GetByIdAsync(int id);
        Task<List<Plan>> GetAllAsync();
        Task<Plan> CreateAsync(Plan plan);
        Task UpdateAsync(Plan plan);
        Task DeleteAsync(int id);
    }
}
