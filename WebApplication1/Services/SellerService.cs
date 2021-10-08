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
        public async Task<List<Seller>> findAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }
        public async Task InsertAsync(Seller obj)
        {

            _context.Add(obj);
           await _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj=> obj.Department).FirstOrDefaultAsync(obj => obj.Id == id); // join table
        }
        public async Task RemoveASync(int id)
        {
            try
            {
                var obj = _context.Seller.Find(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }catch(DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
        public async Task UpdateASync(Seller seller)
        {
            bool hasAny = _context.Seller.Any(x => x.Id == seller.Id);
            if (!hasAny) throw new NotFoundException("Id not Found");
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
