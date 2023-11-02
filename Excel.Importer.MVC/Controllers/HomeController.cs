//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Microsoft.AspNetCore.Mvc;

namespace Excel.Importer.MVC.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
