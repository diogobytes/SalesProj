using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services.Exceptions;

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
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj=> obj.Department).FirstOrDefault(obj => obj.Id == id); // join table
        }
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
        public void Update(Seller seller)
        {
            if (!_context.Seller.Any(x => x.Id == seller.Id)) throw new NotFoundException("Id not Found");
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
