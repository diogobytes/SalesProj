using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Models.ViewModels;
using WebApplication1.Services.Exceptions;
using System.Diagnostics;

namespace WebApplication1.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService) {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.findAllAsync();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.findAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.findAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Object not Found" });
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
           await _sellerService.RemoveASync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)  return RedirectToAction(nameof(Error), new { message = "Not found" }); ;
            return View(obj);

        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) return NotFound();
            List<Department> departments = await _departmentService.findAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments =await  _departmentService.findAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                View(viewModel);
            }
            if (id != seller.Id) return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            try
            {
                await _sellerService.UpdateASync(seller);
            }
            
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
