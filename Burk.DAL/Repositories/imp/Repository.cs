using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Context;
using Burk.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Burk.DAL.Repositories.imp
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BurkDbContext _context;
        public Repository( BurkDbContext context)
        {
            _context=context;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
               return await _context.Set<T>().ToListAsync();
           
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public void AddOne( T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
        public void UpdateOne(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
