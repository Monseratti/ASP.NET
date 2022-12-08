using CW_0112.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CW_0112.Controllers
{
    public class CarController : Controller
    {
        List<Car> Cars { get; set; }
        
        public CarController()
        {
            try
            {
                using (StreamReader sr = new StreamReader("cars.json"))
                {
                    Cars = JsonSerializer.Deserialize<List<Car>>(sr.ReadToEnd())!;
                }
            }
            catch (Exception)
            {
                Cars = new List<Car>();
            }
        }        
        
        
        // GET: CarController
        public ActionResult Index()
        {
            return View(Cars);
        }

        // GET: CarController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarController/Create
        public ActionResult Create()
        {
            return View(new Car());
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Cars.Add(new Car(int.Parse(collection["Id"]),collection["Name"], collection["Color"], collection["Manufacturer"], int.Parse(collection["Year"]), int.Parse(collection["EngineV"])));
                using (StreamWriter sw = new StreamWriter("cars.json",false))
                {
                    sw.WriteLine(JsonSerializer.Serialize(Cars));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Cars.Where(o=>o.Id.Equals(id)).First());
        }

        // POST: CarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Cars.Where(o => o.Id.Equals(id)).First().Name = collection["Name"];
                Cars.Where(o => o.Id.Equals(id)).First().Color = collection["Color"];
                Cars.Where(o => o.Id.Equals(id)).First().Manufacturer = collection["Manufacturer"];
                Cars.Where(o => o.Id.Equals(id)).First().Year = int.Parse(collection["Year"]);
                Cars.Where(o => o.Id.Equals(id)).First().EngineV = int.Parse(collection["EngineV"]);
                using (StreamWriter sw = new StreamWriter("cars.json", false))
                {
                    sw.WriteLine(JsonSerializer.Serialize(Cars));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Cars.Remove(Cars.Where(o => o.Id.Equals(id)).First());
                using (StreamWriter sw = new StreamWriter("cars.json", false))
                {
                    sw.WriteLine(JsonSerializer.Serialize(Cars));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
