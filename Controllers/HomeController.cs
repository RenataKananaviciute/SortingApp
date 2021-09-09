using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SortingApp.Models;
using System.IO;

namespace SortingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public string filepath = "";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Sorting()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SortNumbers()
        {
            NumbersLineModel cd = new NumbersLineModel();
            
            cd.Line = HttpContext.Request.Form["Text1"].ToString();
            if ((cd.Line == "")||(cd.Line is null))
            {
                ViewBag.Message = "There is no numbers line";
                return View("Sorting");
            }
            int[] bublenumberline = NumberLine.GetNumbersArray(cd.Line);
            int[] selectnumberline = bublenumberline;

            ViewBag.Btime=NumberLine.BubleSort(bublenumberline);
            ViewBag.Atime=NumberLine.SelectionSort(selectnumberline);

            cd.BubbleLine = bublenumberline.Aggregate(" ", (s, i) => s + " " + i.ToString());
            cd.SelectionLine = selectnumberline.Aggregate(" ", (s, i) => s + " " + i.ToString());

            cd.Filename = NumberLine.WritetoFile(cd.Line, cd.BubbleLine, cd.SelectionLine);

            ViewBag.Line = cd.Line;
            ViewBag.BubbleLine = cd.BubbleLine;
            ViewBag.SelectionLine = cd.SelectionLine;
            ViewBag.Filename = cd.Filename;
            return View("Sorting");
        }
        [HttpPost]
        public IActionResult GetFromFile()
        {
            string filepath= HttpContext.Request.Form["Filepath"].ToString();
            if(System.IO.File.Exists(filepath) == false){
                ViewBag.Message = "There is no file saved with sorting result";
                return View("Sorting");
            }

                int counter = 3;
            string line = "";

            System.IO.StreamReader file = new System.IO.StreamReader(filepath);
            while ((line = file.ReadLine()) != null)
            {
                if(counter == 3)
                {
                    ViewBag.Line = line;
                }
                if (counter == 2)
                {
                    ViewBag.BubbleLine = line;
                }
                if (counter == 1)
                {
                    ViewBag.SelectionLine = line;
                }
                counter--;
            }

            file.Close();
            ViewBag.Filename = filepath;
            return View("Sorting");
        }
        [HttpPost]
        public IActionResult DeleteFile()
        {
            string filepath = HttpContext.Request.Form["Filepath"].ToString();
            if (System.IO.File.Exists(filepath) == false)
            {
                ViewBag.Message = "File not found";
                return View("Sorting");
            }
            System.IO.File.Delete(filepath);
            ViewBag.Message = "File was deleted";
            return View("Sorting");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
