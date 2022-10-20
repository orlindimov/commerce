using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        IQueryable<T> GetAll();

        IQueryable<T> Where(Expression<Func<T, bool>> expression);//veri tabanina gitmez

        Task<bool> AnyAsync(Expression<Func<T,bool>>expression);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);//birden fazla veri ekler

        void Update(T entity);

        void Remove(T entitiy);

        void RemoveRange(IEnumerable<T> entities);

        //veri tabanina burada yansimaz
        

    }
}
