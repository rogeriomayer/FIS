using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMC.WebSite.FIS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FMC.WebSite.FIS.Controllers
{
    [Route("Agendar-Ligacao")]
    public class AgendarLigacaoController : Controller
    {
        // GET: AgendarLigacao
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Agendar")]
        public JsonResult Agendar(AgendarLigacao agendar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Utils.SendMail.ContactMail(agendar);
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
    }
}