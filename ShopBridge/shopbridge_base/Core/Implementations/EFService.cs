using Shopbridge_base.Core.Interfaces;
using Shopbridge_base.Data.Repository;
using Shopbridge_base.Domain.Models.DTO;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopbridge_base.Core.Implementations
{
    public class EFService<T> : IEFService<T> where T : class
    {
        private IRepository<T> _repository;
        public EFService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<T>> AddAsync(T entity)
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.Data = await _repository.AddAsync(entity);
            return response;
        }

        public async Task<ApiResponse<bool>> DeleteAsync(T entity)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();
            response.Data = await _repository.DeleteAsync(entity);
            return response;
        }

        public IQueryable<T> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync<I>(I id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ApiResponse<T>> UpdateAsync(T entity)
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.Data = await _repository.UpdateAsync(entity);
            return response;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
