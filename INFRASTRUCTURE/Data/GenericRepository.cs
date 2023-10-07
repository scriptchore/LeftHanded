using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Interfaces;
using CORE.Specifications;
using Microsoft.EntityFrameworkCore;

namespace INFRASTRUCTURE.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        {
            _context = context;
            
        }
        public async Task<T> GetByIdAsync(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
           return await _context.Set<T>().ToListAsync();
        }

        public Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> ListAllAsync(ISpecification<T> spec)
        {
            throw new NotImplementedException();
        }
    }
}