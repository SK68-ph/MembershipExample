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
    public class MembershipRepository : IMembershipRepository
    {
        private readonly MembershipDbContext _context;

        public MembershipRepository(MembershipDbContext context)
        {
            _context = context;
        }

        public async Task<Membership> CreateAsync(Membership membership)
        {
            _context.Memberships.Add(membership);
            await _context.SaveChangesAsync();
            return membership;
        }

        public async Task DeleteAsync(int id)
        {
            var membership = await _context.Memberships.FindAsync(id);
            if (membership != null)
            {
                _context.Memberships.Remove(membership);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<Membership> GetByIdAsync(int id)
        {
            return await _context.Memberships.FindAsync(id);
        }

        public async Task<List<Membership>> GetByUserIdAsync(int userId)
        {
            return await _context.Memberships.Where(m => m.UserId == userId).ToListAsync();
        }

        public async Task UpdateAsync(Membership membership)
        {
            _context.Entry(membership).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
