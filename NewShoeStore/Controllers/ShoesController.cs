using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewShoeStore.Data;
using NewShoeStore.Models;

namespace NewShoeStore.Controllers
{
    public class ShoesController : Controller
    {
        private readonly NewShoeStoreContext _context;

        public ShoesController(NewShoeStoreContext context)
        {
            _context = context;
        }

        // GET: Shoes
        public async Task<IActionResult> Index()
        {
           
            //if (HttpContext.Session.GetString("cart") == null)
            //{
            //    return View(await _context.Shoe.ToListAsync());
            //}
            //else
            //{
            //    string productId = HttpContext.Session.GetString("cart");
            //    string[] ids = productId.Split(',');
            //    int[] myInts = ids.Select(int.Parse).ToArray();
            //    var purchased = from p in _context.Shoe
            //                    where myInts.Any(s => s == p.Id)
            //                    select p;
            //    var leftItems = _context.Shoe.Except(purchased);
            //    return View(await leftItems.ToListAsync());
            //}
            return View(await _context.Shoe.ToListAsync());
        }

        // GET: Shoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoe = await _context.Shoe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoe == null)
            {
                return NotFound();
            }
            return View(shoe);
            //קוד מאפרת
            //if (HttpContext.Session.GetString("cart") == null)
            //{
            //    return View(shoe);
            //    //return View(await _context.Shoe.ToListAsync());
            //}
            //else
            //{
            //    string productId = HttpContext.Session.GetString("cart");
            //    string[] ids = productId.Split(',');
            //    int[] myInts = ids.Select(int.Parse).ToArray();
            //    var purchased = from p in _context.Shoe
            //                    where myInts.Any(s => s == p.Id)
            //                    select p;
            //    var leftItems = _context.Shoe.Except(purchased);
            //    return View(shoe);
            //    //return View(await _context.Shoe.ToListAsync());
            //    //return View(await leftItems.ToListAsync());
            //}


        }

        // GET: Shoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Color,Price,ProductDescription,Img,Views,Size,Category")] Shoe shoe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["thisUser"] = HttpContext.Session.GetString("user");
            return View(shoe);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string name)
        {
            var s = from Shoe in _context.Shoe
                    where Shoe.Category.Contains(name)
                    orderby Shoe.Category
                    select Shoe;
            return View(await s.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> SearchAsync(string name)
        {
            var s = from Shoe in _context.Shoe
                    where Shoe.Name.Contains(name)
                    orderby Shoe.Name
                    select Shoe;
            //return RedirectToAction(nameof(Index), s.ToListAsync());
            return RedirectToAction("Index", "Shoes", s.ToListAsync());
        }

        //מאפרת
        public IActionResult AddToCart(int Id)
        {
            if (HttpContext.Session.GetString("user") == null)
                    {
                        return RedirectToAction("Create", "Customers");
                    }
            string cart = HttpContext.Session.GetString("cart");
            if (cart == "")
                cart = "";
            cart += "," + Id;
            HttpContext.Session.SetString("cart", cart);
            return RedirectToAction("Index", "Shoes");
           
        }

        //public IActionResult ToGo(int id, [Bind("Id,Name,Color,Price,ProductDescription,Img,Views,Size,Category")] Shoe shoe)
        //{
        //    if(HttpContext.Session.GetString("user")==null)
        //    {
        //        return RedirectToAction("Create", "Customers");
        //    }
        //    //TempData["ShoeId"] = id;

        //    return RedirectToAction("SetForOrder", "OrderShoes", shoe);
        //}

        public async Task<IActionResult> Cart()
        {
            string cart = HttpContext.Session.GetString("cart");
            var products = new List<Shoe>();

            if(cart != null)
            {

            string[] productIds = cart.Split(",", StringSplitOptions.RemoveEmptyEntries);
            products = _context.Shoe.Where(x => productIds.Contains(x.Id.ToString())).ToList();

            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var id in productIds)
            {
                if (dict.ContainsKey(id))
                    dict[id]++;
                else
                    dict.Add(id, 1);
            }
            ViewData["quantity"] = dict;
            }
            return View(products);
        }

        // GET: Shoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoe = await _context.Shoe.FindAsync(id);
            if (shoe == null)
            {
                return NotFound();
            }
            return View(shoe);
        }

        // POST: Shoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Color,Price,ProductDescription,Img,Category,Views,Size")] Shoe shoe)
        {
            if (id != shoe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoeExists(shoe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shoe);
        }

        // GET: Shoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoe = await _context.Shoe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoe == null)
            {
                return NotFound();
            }

            return View(shoe);
        }

        // POST: Shoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoe = await _context.Shoe.FindAsync(id);
            _context.Shoe.Remove(shoe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoeExists(int id)
        {
            return _context.Shoe.Any(e => e.Id == id);
        }
    }
}
