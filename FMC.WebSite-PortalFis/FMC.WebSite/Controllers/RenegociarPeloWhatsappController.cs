using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FMC.WebSite.FIS.Controllers
{
    [Route("Renegociar-Pelo-Whatsapp")]
    public class RenegociarPeloWhatsappController : Controller
    {
        // GET: RenegociarPeloWhatsapp
        public IActionResult Index()
        {
            return View();
        }
    }
}