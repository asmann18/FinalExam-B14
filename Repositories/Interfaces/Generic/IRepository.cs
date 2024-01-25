using FinalExam_B14.Models.Common;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq.Expressions;

namespace FinalExam_B14.Repositories.Interfaces
{

    public interface IRepository<T> where T : BaseEntity,new()
    {
        IQueryable<T> GetAll(Expression<Func<T,bool>>? expression=null,params string[] includes);
        Task<T> GetSingleAsync(Expression<Func<T,bool>> expression, params string[] includes);
        Task<bool> AnyAsync(Expression<Func<T,bool>> expression);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}
