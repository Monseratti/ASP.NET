using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMarketplase.Models;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;

namespace MyMarketplase.Controllers
{
    class Cart
    {
        public int Id { get; set; }
        public int Amount { get; set; }
    }

    public class HomeController : Controller
    {
        MyAppContext db;

        public HomeController(MyAppContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> ToCart()
        {
            Dictionary<Nomenclature, int> pairs = new Dictionary<Nomenclature, int>();
            var cookie = Request.Cookies[$"cart{User.Identity!.Name}"];
            if (cookie != null)
            {
                List<Cart> carts = JsonSerializer.Deserialize<List<Cart>>(cookie)!;

                foreach (var item in carts)
                {
                    var product = await db.Nomenclatures.FindAsync(item.Id);
                    if (pairs.ContainsKey(product!))
                    {
                        pairs[product!] += item.Amount;
                    }
                    else
                    {
                        pairs.Add(product!, item.Amount);
                    }
                }
                ViewData["Prices"] = await db.WarehouseNoms.ToListAsync();
                return View(pairs);
            }
            else return RedirectToAction(nameof(EmptyCart));
        }

        public async Task<IActionResult> EmptyCart()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            ViewData["ListOfCategories"] = db.Categories.ToList();
            ViewData["Images"] = await db.FilesPaths.AsNoTracking().ToListAsync();
            ViewData["WarehouseNoms"] = await db.WarehouseNoms.AsNoTracking().ToListAsync();
            var nomenclatures = await db.Nomenclatures.AsNoTracking().ToListAsync();
            return View(nomenclatures);
        }

        public async Task<IActionResult> IndexCat(int id)
        {
            ViewData["ListOfCategories"] = db.Categories.ToList();
            ViewData["Images"] = await db.FilesPaths.AsNoTracking().ToListAsync();
            ViewData["WarehouseNoms"] = await db.WarehouseNoms.AsNoTracking().ToListAsync();
            var nomenclatures = await db.Nomenclatures.Where(n=>n.CategoryID.Equals(id)).AsNoTracking().ToListAsync();
            return View("Index",nomenclatures);
        }

        public async Task<IActionResult> SaleDetails(int? id)
        {
            if (id == null || db.Nomenclatures == null)
            {
                return NotFound();
            }

            var nomenclature = await db.Nomenclatures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nomenclature == null)
            {
                return NotFound();
            }
            ViewData["Images"] = await db.FilesPaths.Where(f => f.NomID.Equals(id)).ToListAsync();
            try
            {
                ViewData["NomPrice"] = db.WarehouseNoms.Where(f => f.NomID.Equals(id)).ToList().Last().NomPrice;
            }
            catch (Exception)
            {
                ViewData["NomPrice"] = "NaN";
            }
            return View(nomenclature);
        }

        public IActionResult About()
        {
            ViewData["ListOfCategories"] = db.Categories.ToList();
            return View();
        }

        public IActionResult Login()
        {
            ViewData["ListOfCategories"] = db.Categories.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(IFormCollection fcol)
        {
            ViewData["ListOfCategories"] = db.Categories.ToList();
            User? user = null;
            try
            {
                user = await db.Users.AsNoTracking().FirstAsync(u => (u.Name.Equals(fcol["Name"]) || u.Email.Equals(fcol["Name"])) && u.Password.Equals(fcol["Password"]));
            }
            catch (Exception)
            {
            }
            if (user == null)
            {
                return View("Registration");
            }
            var userRole = await db.Roles.FindAsync(user.RoleID);
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole!.Name)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration([Bind] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User tmp = await db.Users.AsNoTracking().FirstAsync(u => u.Name.Equals(user.Name) || u.Email.Equals(user.Email));
                    if (tmp != null)
                    {
                        return View(user);
                    }
                }
                catch (Exception)
                {
                    db.Add(user);
                    await db.SaveChangesAsync();
                    var userRole = await db.Roles.FindAsync(user.RoleID);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole!.Name)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }

        public IActionResult Privacy()
        {
            ViewData["ListOfCategories"] = db.Categories.ToList();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData["ListOfCategories"] = db.Categories.ToList();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}