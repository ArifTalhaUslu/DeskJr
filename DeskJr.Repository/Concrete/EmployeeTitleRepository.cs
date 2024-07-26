using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repositories.Concrete;
using DeskJr.Repository.Abstract;

namespace DeskJr.Repository.Concrete
{
    public class EmployeeTitleRepository : GenericRepository<EmployeeTitle>, IEmployeeTitleRepository
    {
        private readonly AppDbContext _context;

        public EmployeeTitleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

