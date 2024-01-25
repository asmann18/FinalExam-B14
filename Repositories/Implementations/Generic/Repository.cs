using FinalExam_B14.DAL;
using FinalExam_B14.Models.Common;
using FinalExam_B14.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinalExam_B14.Repositories.Implementations.Generic
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
           return await _context.Set<T>().AnyAsync(expression);
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null, params string[] includes)
        {
            IQueryable<T> query= _context.Set<T>();
            if (expression != null)
            {

                 query = _context.Set<T>().Where(expression);
            }
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
            
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes)
        {
            IQueryable<T> entity = _context.Set<T>();
            foreach (var include in includes)
            {
                entity = entity.Include(include);
            }
            return await entity.FirstOrDefaultAsync(expression);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
