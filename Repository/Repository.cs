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

        //public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        //{
        //    IQueryable<T> query = _dbSet;

        //    // Include فقط العلاقات المطلوبة
        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }

        //    return await query.AsNoTracking()
        //        .FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        //}
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



        // تحديث الكيان مع التحقق من حالة التتبع
        //public async Task<T> UpdateAsync(T entity)
        //{
        //    try
        //    {
        //      //  var entry = _context.Entry(entity);

        //        //if (entry.State == EntityState.Detached)
        //        //{
        //        //    // الكيان غير متعقب - استخدم Update
        //        //    _dbSet.Update(entity);
        //        //}
        //        //else
        //        //{
        //        //    // الكيان متعقب بالفعل - فقط حدد أن التغييرات قد تمت
        //        //    entry.State = EntityState.Modified;
        //        //}
        //        _dbSet.Update(entity);
        //        await _context.SaveChangesAsync();
        //        return entity;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                var entry = _context.Entry(entity);

                if (entry.State == EntityState.Detached)
                {
                    // تحقق إذا كان هناك كيان بنفس الـ Id موجود بالفعل في السياق
                    var existingEntity = await _dbSet.FindAsync(GetEntityId(entity));
                    if (existingEntity != null)
                    {
                        // إذا كان موجوداً، قم بتحديث قيمه بدلاً من إضافة كيان جديد
                        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                    }
                    else
                    {
                        // إذا لم يكن موجوداً، استخدم Update
                        _dbSet.Update(entity);
                    }
                }
                else
                {
                    // الكيان متعقب بالفعل - فقط حدد أن التغييرات قد تمت
                    entry.State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // دالة مساعدة للحصول على Id الكيان
        private object GetEntityId(T entity)
        {
            var property = typeof(T).GetProperty("Id") ??
                           typeof(T).GetProperty("BorrowingId") ??
                           typeof(T).GetProperty("BorrowingRecordId");

            return property?.GetValue(entity);
        }


        //get all with then include 
        public async Task<IEnumerable<T>> GetAllWithThenIncludeAsync(
                                                  Expression<Func<T, bool>> filter = null,
                                                                    params Func<IQueryable<T>, IQueryable<T>>[] includeActions)
        {
            IQueryable<T> query = _dbSet;

            // تطبيق الـ Include actions
            foreach (var includeAction in includeActions)
            {
                query = includeAction(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.AsNoTracking().ToListAsync();
        }

     

        public async Task<IEnumerable<T>> GetAllWitAllIncludeAsync(Expression<Func<T, bool>> filter)
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
       return await query.AsNoTracking().ToListAsync();
          //  return await query.ToListAsync();
        }

        // ✅ دالة مساعدة للحصول على الـ Id
        //private int GetEntityId(T entity)
        //{
        //    var property = typeof(T).GetProperty("Id") ??
        //                   typeof(T).GetProperty("id") ??
        //                   typeof(T).GetProperties().FirstOrDefault(p => p.Name.EndsWith("Id"));

        //    return property != null ? (int)property.GetValue(entity) : 0;
        //}
        //public async Task<T> UpdateAsync(T entity, params string[] propertiesToUpdate)
        //{
        //    try
        //    {
        //        var existingEntity = await _dbSet.FindAsync(EF.Property<int>(entity, "Id"));
        //        if (existingEntity == null)
        //        {
        //            throw new Exception("Entity not found");
        //        }

        //        if (propertiesToUpdate == null || propertiesToUpdate.Length == 0)
        //        {
        //            // If no specific properties are provided, update all properties
        //            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        //        }
        //        else
        //        {
        //            foreach (var property in propertiesToUpdate)
        //            {
        //                var propertyValue = GetPropertyValue(entity, property);

        //                _context.Entry(existingEntity).Property(property).CurrentValue = propertyValue;

        //                _context.Entry(existingEntity).Property(property).IsModified = true;



        //            }
        //        }


        //        await _context.SaveChangesAsync();
        //        return existingEntity;




        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private object GetPropertyValue(T entity, string propertyName)
        //{
        //    return typeof(T).GetProperty(propertyName)?.GetValue(entity);
        //}

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }





    }
}
