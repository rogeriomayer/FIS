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
using System.Collections.ObjectModel;

namespace FMC.Fis.Controllers
{
    public class P2Controller : Controller
    {
        private readonly IMemoryCache _cache;
        public IHttpContextAccessor _contextAccessor;
        private readonly string _key;
        private CacheSession cache;
        private readonly IList<string> statusAgreement = new List<string> { "1", "2", "5", "9", "K", "I", "F", "G" };

        public P2Controller(IMemoryCache _cache, IHttpContextAccessor _contextAccessor)
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

        [Route("P2/Buscar-Cliente")]
        public IActionResult BuscarCliente()
        {
            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");
            cache.Remove(new List<string> { "BilletResponse", "PersonResponse", "Account" });

            ViewBag.Message = TempData["Message"];

            IList<object> list = new List<object> { new PersonRequest(), new PersonResponse(), statusAgreement };
            return View(list);
        }

        [HttpPost]
        [Route("P2/Buscar-Cliente")]
        public IActionResult BuscarCliente(PersonRequest personRequest)
        {
            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");

            PersonResponse personResponse = new PersonResponse();
            try
            {
                personRequest.Cpf = Regex.Replace(personRequest.Cpf, @"\D", string.Empty);

                IList<object> list = new List<object>();


                //Consulta WebService
                personResponse = HttpHelper.GET<PersonResponse>(Utils.API.Fis.Uri.Customer(personRequest.Cpf));
                cache.AddCache("PersonResponse", personResponse);

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || personResponse == null || personResponse.Cpf == null)
                {
                    ViewBag.Message = "Ops! Não foi possível encontrar nenhuma informação, verifique os dados e tente novamente.";
                    list = new List<object> { new PersonRequest(), personResponse, statusAgreement };
                    return View(list);
                }

                Parallel.ForEach(personResponse.Accounts, (Account account) =>
                {
                    string card = (account.CardNumber.Length > 16 && account.CardNumber.Substring(0, 3) == "000") ? account.CardNumber.Substring(3, 16) : account.CardNumber;
                    var agreement = HttpHelper.GET<Agreement>(Utils.API.Fis.Uri.Agreement(card));
                    account.Agreement = agreement;
                });

                cache.AddCache("PersonResponse", personResponse);
                list = new List<object> { new PersonRequest(), personResponse, statusAgreement };
                return View(list);
                //return RedirectToAction("BoletoConcluido");
            }
            catch
            {
                ViewBag.Message = "Ops! Não foi possível gerar o boleto, aconteceu alguma falha durante o processo. Tente novamente.";
                IList<object> list = new List<object> { new BilletRequest(), personResponse };
                return View(list);
            }

            return RedirectToAction(nameof(GerarBoleto));
        }

        [HttpPost]
        [Route("P2/Produto-Selecionado")]
        public IActionResult ProdutoSelecionado(string card)
        {
            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");

            PersonResponse personResponse = cache.Get<PersonResponse>("PersonResponse");
            if (personResponse != null && personResponse.Accounts.Count > 0)
            {
                Account account = personResponse.Accounts.Where(p => p.CardNumber.Contains(card)).FirstOrDefault();
                if (account == null)
                {
                    TempData["Message"] = "Ops! Aconteceu algum problema ao selecionar o produto. Ele não foi localizado neste cliente, tente novamente.";
                    return RedirectToAction(nameof(BuscarCliente));
                }

                cache.AddCache("Account", account);
                return RedirectToAction(nameof(GerarBoleto));
            }

            TempData["Message"] = "Ops! Aconteceu algum problema ao selecionar o produto. Ele não foi localizado neste cliente, tente novamente.";
            return RedirectToAction(nameof(BuscarCliente));

        }


        [Route("P2/Gerar-Boleto-Old")]
        public IActionResult GerarBoletoOld()
        {
            try
            {
                UserLogin userLogin = cache.Get<UserLogin>("userLogin");
                if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");
                Account account = cache.Get<Account>("Account");
                PersonResponse person = cache.Get<PersonResponse>("PersonResponse");

                if (person == null || account == null)
                {
                    TempData["Message"] = "Ops! Aconteceu algum problema ao selecionar o produto. Ele não foi localizado neste cliente, tente novamente.";
                    return RedirectToAction(nameof(BuscarCliente));
                }

                IList<object> list = new List<object> { new BilletRequest(), person, account, statusAgreement };
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Ops! Aconteceu algum problema na tentativa de gerar o boleto. Ele não foi localizado neste cliente, tente novamente.";
                return RedirectToAction(nameof(BuscarCliente));
            }
        }

        [Route("P2/Gerar-Boleto")]
        public IActionResult GerarBoleto()
        {
            try
            {
                UserLogin userLogin = cache.Get<UserLogin>("userLogin");
                if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");

                IList<object> list = new List<object> { new BilletRequest() };
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Ops! Aconteceu algum problema na tentativa de gerar o boleto. Ele não foi localizado neste cliente, tente novamente.";
                return RedirectToAction(nameof(BuscarCliente));
            }
        }

