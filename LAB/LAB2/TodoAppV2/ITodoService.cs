using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAppV2
{
    public interface ITodoService
    {
        public Task<List<Todo>> GetAllAsync();
        public Task<Todo> GetAsync(int id);
        public Task addASync(string title);
        public Task DeleteAsync(int id);
        public Task UpdateAsync(int id, string title);
        public Task ToggleTodoAsync(int id);
    }
}
