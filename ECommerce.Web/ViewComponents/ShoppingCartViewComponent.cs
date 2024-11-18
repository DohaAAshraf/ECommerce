using ECommerce.Entities.Repositories;
using ECommerce.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.Web.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if(claim is not null)
            {
                if (HttpContext.Session.GetInt32(SD.SessionKey) is not null)
                    return View(HttpContext.Session.GetInt32(SD.SessionKey));
                else
                {
                    var result = await _unitOfWork.ShoppingCart.GetAllAsync(x => x.ApplicationUserId == claim.Value);
                    var count = result.ToList().Count;
                    HttpContext.Session.SetInt32(SD.SessionKey, count);
                    return View(HttpContext.Session.GetInt32(SD.SessionKey));
                }
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
