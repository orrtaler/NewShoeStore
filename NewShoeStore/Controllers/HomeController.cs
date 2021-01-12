using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewShoeStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NewShoeStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       // [HttpPost]
       /* public async Task<IActionResult> Search(string name)
        {
            var s = from Shoe in _context.Shoe
                    where Shoe.Category(name.ToLower())
                    orderby Shoe.Category
                    select Shoe;
            return RedirectToAction(nameof(Index), await s.ToListAsync());
        }*/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TermsofUse()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
