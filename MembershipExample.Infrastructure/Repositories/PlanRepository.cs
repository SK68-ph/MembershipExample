using MembershipExample.Domain.Entities;
using MembershipExample.Domain.Interfaces;
using MembershipExample.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Infrastructure.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly MembershipDbContext _context;

        public PlanRepository(MembershipDbContext context)
        {
            _context = context;
        }

        public async Task<Plan> CreateAsync(Plan plan)
        {
            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        public async Task DeleteAsync(int id)
        {
            var plan = await _context.Plans.FindAsync(id);
            if (plan != null)
            {
                _context.Plans.Remove(plan);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<List<Plan>> GetAllAsync()
        {
            return await _context.Plans.ToListAsync();
        }

        public async Task<Plan> GetByIdAsync(int id)
        {
            return await _context.Plans.FindAsync(id);
        }

        public async Task UpdateAsync(Plan plan)
        {
            _context.Entry(plan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


    }
}
