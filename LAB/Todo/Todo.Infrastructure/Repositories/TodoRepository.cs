using System;
using System.Collections.Generic;
using System.Text;
using Todo.Infrastructure.Data;
using ToDo.Domain.Repositories;
using ToDo.Infrastructure.Repositories;

namespace Todo.Infrastructure.Repositories
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        public TodoRepository(TodoDbContext context) : base(context)
        {
        }
    }
}
