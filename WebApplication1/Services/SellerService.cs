using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class SellerService
    {
        private readonly SalesWebAppContext _context;
        public SellerService(SalesWebAppContext context)
        {
            _context = context;

        }
        public List<Seller> findAll()
        {
            return _context.Seller.ToList();
        }
        public void Insert(Seller obj)
        {

            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
