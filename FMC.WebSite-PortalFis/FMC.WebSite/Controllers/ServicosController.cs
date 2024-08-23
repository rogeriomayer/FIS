using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FMC.WebSite.FIS.Controllers
{
    public class ServicosController : Controller
    {
        // GET: Servicos
        public IActionResult Index()
        {
            return View();
        }
    }
}