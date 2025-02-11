using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repositories.Concrete;
using DeskJr.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Repository.Concrete
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly AppDbContext _context;

        public LeaveTypeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> HasRelatedLeavesAsync(Guid leaveTypeId)
        {
            return await _context.Leaves.AnyAsync(l => l.LeaveTypeId == leaveTypeId);
        }
    }
}

