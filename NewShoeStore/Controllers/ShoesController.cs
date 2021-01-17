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
        public async Task<IActionResult> Index(string name)
        {
            if(name == null)
            {
                return View(await _context.Shoe.ToListAsync());
            }
            var s = from Shoe in _context.Shoe
                    where Shoe.Name.Contains(name)
                    orderby Shoe.Name
                    select Shoe;
            return View(await s.ToListAsync());
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
            shoe.Views++;
            _context.Update(shoe);
            await _context.SaveChangesAsync();

            return View(shoe);
        }

        // GET: Shoes/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("user") == "4")
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
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
                    where Shoe.Category == ""
                    select Shoe;
            if (name == "בנים" || name == "בנות")
            {
                s = from Shoe in _context.Shoe
                    where Shoe.Category.Contains(name) && !(Shoe.Category.Contains("בייבי"))
                    orderby Shoe.Category
                    select Shoe;
            }
            else
            {
                s = from Shoe in _context.Shoe
                    where Shoe.Category.Contains(name)
                    orderby Shoe.Category
                    select Shoe;
            }
            return View(await s.ToListAsync());
        }

        public IActionResult AddToCart(int Id)
        {
            string cart = HttpContext.Session.GetString("cart");
            if (cart != null && cart.Any(c => cart.Contains(Id.ToString())))
            {
                        return RedirectToAction("Details", "Shoes", new { id = Id });
            }
            if (cart == null)
                cart = "";
           cart += "," + Id;
           HttpContext.Session.SetString("cart", cart);
            if (HttpContext.Session.GetString("user") == null || HttpContext.Session.GetString("user") == "")
            {
                return RedirectToAction("Create", "Customers");
            }
            return RedirectToAction("Details", "Shoes", new { id = Id });
        }

        public IActionResult DeleteFromCart(int id)
        {
            string c =HttpContext.Session.GetString("cart");
            string i = "," + id;
            string after = c.Replace(i, "");
            HttpContext.Session.SetString("cart", after);
            return RedirectToAction("Cart", "Shoes");
        }

        public async Task<IActionResult> Cart(int Id)
        {
            string cart = HttpContext.Session.GetString("cart");
            var products = new List<Shoe>();
            if (cart != null && cart !="")
            {
                string[] productIds = cart.Split(",", StringSplitOptions.RemoveEmptyEntries);
                products = _context.Shoe.Where(x => productIds.Contains(x.Id.ToString())).ToList();
                return View(products);
            }
            return RedirectToAction("Details", "Shoes", new { id = Id });
            //return RedirectToAction("Index", "Home");
        }

        // GET: Shoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("user") == "4")
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
            return RedirectToAction("Index", "Home");
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
            if (HttpContext.Session.GetString("user") == "4")
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
            return RedirectToAction("Index", "Home");
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
