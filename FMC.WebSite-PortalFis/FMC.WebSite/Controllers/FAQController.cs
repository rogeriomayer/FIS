﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FMC.WebSite.FIS.Controllers
{
    [Route("Faq")]
    public class FAQController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}