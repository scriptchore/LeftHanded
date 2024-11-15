using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Interfaces;
using INFRASTRUCTURE.Identity;

namespace INFRASTRUCTURE.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppIdentityDbContext _context;

        private Hashtable _repositories;

        public UnitOfWork(AppIdentityDbContext context)
        {
            this._context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
           if (_repositories == null) _repositories = new Hashtable();

           var type = typeof(T).Name;

           if(! _repositories.ContainsKey(type))
           {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType
                .MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
           }

           return (IGenericRepository<T>) _repositories[type];
        }
    }
}