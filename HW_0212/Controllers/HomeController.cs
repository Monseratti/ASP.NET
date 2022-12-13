using HW_0212.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace HW_0212.Controllers
{
    public class HomeController : Controller
    {
        List<User> Users { get; set; }
        User CurrentUser { get; set; }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            try
            {
                using (StreamReader sr = new StreamReader("Users.json"))
                {
                    Users = JsonSerializer.Deserialize<List<User>>(sr.ReadToEnd())!;
                }
            }
            catch (Exception)
            {
                Users = new List<User>();
            }
            CurrentUser = new User();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Programm()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(IFormCollection col)
        {
            CurrentUser = new User() { UserName = $"{col["Name"]} {col["LastName"]}", Email = col["Email"], PhoneNumber = col["Phone"], Password = col["Password"] };
            if (Users.Contains(CurrentUser)) return RedirectToAction(nameof(UserList));
            Users.Add(CurrentUser);

            var client = new SmtpClient();

            MailboxAddress From = new MailboxAddress("home.02.12.monseratti@gmail.com","ConferentionNoReply");
            MailboxAddress To = new MailboxAddress("", col["Email"]);

            MimeMessage ms = new MimeMessage();
            ms.From.Add(From);
            ms.To.Add(To);
            ms.Subject = "Thanks for registration!";
            ms.Body = new TextPart("Plain") { Text = "You registration on conferention" };

            //client.Connect("smtp.gmail.com", 587, false);
            //client.Authenticate("home.02.12.monseratti", "123456home8");
            //client.Send(ms);
            //client.Disconnect(true);
            
            using (StreamWriter sw = new StreamWriter("Users.json",false))
            {
                sw.WriteLine(JsonSerializer.Serialize(Users));
            }
            return RedirectToAction(nameof(UserList));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(IFormCollection col)
        {
            var user = Users.Find(o => o.Email.Equals(col["Email"]) && o.Password.Equals(col["Password"]));
            if (user != null)
            {
                CurrentUser = user;
                return RedirectToAction(nameof(UserList));
            }
            else
            {
                return View();
            }
        }

        public IActionResult UserList()
        {
           return View(Users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}