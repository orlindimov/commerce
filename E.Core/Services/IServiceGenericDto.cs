using E.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E.Core.Services
{
    public interface IServiceGenericDto<TEntity, TDto> where TEntity :class where TDto:class
    {
        Task<CustomResponseDto<TDto>> GetByIdAsync(int id);

        Task<CustomResponseDto<IEnumerable<TDto>>> GetAllAsync();

        Task<CustomResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);

        Task<CustomResponseDto<TDto>> AddAsync(TDto entity);

        Task<CustomResponseDto<NoContentDto>> Remove(int id);

        Task<CustomResponseDto<NoContentDto>> Update(TDto entity, int id);
    }
}
