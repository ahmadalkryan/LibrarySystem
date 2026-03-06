using Application.IRepository;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DBContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DBContext dBContext)
        {
            _context = dBContext;
            _dbSet = _context.Set<T>();

        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await _dbSet.AddRangeAsync(entities);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }
            _dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> entities = _dbSet;

            var navProperties = _context.Model.FindEntityType(typeof(T))?
                .GetNavigations()
                .Select(n => n.Name);

            foreach (var navProperty in navProperties)
            {
                entities = entities.Include(navProperty);
            }
            return await entities.AsNoTracking().ToListAsync();
        }

        public Task<T> GetByIdAsync(int id)
        {
            var navProperties = _context.Model.FindEntityType(typeof(T))?
                 .GetNavigations()
                 .Select(n => n.Name);
            IQueryable<T> query = _dbSet;
            foreach (var navProperty in navProperties)
            {
                query = query.Include(navProperty);
            }
            return query.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

      public async   Task<IEnumerable<T>> GetAllWitAllIncludeAsync(Expression<Func<T, bool>> filter)
        {
           IQueryable<T> query = _dbSet;
            var navProperties = _context.Model.FindEntityType(typeof(T))?
                .GetNavigations()
                .Select(n => n.Name);

            foreach (var navProperty in navProperties)
            {
                query = query.Include(navProperty);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }
    }
}
