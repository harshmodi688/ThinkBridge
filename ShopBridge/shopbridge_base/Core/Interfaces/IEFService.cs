using Shopbridge_base.Domain.Models.DTO;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopbridge_base.Core.Interfaces
{
    public interface IEFService<T> where T : class
    {
        IQueryable<T> GetAllAsync();
        Task<T> GetByIdAsync<I>(I id);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<ApiResponse<T>> AddAsync(T entity);
        Task<ApiResponse<T>> UpdateAsync(T entity);
        Task<ApiResponse<bool>> DeleteAsync(T entity);


    }
}
