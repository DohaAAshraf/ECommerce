using ECommerce.Entities.Repositories;
using ECommerce.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
           var orderHeaders = await _unitOfWork.OrderHeader.GetAllAsync();
            ViewBag.Orders = orderHeaders.Count();

            var orderHeadersStatus = await _unitOfWork.OrderHeader.GetAllAsync(x => x.OrderStatus == SD.Approve);
            ViewBag.ApprovedOrders = orderHeadersStatus.Count();

            var ApplicationUsers = await _unitOfWork.ApplicationUser.GetAllAsync();
            ViewBag.Users = ApplicationUsers.Count();

            var products = await _unitOfWork.Product.GetAllAsync();
            ViewBag.Products = products.Count();

            return View();
        }
    }
}
