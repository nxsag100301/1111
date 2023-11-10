using DATN_Ecommerce.Extension;
using DATN_Ecommerce.ModelsView;
using Microsoft.AspNetCore.Mvc;

namespace DATN_Ecommerce.Controllers.Components
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            return View(cart);
        }
    }
}
