using DATN_Ecommerce.Extension;
using Microsoft.AspNetCore.Mvc;
using DATN_Ecommerce.ModelsView;

namespace DATN_Ecommerce.Controllers.Components
{
    public class HeaderCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            return View(cart);
        }
    }
}
