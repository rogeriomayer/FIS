using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FMC.WebSite.FIS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FMC.WebSite.FIS.Controllers
{
    [Route("Contato")]
    public class ContatoController : Controller
    {
        // GET: Contato
        public IActionResult Index()
        {
            return View();
        }


        // POST: Contato/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}