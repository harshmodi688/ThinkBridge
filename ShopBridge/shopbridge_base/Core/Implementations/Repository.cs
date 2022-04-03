using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Shopbridge_Context _dbcontext;

        public Repository(Shopbridge_Context dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<T> AddAsync(T entity)
        {
            T returnEntity = null;
            EntityEntry<T> dbentry = null;
            try
            {
                dbentry = _dbcontext.Entry(entity);
                if (dbentry.State != EntityState.Detached)
                {
                    dbentry.State = EntityState.Added;
                }
                else
                {
                    returnEntity = (await _dbcontext.Set<T>().AddAsync(entity)).Entity;
                }

                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {
                dbentry.State = EntityState.Detached;
                throw;
            }
            return returnEntity;
        }

        public Task<DataTable> BulkInsert(DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> DeleteAll(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            EntityEntry<T> dbentry = null;
            try
            {
                dbentry = _dbcontext.Entry(entity);
                if (dbentry.State != EntityState.Deleted)
                {
                    dbentry.State = EntityState.Deleted;
                }
                else
                {
                    _dbcontext.Set<T>().Attach(entity);
                    _dbcontext.Set<T>().Remove(entity);
                }
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbcontext.Set<T>()
           .Where(expression).AsNoTracking();
        }

        public IQueryable<T> GetAllAsync()
        {
            return _dbcontext.Set<T>();
        }

        public async Task<T> GetByIdAsync<I>(I id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public Task<List<T>> InsertAll(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> UpdateAll(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            T returnEntity = null;
            EntityEntry<T> dbentry = null;
            try
            {
                dbentry = _dbcontext.Entry(entity);
                if (dbentry.State == EntityState.Detached)
                {
                    returnEntity = _dbcontext.Set<T>().Attach(entity).Entity;
                }
                else
                {
                    returnEntity = entity;
                }
                dbentry.State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {
                dbentry.State = EntityState.Unchanged;
                throw;
            }
            return returnEntity;
        }
    }
}
