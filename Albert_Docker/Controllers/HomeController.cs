using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Albert_Docker.Models;
using System.Net;

namespace Albert_Docker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }
        [Route("Lublin")]
        public IActionResult Lublin()
        {
            string temp = new WebClient().DownloadString("http://worldtimeapi.org/api/timezone/Europe/Warsaw.txt");
            string surowiec = getBetween(temp, "utc_datetime:", "+00:00");
            string data = getBetween(surowiec, "2020-", "T")+"X";
            string d2 = getBetween(surowiec, "T", ".");
            string godzina = d2.Substring(0, 5) + "X";
            string minute = getBetween(godzina, ":", "X");
            string hour = godzina.Substring(0, 2);

            Int32.TryParse(hour, out int numValue);
            numValue += 1;
            int end = (numValue % 24);

            string dzien, miesiac;
            miesiac = data.Substring(0, 2);
            dzien = getBetween(data, "-", "X");


            ViewBag.Nazwa = "Lublin";
            ViewBag.Data = " " + dzien + " - " + miesiac;
            if (end > 9) ViewBag.godzina = end.ToString();
            else
            {
                Int32.TryParse(dzien, out int next);
                next++;
                if (next < 10)
                {
                    string wynik = " 0" + next + " - " + miesiac;
                    ViewBag.Data = wynik;
                }
                else
                {
                    string wynik = " " + next + " - " + miesiac;
                    ViewBag.Data = wynik;
                }

                ViewBag.godzina = "0" + end.ToString();
            }
            ViewBag.min = minute;


           
            return PartialView("Views/Lublin.cshtml");
        }


        [Route("Sydney")]
        public IActionResult Sydney()
        {
            string temp = new WebClient().DownloadString("http://worldtimeapi.org/api/timezone/Australia/Sydney");
            string surowiec = getBetween(temp, "utc_datetime", "+00:00");
            string data = getBetween(surowiec, "2020-", "T")+"X";
            string d2 = getBetween(surowiec, "T", ".");

            string godzina = d2.Substring(0, 5) + "X";
            string minute = getBetween(godzina, ":", "X");
            string hour = godzina.Substring(0, 2);

            string dzien, miesiac;
            miesiac = data.Substring(0, 2);
            dzien = getBetween(data, "-", "X");

            Int32.TryParse(hour, out int numValue);
            numValue += 11;
            int end = (numValue % 24);
            ViewBag.Nazwa = "Sydney";
            ViewBag.Data = " " + dzien + " - " + miesiac;
            if (end > 9) ViewBag.godzina = end.ToString();
            else
            {
                Int32.TryParse(dzien, out int next);
                next++;
                if (next < 10)
                {
                    string wynik = " 0" + next + " - " + miesiac;
                    ViewBag.Data = wynik;
                }
                else {
                    string wynik = " " + next + " - " + miesiac;
                    ViewBag.Data = wynik;
                }
              
                ViewBag.godzina = "0" + end.ToString();
            }

            ViewBag.min = minute;
            return PartialView("Views/Sydney.cshtml");
        }
        [Route("NewYork")]
        public IActionResult NewYork()
        {
            string temp = new WebClient().DownloadString("http://worldtimeapi.org/api/timezone/America/New_York");

            string surowiec = getBetween(temp, "utc_datetime", "+00:00");
            string data = getBetween(surowiec, "2020-", "T")+"X";
           

            string d2 = getBetween(surowiec, "T", ".");
            string godzina = d2.Substring(0, 5) + "X";
            string minute = getBetween(godzina, ":", "X");

            string hour = godzina.Substring(0, 2);


           

            string dzien, miesiac;
            miesiac = data.Substring(0, 2);
            dzien = getBetween(data, "-", "X");
            Int32.TryParse(hour, out int hour1);
            Int32.TryParse(hour, out int numValue);
            numValue += 17;
            int end = (numValue % 24);
            ViewBag.Nazwa = "NewYork";
            ViewBag.Data = " " + dzien + " - " + miesiac;
            if(end>hour1)
            {
                Int32.TryParse(dzien, out int next);
                next--;
                if (next < 10)
                {
                    string wynik = " 0" + next + " - " + miesiac;
                    ViewBag.Data = wynik;
                }
                else
                {
                    string wynik = " " + next + " - " + miesiac;
                    ViewBag.Data = wynik;
                }
            }
            if (end > 9)
            {
                ViewBag.godzina = end.ToString();
            }
            else
            {
                
                        
                ViewBag.godzina = "0" + end.ToString();
            }
            ViewBag.min = minute;
            return PartialView("Views/NewYork.cshtml");
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
    }
}
