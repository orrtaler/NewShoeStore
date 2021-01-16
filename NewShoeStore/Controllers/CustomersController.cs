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
    public class CustomersController : Controller
    {
        private readonly NewShoeStoreContext _context;

        public CustomersController(NewShoeStoreContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("user") == "4")
                {
                return View(await _context.Customer.ToListAsync());
                }
            return RedirectToAction("Index", "Home");
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("user") == "4")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var customer = await _context.Customer
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (customer == null)
                {
                    return NotFound();
                }
                else
                    return View(customer);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,City,Country,Street,HouseNumber,Mail,Password,Order")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("user", customer.Id.ToString());
                string id = HttpContext.Session.GetString("cart");
                string afterId = id.Replace(",", "");
                int newId = Int32.Parse(afterId);
                return RedirectToAction("Details", "Shoes", new { id = newId });
            }
            else
            {
                ViewData["MissingInformation"] = "חסרים פרטים חשובים ברישום";
            }
            return View(customer);
        }


        // GET: Customers/LogIn
        public IActionResult LogIn()
        {
            return View();
        }

        // POST: Customers/LogIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn([Bind("Id,FullName,City,Country,Street,HouseNumber,Mail,Password,Order")] Customer customer)
        {
            var q = from a in _context.Customer
                    where customer.Mail == a.Mail &&
                          customer.Password == a.Password
                    select a;

            if (q.Count() > 0 )
            {
                HttpContext.Session.SetString("user", q.First().Id.ToString());
                string id = HttpContext.Session.GetString("cart");
                string afterId = id.Replace(",", "");
                int newId = Int32.Parse(afterId);
                return RedirectToAction("Details" , "Shoes" , new {id = newId });
            }
            else
            {
                ViewData["Error"] = "משתמש לא קיים במערכת!!!";
            }
            return RedirectToAction(nameof(Create),customer);
            //return View(customer);
        }


        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("user") == "4")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var customer = await _context.Customer.FindAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,City,Country,Street,HouseNumber,Mail,Password,Order")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("user") == "4")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var customer = await _context.Customer
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (customer == null)
                {
                    return NotFound();
                }

                return View(customer);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
