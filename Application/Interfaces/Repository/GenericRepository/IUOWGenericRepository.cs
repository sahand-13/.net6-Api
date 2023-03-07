using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository.GenericRepository
{
    public interface IUOWGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByGuidAsync(Guid id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<ICollection<T>> GetByFilterAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> addListAsync(List<T> entityList);
        Task updateListAsync(List<T> entityList);
        Task DeleteListAsync(List<T> entityList);
    }
}
