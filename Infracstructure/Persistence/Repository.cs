using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructure.Persistence
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ToDo>> GetAllAsync()
        {
            return await _context.ToDos.ToListAsync();
        }

        public async Task AddAsync(ToDo todo)
        {
            _context.ToDos.Add(todo);
            await _context.SaveChangesAsync();
        }
    }
}
