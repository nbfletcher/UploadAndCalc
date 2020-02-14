using UploadAndCalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UploadAndCalc.ViewModels;
using System.Diagnostics;

namespace UploadAndCalc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICalcItemRepository _CalcItemRepository;

        public HomeController(ICalcItemRepository CalcItemRepository)
        {
            _CalcItemRepository = CalcItemRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel{};

            return View(homeViewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Upload()
        {
            return View();
        }

    }
}