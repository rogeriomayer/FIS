using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMC.WebSite.FIS.Models;
using FMC.WebSite.FIS.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FMC.WebSite.FIS.Controllers
{
    [Route("Trabalhe-Conosco")]
    public class TrabalheConoscoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Cadastrar-Curriculo")]
        public IActionResult CadastrarCurriculo()
        {
            return View();
        }

        [HttpPost]
        [Route("Cadastrar-Curriculo")]
        public IActionResult CadastrarCurriculo(Curriculo curriculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utils.SendMail.CurriculoMail(curriculo);
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

        [HttpGet]
        [Route("Consultar-Cep")]
        public JsonResult ConsultaCep(string cep)
        {
            GoogleMapsApi.EnderecoGoogleMaps endereco = GoogleMapsApi.GetEnderecoByCep(cep);
            return Json(endereco);
        }

    }

}