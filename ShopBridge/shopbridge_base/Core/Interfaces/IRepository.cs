using Shopbridge_base.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAllAsync();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync<I>(I id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<List<T>> InsertAll(List<T> entities);
        Task<List<T>> UpdateAll(List<T> entities);
        Task<List<T>> DeleteAll(List<T> entities);
        Task<DataTable> BulkInsert(DataTable dataTable);
    }
}
