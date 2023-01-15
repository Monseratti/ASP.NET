using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMarketplase.Models;

namespace MyMarketplase.Controllers
{
    public class WarehouseNomenclaturesController : Controller
    {
        private readonly MyAppContext _context;

        public WarehouseNomenclaturesController(MyAppContext context)
        {
            _context = context;
        }

        // GET: WarehouseNomenclatures
        public async Task<IActionResult> Index()
        {
            var myAppContext = _context.WarehouseNoms.Include(w => w.Warehouse);
            return View(await myAppContext.ToListAsync());
        }

        // GET: WarehouseNomenclatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WarehouseNoms == null)
            {
                return NotFound();
            }

            var warehouseNomenclature = await _context.WarehouseNoms
                .Include(w => w.Warehouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warehouseNomenclature == null)
            {
                return NotFound();
            }

            return View(warehouseNomenclature);
        }

        // GET: WarehouseNomenclatures/Create
        public IActionResult Create()
        {
            ViewData["WarehouseID"] = new SelectList(_context.Warehouse, "Id", "Name");
            ViewData["NomID"] = new SelectList(_context.Nomenclatures, "Id", "Name");
            return View();
        }

        // POST: WarehouseNomenclatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WarehouseID,NomID,NomAmount,NomPrice")] WarehouseNomenclature warehouseNomenclature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warehouseNomenclature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WarehouseID"] = new SelectList(_context.Warehouse, "Id", "Name", warehouseNomenclature.WarehouseID);
            ViewData["NomID"] = new SelectList(_context.Nomenclatures, "Id", "Name", warehouseNomenclature.NomID);
            return View(warehouseNomenclature);
        }

        // GET: WarehouseNomenclatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WarehouseNoms == null)
            {
                return NotFound();
            }

            var warehouseNomenclature = await _context.WarehouseNoms.FindAsync(id);
            if (warehouseNomenclature == null)
            {
                return NotFound();
            }
            ViewData["WarehouseID"] = new SelectList(_context.Warehouse, "Id", "Name", warehouseNomenclature.WarehouseID);
            ViewData["NomID"] = new SelectList(_context.Nomenclatures, "Id", "Name", warehouseNomenclature.NomID);
            return View(warehouseNomenclature);
        }

        // POST: WarehouseNomenclatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WarehouseID,NomID,NomAmount,NomPrice")] WarehouseNomenclature warehouseNomenclature)
        {
            if (id != warehouseNomenclature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouseNomenclature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseNomenclatureExists(warehouseNomenclature.Id))
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
            ViewData["WarehouseID"] = new SelectList(_context.Warehouse, "Id", "Name", warehouseNomenclature.WarehouseID);
            ViewData["NomID"] = new SelectList(_context.Nomenclatures, "Id", "Name", warehouseNomenclature.NomID);
            return View(warehouseNomenclature);
        }

        // GET: WarehouseNomenclatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WarehouseNoms == null)
            {
                return NotFound();
            }

            var warehouseNomenclature = await _context.WarehouseNoms
                .Include(w => w.Warehouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warehouseNomenclature == null)
            {
                return NotFound();
            }

            return View(warehouseNomenclature);
        }

        // POST: WarehouseNomenclatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WarehouseNoms == null)
            {
                return Problem("Entity set 'MyAppContext.WarehouseNoms'  is null.");
            }
            var warehouseNomenclature = await _context.WarehouseNoms.FindAsync(id);
            if (warehouseNomenclature != null)
            {
                _context.WarehouseNoms.Remove(warehouseNomenclature);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarehouseNomenclatureExists(int id)
        {
          return _context.WarehouseNoms.Any(e => e.Id == id);
        }
    }
}
