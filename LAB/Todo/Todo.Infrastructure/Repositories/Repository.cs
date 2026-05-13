using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Data;
using ToDo.Domain.Repositories;

namespace ToDo.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TodoDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(TodoDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task SaveChangeAsync() => await _context.SaveChangesAsync();

        public async Task SaveChangersAsync() => await _context.SaveChangesAsync();

        public void update(T entity) => _dbSet.Update(entity);

        public void Update(T entity) => _dbSet.Update(entity);
    }
}