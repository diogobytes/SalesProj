using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Services
{
    public class DepartmentService
    {

        private readonly SalesWebAppContext _context;
        public DepartmentService(SalesWebAppContext context)
        {
            _context = context;
        }
        public async Task<List<Department>> findAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

    }
}
