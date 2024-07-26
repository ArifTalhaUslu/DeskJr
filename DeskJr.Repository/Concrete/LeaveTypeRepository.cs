using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repositories.Concrete;
using DeskJr.Repository.Abstract;

namespace DeskJr.Repository.Concrete
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly AppDbContext _context;

        public LeaveTypeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

