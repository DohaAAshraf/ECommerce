using ECommerce.Entities.Models;
using ECommerce.Entities.Repositories;
using ECommerce.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using X.PagedList;

namespace ECommerce.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public HomeController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task<IActionResult> Index(int? page)
        {
            var PageNumber = page ?? 1;
            int PageSize = 8;

            var productsList = await _unitofwork.Product.GetAllAsync();
            var products = productsList.ToPagedList(PageNumber, PageSize);
            return View(products);
        }

        public async Task<IActionResult> Details(int ProductId)
        {
            ShoppingCart obj = new ShoppingCart()
            {
                ProductId = ProductId,
                Product = await _unitofwork.Product.GetFirstorDefaultAsync(v => v.Id == ProductId, Includeword: "Category"),
                Count = 1
            };
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            ShoppingCart Cartobj = await _unitofwork.ShoppingCart.GetFirstorDefaultAsync(
                u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);

            if (Cartobj == null)
            {
                _unitofwork.ShoppingCart.Add(shoppingCart);
                await _unitofwork.CompleteAsync();
                var query = await _unitofwork.ShoppingCart.GetAllAsync(x => x.ApplicationUserId == claim.Value);
                HttpContext.Session.SetInt32(SD.SessionKey,
                    query.ToList().Count()
                   );

            }
            else
            {
                _unitofwork.ShoppingCart.IncreaseCount(Cartobj, shoppingCart.Count);
                await _unitofwork.CompleteAsync();
            }


            return RedirectToAction("Index");
        }
    }
}
