using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMC.Fis.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using FMC.Fis.Utils;
using FMC.Fis.Utils.API.Fis;
using System.IO;
using System.Text.RegularExpressions;

namespace FMC.Fis.Controllers
{
    public class P1Controller : Controller
    {
        private readonly IMemoryCache _cache;
        public IHttpContextAccessor _contextAccessor;
        private readonly string _key;
        private CacheSession cache;
        public P1Controller(IMemoryCache _cache, IHttpContextAccessor _contextAccessor)
        {
            this._cache = _cache;
            this._contextAccessor = _contextAccessor;

            if (_contextAccessor.HttpContext.Session.GetString("_sID") != _contextAccessor.HttpContext.Session.Id)
            {
                _contextAccessor.HttpContext.Session.SetString("_sID", _contextAccessor.HttpContext.Session.Id);
                _key = _contextAccessor.HttpContext.Session.Id;
                cache = new CacheSession(_contextAccessor, _key);
            }
            else
            {
                _key = _contextAccessor.HttpContext.Session.GetString("_sID");
                cache = new CacheSession(_contextAccessor, _key);
            }
        }
        [Route("P1/Gerar-Boleto")]
        public IActionResult GerarBoleto()
        {

            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");
            cache.Remove("BilletResponse");

            var cardP1Response = cache.Get<CardP1Response>("CardP1Response");

            if (cardP1Response == null)
                cardP1Response = new CardP1Response();

            IList<object> list = new List<object> { new BilletRequest(), cardP1Response };
            //IList<object> list = new List<object> { new BilletRequest() };
            return View(list);
        }

        [HttpPost]
        [Route("P1/Gerar-Boleto")]
        public IActionResult GerarBoleto(BilletRequest billetRequest)
        {
            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");

            BilletResponse billetResponse = null;
            try
            {
                /** Tratando máscaras **/
                billetRequest.Account = cache.Get<CardsP1Response>("CardsP1Response").ListaCartoes.Where(p => p.Cartao.nrToken == billetRequest.Account).FirstOrDefault().Cartao.nrContaCartao;

                billetRequest.Account = billetRequest.Account.Length > 16 ? billetRequest.Account.Substring(billetRequest.Account.Length - 16, 16) : billetRequest.Account;


                billetRequest.CEP = Regex.Replace(billetRequest.CEP, @"\D", string.Empty);
                billetRequest.CPF = Regex.Replace(billetRequest.CPF, @"\D", string.Empty);
                billetRequest.CPF = billetRequest.CPF.Length > 11 ? billetRequest.CPF.Substring(billetRequest.CPF.Length - 11, 11) : billetRequest.CPF;
                if (String.IsNullOrEmpty(billetRequest.Complement))
                    billetRequest.Complement = "";

                billetResponse = HttpHelper.POST<BilletResponse, BilletRequest>(Utils.API.Fis.Uri.BilletP1(), billetRequest);
                cache.AddCache("BilletResponse", billetResponse);

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || billetResponse == null || billetResponse.PDF == null)
                {
                    ViewBag.Message = "Ops! Não foi possível gerar o boleto, verifique os dados e tente novamente.";
                    IList<object> list = new List<object> { new BilletRequest(), cache.Get<CardP1Response>("CardP1Response") };
                    return View(list);
                }

                return RedirectToAction("BoletoConcluido");
            }
            catch
            {
                ViewBag.Message = "Ops! Não foi possível gerar o boleto, aconteceu alguma falha durante o processo. Tente novamente.";
                //IList<object> list = new List<object> { new PersonRequest(), personResponse, statusAgreement };
                IList<object> list = new List<object> { new BilletRequest() };
                return View(list);
            }

            return RedirectToAction(nameof(GerarBoleto));
        }

