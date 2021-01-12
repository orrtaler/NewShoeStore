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
    public class OrdersController : Controller
    {
        private readonly NewShoeStoreContext _context;

        public OrdersController(NewShoeStoreContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            //IF רשמתי
            //כי זה מגדיר שניתן לגשת רק אם המשתמש רשום
            //אחרת הוא יעביר אותו לעמוד רישום
            if (HttpContext.Session.GetString("user") == null)
            {
                return RedirectToAction("Create", "Customers");
            }
            return View(await _context.Order.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //IF רשמתי
            //כי זה מגדיר שניתן לגשת רק אם המשתמש רשום
            //אחרת הוא יעביר אותו לעמוד רישום
            //if (HttpContext.Session.GetString("user") == null)
            //{
            //    return RedirectToAction("Create", "Customers");
            //}
            //var order = await _context.Order.Include(x => x.Shoes).ThenInclude(x => x.Shoe).ToListAsync();

            //string cart = HttpContext.Session.GetString("cart");
            ////var products = new List<Shoe>();
            //if (cart != null)
            //{
            //    string[] productIds = cart.Split(",", StringSplitOptions.RemoveEmptyEntries);
            //    //products = _context.Shoe.Where(x => productIds.Contains(x.Id.ToString())).ToList();
            //    Dictionary<string, int> dict = new Dictionary<string, int>();
            //    foreach (var id in productIds)
            //    {
            //        if (dict.ContainsKey(id))
            //            dict[id]++;
            //        else
            //            dict.Add(id, 1);
            //    }
            //    ViewData["quantityForOrder"] = dict;
            //}
           // return View(order);
            //return View(await _context.Order.ToListAsync());





            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,CardIdNumber,CardName,ExpiryDate,SecurityCode")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.CustomerId = int.Parse(HttpContext.Session.GetString("user"));
                _context.Add(order);
                await _context.SaveChangesAsync();


                string cart = HttpContext.Session.GetString("cart");
                string[] productIds = cart.Split(",", StringSplitOptions.RemoveEmptyEntries);
                foreach (var id in productIds)
                {
                    OrderShoe po = new OrderShoe();
                    po.IdShose = int.Parse(id);
                    po.IdOrder = order.Id;
                    _context.Add(po);
                    await _context.SaveChangesAsync();
                }
                
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("cart", "");
                return RedirectToAction("Index", "Home");
            }
            //return View(order);
            return RedirectToAction("Index", "Home");
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,CardIdNumber,CardName,ExpiryDate,SecurityCode")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
