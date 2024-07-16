using System;
using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Repository.Concrete
{
	public class TeamRepository : ITeamRepository
	{
        private readonly AppDbContext _context;
        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
            var affectedRowCount = await _context.SaveChangesAsync();
            return affectedRowCount > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var dbTeam = await _context.Teams.FirstOrDefaultAsync(e => e.ID == id);
            if (dbTeam != null)
            {
                _context.Teams.Remove(dbTeam);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Team>> GetAllAsync()
        {
            var dbTeams = await _context.Teams.ToListAsync();
            return dbTeams;
        }

        public async Task<Team?> GetByIdAsync(Guid id)
        {
            var dbTeam = await _context.Teams.FirstOrDefaultAsync(e => e.ID == id);
            return dbTeam;
        }

        public async Task<bool> UpdateAsync(Team request)
        {
            var affectedRowCount = 0;
            var dbTeam = await _context.Employees.FindAsync(request.ID);
            if (dbTeam != null)
            {
                _context.Entry(dbTeam).CurrentValues.SetValues(request);

                affectedRowCount = await _context.SaveChangesAsync();
            }
            return affectedRowCount > 0;
        }
    }
}

