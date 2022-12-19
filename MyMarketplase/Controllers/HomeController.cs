using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMarketplase.Models;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace MyMarketplase.Controllers
{
    public class HomeController : Controller
    {
        MyAppContext db;

        public HomeController(MyAppContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            ViewData["ListOfCategories"] = db.Categories.ToList();
            return View();
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