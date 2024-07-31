
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Repositories.Concrete;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<bool> AddAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            var affectedRowCount = await _context.SaveChangesAsync();
            return affectedRowCount > 0;
        }
        catch (Exception ex)
        {
            throw new BadRequestException("default");
        }
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        var affectedRowCount = 0;
        var dbTeam = await _dbSet.FindAsync(entity.ID);

        if (dbTeam == null)
        {
            throw new NotFoundException($"Employee with ID {entity.ID} not found");
        }

        _context.Entry(dbTeam).CurrentValues.SetValues(entity);
        affectedRowCount = await _context.SaveChangesAsync();
        
        return affectedRowCount > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var dbTeam = await _dbSet.FirstOrDefaultAsync(e => e.ID == id);
        if (dbTeam == null)
        {
            throw new NotFoundException($"Employee with ID {id} not found");
        }
        _dbSet.Remove(dbTeam);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<T>> GetAllAsync()
    {
        var dbTeams = await _dbSet.ToListAsync();
        return dbTeams;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var dbTeam = await _dbSet.FirstOrDefaultAsync(e => e.ID == id);
        if (dbTeam == null)
        {
            throw new NotFoundException($"Employee with ID {id} not found");
        }
        return dbTeam;
    }
}