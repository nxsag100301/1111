using DATN_Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace DATN_Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly dbMarketsContext _context;

        public ProductController(dbMarketsContext context)
        {
            _context = context;
        }

        [Route("shop.html", Name = "ProductIndex")]
        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 10;
                var lsProductss = _context.Products.AsNoTracking().OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsProductss, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Route("/{Alias}", Name = "ProductList")]
        public IActionResult List(string Alias, int page = 1)
        {
            try
            {
                    var pageSize = 10;
                    var danhmuc = _context.Categories.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);
                    var lsProducts = _context.Products
                        .AsNoTracking()
                        .Where(x => x.CatId == danhmuc.CatId)
                        .OrderByDescending(x => x.DateCreated);
                    PagedList<Product> models = new PagedList<Product>(lsProducts, page, pageSize);
                    ViewBag.CurrentPage = page;
                    ViewBag.CurrentCat = danhmuc;
                    return View(models);
                }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [Route("/{Alias}-{id}.html", Name = "ProductDetails")]
        public IActionResult Details(int id)
        {
            try
            {
                var product = _context.Products.Include(x => x.Cat).FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                var lsProduct = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CatId == product.CatId && x.ProductId != id && x.Active == true)
                    .Take(4)
                    .OrderByDescending(x => x.DateCreated)
                    .ToList();

                ViewBag.SanPham = lsProduct;
                return View(product);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
