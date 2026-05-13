using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task SaveChangeAsync();
        void Update(T entity);
        void Delete(T entity);
    }
}
