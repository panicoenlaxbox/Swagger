using Microsoft.AspNetCore.Mvc;

namespace ApiNSwag.Controllers
{
    public class EfController : Controller
    {
        private readonly ShopContext _context;
        public EfController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context);
        }
    }
}