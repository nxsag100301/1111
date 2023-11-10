using DATN_Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace DATN_Ecommerce.Controllers
{
    public class BlogController : Controller
    {
        private readonly dbMarketsContext _context;

        public BlogController(dbMarketsContext context)
        {
            _context = context;
        }
        [Route("blog.html", Name = "Blog")]
        public IActionResult Index(int? page)
        {

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 4;
            var lsTinDangs = _context.TinDangs.AsNoTracking().OrderByDescending(x => x.PostId);
            PagedList<TinDang> models = new PagedList<TinDang>(lsTinDangs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);

        }
        [Route("/tin-tuc/{Alias}-{id}.html", Name = "TinDetails")]
        public IActionResult Details(int id)
        {
            var tindang = _context.TinDangs.FirstOrDefault(x => x.PostId == id);
            if (tindang == null)
            {
                return RedirectToAction("Index");
            }
            var lsBaivietlienquan = _context.TinDangs
                .AsNoTracking()
                .Where(x => x.Published == true && x.PostId != id)
                .Take(3).OrderByDescending(x => x.CreatedDate)
                .ToList();
            ViewBag.Baivietlienquan = lsBaivietlienquan;
            return View(tindang);
        }
    }
}
