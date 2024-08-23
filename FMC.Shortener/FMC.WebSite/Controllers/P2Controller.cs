using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMC.Shortener.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using FMC.Shortener.Utils;
using FMC.Shortener.Utils.API.Shortener;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace FMC.Shortener.Controllers
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
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsUser)) return RedirectToAction("Login", "Home");
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
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsUser)) return RedirectToAction("Login", "Home");

            PersonResponse personResponse = new PersonResponse();
            try
            {
                personRequest.Cpf = Regex.Replace(personRequest.Cpf, @"\D", string.Empty);

                IList<object> list = new List<object>();


                //Consulta WebService
                personResponse = HttpHelper.GET<PersonResponse>(Utils.API.Shortener.Uri.Customer(personRequest.Cpf));
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
                    var agreement = HttpHelper.GET<Agreement>(Utils.API.Shortener.Uri.Agreement(card));
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
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsUser)) return RedirectToAction("Login", "Home");

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


        [Route("P2/Gerar-Boleto")]
        public IActionResult GerarBoleto()
        {
            try
            {
                UserLogin userLogin = cache.Get<UserLogin>("userLogin");
                if (userLogin == null || string.IsNullOrEmpty(userLogin.DsUser)) return RedirectToAction("Login", "Home");
                Account account = cache.Get<Account>("Account");
                PersonResponse person = cache.Get<PersonResponse>("PersonResponse");

                if(person == null || account == null)
                {
                    TempData["Message"] = "Ops! Aconteceu algum problema ao selecionar o produto. Ele não foi localizado neste cliente, tente novamente.";
                    return RedirectToAction(nameof(BuscarCliente));
                }

                IList<object> list = new List<object> { new BilletRequest(), person, account, statusAgreement };
                return View(list);
            }catch(Exception ex)
            {
                TempData["Message"] = "Ops! Aconteceu algum problema na tentativa de gerar o boleto. Ele não foi localizado neste cliente, tente novamente.";
                return RedirectToAction(nameof(BuscarCliente));
            }
        }

        
    }
}
