using Todo.Domain.Repositories;

namespace ToDo.Domain.Repositories
{
    public interface ITodoRepository : IRepository<Todo.Infrastructure.Todo>
    {
    }
}