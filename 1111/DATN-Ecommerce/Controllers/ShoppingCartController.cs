using AspNetCoreHero.ToastNotification.Abstractions;
using DATN_Ecommerce.Extension;
using DATN_Ecommerce.Models;
using DATN_Ecommerce.ModelsView;
using Microsoft.AspNetCore.Mvc;

namespace DATN_Ecommerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly dbMarketsContext _context;

        public ShoppingCartController(dbMarketsContext context)
        {
            _context = context;
        }

        public List<CartItem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;
            }
        }

        [HttpPost]
        [Route("api/cart/add")]
        public IActionResult AddToCart(int productID, int? amount)
        {
            List<CartItem> giohang = GioHang;
            try
            {
                CartItem item = GioHang.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null)
                {
                    if (amount.HasValue)
                    {
                        item.amount = amount.Value;
                    }
                    else
                    {
                        item.amount++;
                    }
                }
                else
                {
                    Product hh = _context.Products.SingleOrDefault(p => p.ProductId == productID);
                    item = new CartItem
                    {
                        amount = amount.HasValue ? amount.Value : 1,
                        product = hh
                    };
                    giohang.Add(item);
                }
                HttpContext.Session.Set<List<CartItem>>("GioHang", giohang);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Route("api/cart/udate")]
        public IActionResult UpdateCart(int productID, int? amount)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            try
            {
                if(cart!=null)
                {
                    CartItem item = cart.SingleOrDefault(p => p.product.ProductId == productID);
                    if (item != null && amount.HasValue)
                    {
                        item.amount = amount.Value;
                    }
                    HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                }
                return Json(new { success = true}); 
            }
            catch
            {
                return Json(new { success = false});    
            }
        }

        [HttpPost]
        [Route("api/cart/remove")]
        public IActionResult Remove(int productID)
        {
            try
            {
                List<CartItem> giohang = GioHang;
                CartItem item = GioHang.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null)
                {
                    giohang.Remove(item);
                }
                HttpContext.Session.Set<List<CartItem>>("GioHang", giohang);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [Route("cart.html", Name = "Cart")]
        public IActionResult Index()
        {
            var lsGioHang = GioHang;
            return View(GioHang);
        }
    }
}