        [Route("P1/Boleto-Concluido")]
        public IActionResult BoletoConcluido()
        {
            try
            {
                UserLogin userLogin = cache.Get<UserLogin>("userLogin");
                if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");

                BilletResponse billet = cache.Get<BilletResponse>("BilletResponse");

                if (billet == null)
                    ViewBag.Message = "Ops! Não existe boleto para impressão ou o tempo para impressão já expirou, tente novamente.";

                IList<object> list = new List<object> { billet };
                return View(list);
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [Route("P1/Imprimir-Boleto")]
        public ActionResult ImprimirBoleto()
        {
            BilletResponse billet = cache.Get<BilletResponse>("BilletResponse");
            string html = string.Empty;
            if (billet != null && billet.PDF != null)
            {
                MemoryStream pdfStream = new MemoryStream();
                pdfStream.Write(billet.PDF, 0, billet.PDF.Length);
                pdfStream.Position = 0;
                return new FileStreamResult(pdfStream, "application/pdf") { FileDownloadName = "BOLETO_BRADESCARD.pdf" };
            }
            else
            {
                return RedirectToAction("GerarBoleto", "P1");
            }
        }

        public JsonResult ConsultaCep(string cep)
        {
            try
            {
                int cepSearch = Convert.ToInt32(cep.Replace("-", ""));
                FMC.Fis.Utils.ViaCEP.Model.ViaCEP address = Util.ByZipCode(cepSearch);
                if (address.ZipCode != null)
                {
                    return Json(address);
                }
                return Json(null);
            }
            catch
            {
                return Json(null);
            }
        }

        //Alteração buscar dados cliente 

        [Route("P1/Buscar-Cliente")]
        public IActionResult BuscarCliente()
        {
            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");
            cache.Remove(new List<string> { "BilletResponse", "CardsP1Response", "Account" });

            ViewBag.Message = TempData["Message"];

            IList<object> list = new List<object> { new PersonRequest(), new CardsP1Response() };
            return View(list);
        }

        [HttpPost]
        [Route("P1/Buscar-Cliente")]
        public IActionResult BuscarCliente(PersonRequest personRequest)
        {
            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");

            //PersonResponse personResponse = new PersonResponse();
            CardsP1Response cardsP1Response = new CardsP1Response();

            try
            {
                personRequest.Cpf = Regex.Replace(personRequest.Cpf, @"\D", string.Empty);

                IList<object> list = new List<object>();


                //Consulta WebService
                cardsP1Response = HttpHelper.GET<CardsP1Response>(Utils.API.Fis.Uri.CardsP1(personRequest.Cpf));

                cache.AddCache("CardsP1Response", cardsP1Response);
                cache.Remove("CardP1Response");

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || cardsP1Response == null || cardsP1Response.chaveRestart == null)
                {
                    ViewBag.Message = "Ops! Não foi possível encontrar nenhuma informação, verifique os dados e tente novamente.";
                    list = new List<object> { new PersonRequest(), cardsP1Response };
                    return View(list);
                }

                cache.AddCache("CardsP1Response", cardsP1Response);
                list = new List<object> { new PersonRequest(), cardsP1Response };
                return View(list);
                //return RedirectToAction("BoletoConcluido");
            }
            catch
            {
                ViewBag.Message = "Ops! Não foi possível gerar o boleto, aconteceu alguma falha durante o processo. Tente novamente.";
                IList<object> list = new List<object> { new BilletRequest(), cardsP1Response };
                return View(list);
            }

            return RedirectToAction(nameof(GerarBoleto));
        }

        [HttpPost]
        [Route("P1/Produto-Selecionado")]
        public IActionResult ProdutoSelecionado(string card)
        {
            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");

            CardP1Response cardP1Response = HttpHelper.POST<CardP1Response, CardP1Request>(Utils.API.Fis.Uri.CardP1(), new CardP1Request() { nrToken = card });

            if (cardP1Response != null)
            {
                cache.AddCache("CardP1Response", cardP1Response);
                return RedirectToAction(nameof(GerarBoleto));
            }
            else
            {
                TempData["Message"] = "Ops! Aconteceu algum problema ao selecionar o produto. Ele não foi localizado neste cliente, tente novamente.";
                return RedirectToAction(nameof(BuscarCliente));
            }
        }
    }
}
