using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewShoeStore.Data;
using NewShoeStore.Models;

namespace NewShoeStore.Controllers
{
    public class OrderShoesController : Controller
    {
        private readonly NewShoeStoreContext _context;

        public OrderShoesController(NewShoeStoreContext context)
        {
            _context = context;
        }

        // GET: OrderShoes
        public async Task<IActionResult> Index()
        {
            var newShoeStoreContext = _context.OrderShoe.Include(o => o.Order).Include(o => o.Shoe);
            return View(await newShoeStoreContext.ToListAsync());
        }

        // GET: OrderShoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderShoe = await _context.OrderShoe
                .Include(o => o.Order)
                .Include(o => o.Shoe)
                .FirstOrDefaultAsync(m => m.IdShose == id);
            if (orderShoe == null)
            {
                return NotFound();
            }

            return View(orderShoe);
        }

        // GET: OrderShoes/Create
        public IActionResult Create()
        {
            ViewData["IdOrder"] = new SelectList(_context.Order, "Id", "CardName");
            ViewData["IdShose"] = new SelectList(_context.Shoe, "Id", "Color");
            return View();
        }

        // POST: OrderShoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrder,IdShose")] OrderShoe orderShoe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderShoe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOrder"] = new SelectList(_context.Order, "Id", "CardName", orderShoe.IdOrder);
            ViewData["IdShose"] = new SelectList(_context.Shoe, "Id", "Color", orderShoe.IdShose);
            return View(orderShoe);
        }

        // GET: OrderShoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderShoe = await _context.OrderShoe.FindAsync(id);
            if (orderShoe == null)
            {
                return NotFound();
            }
            ViewData["IdOrder"] = new SelectList(_context.Order, "Id", "CardName", orderShoe.IdOrder);
            ViewData["IdShose"] = new SelectList(_context.Shoe, "Id", "Color", orderShoe.IdShose);
            return View(orderShoe);
        }

        // POST: OrderShoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrder,IdShose")] OrderShoe orderShoe)
        {
            if (id != orderShoe.IdShose)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderShoe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderShoeExists(orderShoe.IdShose))
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
            ViewData["IdOrder"] = new SelectList(_context.Order, "Id", "CardName", orderShoe.IdOrder);
            ViewData["IdShose"] = new SelectList(_context.Shoe, "Id", "Color", orderShoe.IdShose);
            return View(orderShoe);
        }

        // GET: OrderShoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderShoe = await _context.OrderShoe
                .Include(o => o.Order)
                .Include(o => o.Shoe)
                .FirstOrDefaultAsync(m => m.IdShose == id);
            if (orderShoe == null)
            {
                return NotFound();
            }

            return View(orderShoe);
        }

        // POST: OrderShoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderShoe = await _context.OrderShoe.FindAsync(id);
            _context.OrderShoe.Remove(orderShoe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderShoeExists(int id)
        {
            return _context.OrderShoe.Any(e => e.IdShose == id);
        }
    }
}
