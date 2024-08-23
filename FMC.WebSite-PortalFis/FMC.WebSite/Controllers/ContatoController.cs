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

        [HttpPost]
        [Route("Fale-Conosco")]
        public JsonResult FaleConosco(Contato contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utils.SendMail.ContactMail(contato);
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception e)
            {
                string erro = e.Message;
                while(e.InnerException != null)
                {
                    e = e.InnerException;
                    erro += Environment.NewLine + e.Message;
                }
                return Json(erro);
            }
        }

    }
}