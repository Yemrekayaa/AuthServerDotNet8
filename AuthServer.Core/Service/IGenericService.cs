using System.Linq.Expressions;
using SharedLibrary.DTOs;

namespace AuthServer.Core.Service
{
    public interface IGenericService<T,TDto> where T : class where TDto : class
        {
        Task<ResponseDto<TDto>> GetByIdAsync(int id);
        Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync();
        ResponseDto<IEnumerable<TDto>> Where(Expression<Func<T,bool>> expression);
        Task<ResponseDto<TDto>> AddAsync(T entity);
        Task<ResponseDto<NoDataDto>> Remove(T entity);
        Task<ResponseDto<NoDataDto>> Update(T entity);
    }
}