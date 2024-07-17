using System;
using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<bool> AddAsync(T entity);
        public Task<bool> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(Guid id);
        public Task<List<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(Guid id);
    }
  
}

