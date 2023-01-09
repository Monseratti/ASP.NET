using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMarketplase.Models;

namespace MyMarketplase.Controllers
{
    [Authorize(Roles = "admin, manager")]
    public class NomenclaturesController : Controller
    {
        private readonly MyAppContext _context;

        public NomenclaturesController(MyAppContext context)
        {
            _context = context;
        }

        // GET: Nomenclatures
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nomenclatures.ToListAsync());
        }

        // GET: Nomenclatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nomenclatures == null)
            {
                return NotFound();
            }

            var nomenclature = await _context.Nomenclatures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nomenclature == null)
            {
                return NotFound();
            }
            ViewData["Images"] = await _context.FilesPaths.Where(f=>f.NomID.Equals(id)).ToListAsync();
            return View(nomenclature);
        }

        // GET: Nomenclatures/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Nomenclatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CategoryID")] Nomenclature nomenclature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nomenclature);
                await _context.SaveChangesAsync();

                var files = Request.Form.Files;

                for (int i = 0; i < files.Count; i++)
                {
                    var File = new FilesPath();
                    File.NomID = _context.Nomenclatures.Where(n=>n.Name.Equals(nomenclature.Name)
                                                                &&n.Description.Equals(nomenclature.Description)
                                                                &&n.CategoryID.Equals(nomenclature.CategoryID)).FirstOrDefault()!.Id;
                    var galleryDir = $@"{Directory.GetCurrentDirectory()}\wwwroot\Files\{nomenclature!.Name.Replace(" ", "_")}\Gallery";
                    Directory.CreateDirectory(galleryDir);
                    File.Path = $@"{galleryDir}\{files[i].FileName.Replace(" ", "_")}";
                    using (FileStream fs = new FileStream(File.Path, FileMode.Create))
                    {
                        await files[i].CopyToAsync(fs);
                    }
                    File.Path = $@"{galleryDir}\{files[i].FileName.Replace(" ", "_")}".Split("wwwroot")[1];
                    _context.FilesPaths.Add(File);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(nomenclature);
        }

        // GET: Nomenclatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nomenclatures == null)
            {
                return NotFound();
            }

                ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name");
                var nomenclature = await _context.Nomenclatures.FindAsync(id);
                if (nomenclature == null)
                {
                    return NotFound();
                }
                ViewData["Images"] = await _context.FilesPaths.Where(f => f.NomID.Equals(id)).ToListAsync();
                return View(nomenclature);
        }

        // POST: Nomenclatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CategoryID")] Nomenclature nomenclature)
        {
            if (id != nomenclature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldNom = await _context.Nomenclatures.AsNoTracking().FirstAsync(o=>o.Id.Equals(id));
                    var files = Request.Form.Files;
                    if (files.Count> 0)
                    {
                        var oldFile = await _context.FilesPaths.Where(f=>f.NomID.Equals(id)).ToListAsync();
                        foreach (var item in oldFile)
                        {
                            using (FileStream fs = new FileStream($@"{Directory.GetCurrentDirectory()}\wwwroot\{item.Path}", FileMode.Truncate))
                            {
                                await fs.FlushAsync();
                            }
                        }
                        _context.FilesPaths.RemoveRange(oldFile);
                        await _context.SaveChangesAsync();

                        _context.Update(nomenclature);
                        await _context.SaveChangesAsync();

                        for (int i = 0; i < files.Count; i++)
                        {
                            try
                            {
                                var File = new FilesPath();
                                File.NomID = id;
                                var galleryDir = $@"{Directory.GetCurrentDirectory()}\wwwroot\Files\{nomenclature!.Name.Replace(" ", "_")}\Gallery";
                                Directory.CreateDirectory(galleryDir);
                                File.Path = $@"{galleryDir}\{files[i].FileName}";
                                using (FileStream fs = new FileStream(File.Path, FileMode.Create))
                                {
                                    await files[i].CopyToAsync(fs);
                                }
                                File.Path = $@"{galleryDir}\{files[i].FileName}".Split("wwwroot")[1];
                                _context.FilesPaths.Add(File);
                                await _context.SaveChangesAsync();
                            }
                            catch (Exception)
                            {
                                break;
                            }
                        }
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NomenclatureExists(nomenclature.Id))
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
            return View(nomenclature);
        }

        #region csc

        #endregion

        // GET: Nomenclatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nomenclatures == null)
            {
                return NotFound();
            }

            var nomenclature = await _context.Nomenclatures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nomenclature == null)
            {
                return NotFound();
            }

            return View(nomenclature);
        }

        // POST: Nomenclatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nomenclatures == null)
            {
                return Problem("Entity set 'MyAppContext.Nomenclatures'  is null.");
            }
            var nomenclature = await _context.Nomenclatures.FindAsync(id);
            if (nomenclature != null)
            {
                _context.Nomenclatures.Remove(nomenclature);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NomenclatureExists(int id)
        {
            return _context.Nomenclatures.Any(e => e.Id == id);
        }
    }
}
