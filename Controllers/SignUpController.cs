﻿using Microsoft.AspNetCore.Mvc;

namespace ElibraryManagement.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
