using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMarketplase.Models;

namespace MyMarketplase.Controllers
{
    public class OrderNomenclaturesController : Controller
    {
        private readonly MyAppContext _context;

        public OrderNomenclaturesController(MyAppContext context)
        {
            _context = context;
        }

        // GET: OrderNomenclatures
        public async Task<IActionResult> Index()
        {
            var myAppContext = _context.SellingNoms.Include(o => o.Nomenclature).Include(o => o.Order);
            return View(await myAppContext.ToListAsync());
        }

        // GET: OrderNomenclatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SellingNoms == null)
            {
                return NotFound();
            }

            var orderNomenclature = await _context.SellingNoms
                .Include(o => o.Nomenclature)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderNomenclature == null)
            {
                return NotFound();
            }

            return View(orderNomenclature);
        }

        // GET: OrderNomenclatures/Create
        public IActionResult Create()
        {
            ViewData["NomID"] = new SelectList(_context.Nomenclatures, "Id", "Name");
            ViewData["OrderID"] = new SelectList(_context.Orders, "Id", "Name");
            return View();
        }

        // POST: OrderNomenclatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderNomenclature orderNomenclature)
        {
            Order order = new Order() 
            {   OrderStateID = 1, 
                CreateAt = DateTime.Now, 
                UpdateAt = DateTime.Now, 
                UserID = _context.Users.Where(u => u.Name.Equals(User.Identity.Name)).First().Id 
            };
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();

            order = await _context.Orders.Where(o=>o.UserID.Equals(order.UserID)).OrderByDescending(o=> o.CreateAt).FirstOrDefaultAsync();


            Dictionary<Nomenclature, int> cart = new Dictionary<Nomenclature, int>();
            var cookie = Request.Cookies[$"cart{User.Identity!.Name}"];
            if (cookie != null)
            {
                List<Cart> carts = JsonSerializer.Deserialize<List<Cart>>(cookie)!;

                foreach (var item in carts)
                {
                    var product = await _context.Nomenclatures.FindAsync(item.Id);
                    if (cart.ContainsKey(product!))
                    {
                        cart[product!] += item.Amount;
                    }
                    else
                    {
                        cart.Add(product!, item.Amount);
                    }
                }
            }
            foreach (var item in cart)
            {
                OrderNomenclature oNom = new OrderNomenclature();
                oNom.OrderID = order!.Id;
                oNom.NomID = item.Key.Id;
                oNom.SellAmount = item.Value;
                oNom.SellingPrice = _context.WarehouseNoms.Where(w => w.NomID.Equals(item.Key.Id)).OrderByDescending(o=>o.Id).Last().NomPrice;
                await _context.AddAsync(oNom);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Home");
        }

        // GET: OrderNomenclatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SellingNoms == null)
            {
                return NotFound();
            }

            var orderNomenclature = await _context.SellingNoms.FindAsync(id);
            if (orderNomenclature == null)
            {
                return NotFound();
            }
            ViewData["NomID"] = new SelectList(_context.Nomenclatures, "Id", "Name", orderNomenclature.NomID);
            ViewData["OrderID"] = new SelectList(_context.Orders, "Id", "Name", orderNomenclature.OrderID);
            return View(orderNomenclature);
        }

        // POST: OrderNomenclatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderID,NomID,SellAmount,SellingPrice")] OrderNomenclature orderNomenclature)
        {
            if (id != orderNomenclature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderNomenclature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderNomenclatureExists(orderNomenclature.Id))
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
            ViewData["NomID"] = new SelectList(_context.Nomenclatures, "Id", "Name", orderNomenclature.NomID);
            ViewData["OrderID"] = new SelectList(_context.Orders, "Id", "Name", orderNomenclature.OrderID);
            return View(orderNomenclature);
        }

        // GET: OrderNomenclatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SellingNoms == null)
            {
                return NotFound();
            }

            var orderNomenclature = await _context.SellingNoms
                .Include(o => o.Nomenclature)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderNomenclature == null)
            {
                return NotFound();
            }

            return View(orderNomenclature);
        }

        // POST: OrderNomenclatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SellingNoms == null)
            {
                return Problem("Entity set 'MyAppContext.SellingNoms'  is null.");
            }
            var orderNomenclature = await _context.SellingNoms.FindAsync(id);
            if (orderNomenclature != null)
            {
                _context.SellingNoms.Remove(orderNomenclature);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderNomenclatureExists(int id)
        {
          return _context.SellingNoms.Any(e => e.Id == id);
        }
    }
}
