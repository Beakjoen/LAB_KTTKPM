using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Application.Services
{
    public interface ITodoService
    {
        Task<List<Infrastructure.Todo>> GetAll();
        Task<Infrastructure.Todo> GetById(int id);
        Task Add(Infrastructure.Todo entity);
        Task Update(Infrastructure.Todo entity);
        Task Delete(int id);
    }
}
