using CW_0112.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CW_0112.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        List<Result> result = new List<Result>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            string json = "";
            using (StreamReader sr = new StreamReader("Result.json"))
            {
                json = sr.ReadToEnd();
            }
            try
            {
                result = JsonSerializer.Deserialize<List<Result>>(json)!;
            }
            catch (Exception)
            {
                
            }
        }

        public IActionResult Index()
        {
            if(result.Count > 0)
            {
                return View(result);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Index(string number, string op, string whole, string numer, string denum)
        {
            Fraction fr1, fr2;
            if(decimal.TryParse(number, out decimal num)  )
                fr1 = new Fraction(num);
            else
                return View();
            if (int.TryParse(numer, out int intNum) && int.TryParse(denum, out int intDenum))
            {
                if (int.TryParse(whole, out int intWhole))
                {
                    fr2 = new Fraction(intNum, intDenum, intWhole);
                }
                else
                {
                    fr2 = new Fraction(intNum, intDenum, intWhole);
                }
                fr2 = fr2.WrightFraction(fr2);
            }
            else
                return View();
            var Result = new Result(fr1, op, fr2, new Fraction());
            switch (op)
            {
                case "+": 
                    Result.Res = fr1.Sum(fr2);
                    break;
                case "-":
                    Result.Res = fr1.Sub(fr2);
                    break;
                case "*":
                    Result.Res = fr1.Multiply(fr2);
                    break;
                case "/":
                    Result.Res = fr1.Divide(fr2);
                    break;
                default: return View();
            }
            result.Add(Result);
            string str = JsonSerializer.Serialize(result);
            using (StreamWriter sw = new StreamWriter("Result.json", false))
            {
                sw.WriteLine(str);
            }
            return View(result);
        }

    }

    public class Fraction
    {
        public int Whole { get; set; }
        public int Num { get; set; }
        public int Denum { get; set; }

        public Fraction()
        {
            Num = 1;
            Denum= 1;
        }

        public Fraction(decimal number)
        {
            var numStr = number.ToString().Split(",");
            Whole = int.Parse(numStr[0]);
            if (numStr.Length > 1)
            {
                Num = int.Parse(numStr[1]);
                Denum = (int)Math.Pow(10, numStr[1].Length);
                int nod = GetNOD(Num, Denum);
                Num /= nod;
                Denum /= nod;
            }
            else
            {
                Num = 0; Denum = 0;
            }
        }

        public Fraction(int num, int denum, int whole = 0)
        {
            Whole = whole;
            int nod = GetNOD(num, denum);
            Num = num/nod;
            Denum = denum/nod;
            if (Num == 0 && Denum == 1)
            {
                Num = Denum = 0;
            }
        }

        public override string ToString()
        {
            return $"{(Whole != 0 ? $"{Whole} whole " : "")}" +
                   $"{(Num != 0 ? $"{Num}/" : "")}" +
                   $"{(Denum != 0 ? $"{Denum}" : "")}";
        }

        public static int GetNOD(int val1, int val2)
        {
            val1 = Math.Abs(val1);
            while ((val1 != 0) && (val2 != 0))
            {
                if (val1 > val2)
                    val1 %= val2;
                else
                    val2 %= val1;
            }
            return Math.Max(val1, val2);
        }

        public Fraction Sum(Fraction fraction)
        {
            Fraction res = new Fraction();
            var fr1 = Non_WrightFraction(this);
            var fr2 = Non_WrightFraction(fraction);
            fr1.Num *= fr2.Denum;
            fr2.Num *=fr1.Denum;
            res.Num = fr1.Num+fr2.Num;
            res.Denum = fr1.Denum*fr2.Denum;
            int nod = GetNOD(res.Num, res.Denum);
            res.Num/= nod;
            res.Denum /= nod;
            return res.WrightFraction(res);
        }

        public Fraction Sub (Fraction fraction)
        {
            Fraction res = new Fraction();
            var fr1 = Non_WrightFraction(this);
            var fr2 = Non_WrightFraction(fraction);
            fr1.Num *= fr2.Denum;
            fr2.Num *= fr1.Denum;
            res.Num = fr1.Num - fr2.Num;
            res.Denum = fr1.Denum * fr2.Denum;
            int nod = GetNOD(res.Num, res.Denum);
            res.Num /= nod;
            res.Denum /= nod;
            return res.WrightFraction(res);
        }

        public Fraction Multiply(Fraction fraction)
        {
            Fraction res = new Fraction();
            var fr1 = Non_WrightFraction(this);
            var fr2 = Non_WrightFraction(fraction);
            res.Num = fr1.Num * fr2.Num;
            res.Denum = fr1.Denum * fr2.Denum;
            int nod = GetNOD(res.Num, res.Denum);
            res.Num /= nod;
            res.Denum /= nod;
            return res.WrightFraction(res);
        }

        public Fraction Divide(Fraction fraction)
        {
            Fraction res = new Fraction();
            var fr1 = Non_WrightFraction(this);
            var tmp = Non_WrightFraction(fraction);
            var fr2 = new Fraction(tmp.Denum, tmp.Num);
            res.Num = fr1.Num * fr2.Num;
            res.Denum = fr1.Denum * fr2.Denum;
            int nod = GetNOD(res.Num, res.Denum);
            res.Num /= nod;
            res.Denum /= nod;
            return res.WrightFraction(res);
        }


        public Fraction Non_WrightFraction(Fraction fr) 
        {
            return new Fraction((fr.Denum * fr.Whole + fr.Num), fr.Denum);
        }

        public Fraction WrightFraction(Fraction fr)
        {
            int whole = (int)(fr.Num / fr.Denum);
            if (whole == 0)
                return fr;
            else return new Fraction(fr.Num-whole*fr.Denum, fr.Denum, whole+fr.Whole);
        }
        public decimal DecimalFraction()
        {
            return decimal.Parse($"{this.Whole + (this.Num!=0?(this.Num*1.0 / this.Denum):0)}");
        }
    }

    public class Result
    {
        public Fraction Fr1 { get; }
        public string Op { get; }
        public Fraction Fr2 { get; }
        public Fraction Res { get; set; }

        public Result(Fraction fr1, string op, Fraction fr2, Fraction res)
        {
            Fr1 = fr1;
            Op = op;
            Fr2 = fr2;
            Res = res;
        }

        public override bool Equals(object? obj)
        {
            return obj is Result other &&
                   EqualityComparer<Fraction>.Default.Equals(Fr1, other.Fr1) &&
                   Op == other.Op &&
                   EqualityComparer<Fraction>.Default.Equals(Fr2, other.Fr2) &&
                   EqualityComparer<Fraction>.Default.Equals(Res, other.Res);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Fr1, Op, Fr2, Res);
        }
        public override string ToString()
        {
            return $"{Fr1}{Op}{Fr2}={Res} or {Res.DecimalFraction()}";
        }
    }

}