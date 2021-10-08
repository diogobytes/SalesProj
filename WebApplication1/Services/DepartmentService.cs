using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DepartmentService
    {

        private readonly SalesWebAppContext _context;
        public DepartmentService(SalesWebAppContext context)
        {
            _context = context;
        }
        public List<Department> findAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }

    }
}
