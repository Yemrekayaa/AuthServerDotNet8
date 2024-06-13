using System.IO.Compression;
using System.Linq.Expressions;
using AuthServer.Core.Repository;
using AuthServer.Core.Service;
using AuthServer.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTOs;

namespace AuthServer.Service.Services
{
    public class GenericService<T, TDto> : IGenericService<T, TDto> where TDto : class where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto<TDto>> AddAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<T>(entity);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
            return ResponseDto<TDto>.Success(newDto,200);
        }

        public async Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync()
        {
            var entitys = await _repository.GetAll().ToListAsync();
            var newEntitys = ObjectMapper.Mapper.Map<List<TDto>>(entitys);
            return ResponseDto<IEnumerable<TDto>>.Success(newEntitys,200);
        }

        public async Task<ResponseDto<TDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if(entity == null){
                return ResponseDto<TDto>.Fail("Id not found",404,true);
            }
            var entityDto = ObjectMapper.Mapper.Map<TDto>(entity);
            return ResponseDto<TDto>.Success(entityDto,200);
        }

        public async Task<ResponseDto<NoDataDto>> RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if(entity == null){
                return ResponseDto<NoDataDto>.Fail("Id not found",404,true);
            }
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            return ResponseDto<NoDataDto>.Success(204);
        }

        public async Task<ResponseDto<NoDataDto>> UpdateAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null){
                return ResponseDto<NoDataDto>.Fail("Id Not Found",404,true);
            }
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return ResponseDto<NoDataDto>.Success(204);
        }

        public ResponseDto<IEnumerable<TDto>> Where(Expression<Func<T, bool>> expression)
        {
            var entity = _repository.Where(expression);
            var entityDto = ObjectMapper.Mapper.Map<List<TDto>>(entity.ToList());
            return ResponseDto<IEnumerable<TDto>>.Success(entityDto,200);
        }
    }
}