        [HttpPost]
        [Route("P2/Gerar-Boleto-Old")]
        public IActionResult GerarBoletoOld(BilletRequest billetRequest)
        {
            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");

            Account account = cache.Get<Account>("Account");
            PersonResponse person = cache.Get<PersonResponse>("PersonResponse");

            var billet = new BilletRequest
            {
                CPF = person.Cpf,
                Account = account.AccountNumber,
                Address = billetRequest.Address,
                CEP = Regex.Replace(billetRequest.CEP, @"\D", string.Empty),
                City = billetRequest.City,
                Complement = string.IsNullOrEmpty(billetRequest.Complement) ? "" : billetRequest.Complement,
                Date = billetRequest.Date,
                District = billetRequest.District,
                Logo = account.LOGO.ToString().PadLeft(3, '0'),
                Name = person.Name,
                Number = billetRequest.Number,
                UF = billetRequest.UF,
                Value = billetRequest.Value
            };

            BilletResponse billetResponse = null;
            try
            {
                billetResponse = HttpHelper.POST<BilletResponse, BilletRequest>(Utils.API.Fis.Uri.BilletP2(), billet);
                cache.AddCache("BilletResponse", billetResponse);

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || billetResponse == null || billetResponse.PDF == null)
                {
                    ViewBag.Message = "Ops! Não foi possível gerar o boleto, verifique os dados e tente novamente.";
                    IList<object> list = new List<object> { new BilletRequest(), person, account, statusAgreement };
                    return View(list);
                }

                return RedirectToAction("BoletoConcluido");
            }
            catch
            {
                ViewBag.Message = "Ops! Não foi possível gerar o boleto, aconteceu alguma falha durante o processo. Tente novamente.";
                IList<object> list = new List<object> { new BilletRequest(), person, account };
                return View(list);
            }

            return RedirectToAction(nameof(GerarBoleto));
        }

        [HttpPost]
        [Route("P2/Gerar-Boleto")]
        public IActionResult GerarBoleto(BilletRequest billetRequest)
        {
            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");

            Account account = cache.Get<Account>("Account");
            PersonResponse person = cache.Get<PersonResponse>("PersonResponse");

            var billet = new BilletRequest
            {
                CPF = Regex.Replace(billetRequest.CPF, @"\D", string.Empty),
                Account = Regex.Replace(billetRequest.Account, @"\D", string.Empty),
                Address = billetRequest.Address,
                CEP = Regex.Replace(billetRequest.CEP, @"\D", string.Empty),
                City = billetRequest.City,
                Complement = string.IsNullOrEmpty(billetRequest.Complement) ? "" : billetRequest.Complement,
                Date = billetRequest.Date,
                District = billetRequest.District,
                Logo = "",
                Name = billetRequest.Name,
                Number = billetRequest.Number,
                UF = billetRequest.UF,
                Value = billetRequest.Value
            };

            BilletResponse billetResponse = null;
            try
            {
                billetResponse = HttpHelper.POST<BilletResponse, BilletRequest>(Utils.API.Fis.Uri.BilletP2(), billet);
                cache.AddCache("BilletResponse", billetResponse);

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || billetResponse == null || billetResponse.PDF == null)
                {
                    ViewBag.Message = "Ops! Não foi possível gerar o boleto, verifique os dados e tente novamente.";
                    IList<object> list = new List<object> { new BilletRequest(), person, account, statusAgreement };
                    return View(list);
                }

                return RedirectToAction("BoletoConcluido");
            }
            catch
            {
                ViewBag.Message = "Ops! Não foi possível gerar o boleto, aconteceu alguma falha durante o processo. Tente novamente.";
                IList<object> list = new List<object> { new BilletRequest(), person, account };
                return View(list);
            }

            return RedirectToAction(nameof(GerarBoleto));
        }

        [Route("P2/Boleto-Concluido")]
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

        [Route("P2/Imprimir-Boleto")]
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

        //public IActionResult GerarBoleto()
        //{
        //    UserLogin userLogin = cache.Get<UserLogin>("userLogin");
        //    if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login");

        //    //Parallel.Invoke(
        //    //                () => listaWsNavigation = HttpHelper.GET<ICollection<WebSiteNavigation>>(Utils.API.Ibi.Uri.GetWebSiteNavigationByPeriod(DateTime.Today.AddDays(-1), DateTime.Now)),
        //    //                () => listaWsSimulate = HttpHelper.GET<ICollection<WebSiteSimulate>>(Utils.API.Ibi.Uri.GetWebSiteSimulateByPeriod(DateTime.Today.AddDays(-1), DateTime.Now)),
        //    //                () => listaWsAgreement = HttpHelper.GET<ICollection<WebSiteAgreement>>(Utils.API.Ibi.Uri.GetWebSiteAgreementByPeriod(DateTime.Today.AddDays(-1), DateTime.Now))
        //    //                );

        //    //Parallel.ForEach(listaWsAgreement, (WebSiteAgreement wsAgr) => {
        //    //    wsAgr.WebSiteProduct = HttpHelper.GET<WebSiteProduct>(Utils.API.Ibi.Uri.GetWebSiteProduct(wsAgr.IdWebSiteProduct.ToString()));
        //    //});
        //    IList<object> list = new List<object> { };
        //    return View(list);
        //}

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
    }
}
