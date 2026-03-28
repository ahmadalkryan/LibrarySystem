using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface IRepository<T> where T : class
    {

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWitAllIncludeAsync(Expression<Func<T, bool>> filter = null);
        Task<IEnumerable<T>> GetAllWithThenIncludeAsync(
       Expression<Func<T, bool>> filter = null,
       params Func<IQueryable<T>, IQueryable<T>>[] includeActions);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
      Task<T> UpdateAsync(T entity);
      //  Task<T> UpdateAsync(T entity, params string[] propertiesToUpdate);
        Task<bool> DeleteAsync(int id);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<int> SaveChangesAsync();
        

    }
}
