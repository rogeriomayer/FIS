using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FMC.WebSite.FIS.Models;
using FMC.WebSite.FIS.Utils;
using FMC.WebSite.FIS.Utils.API.Ibi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FMC.WebSite.FIS.Controllers
{
    [Route("Relatorio")]
    public class RelatorioController : Controller
    {
        private readonly IMemoryCache _cache;
        private IHttpContextAccessor _contextAccessor;
        private readonly string _key;
        private CacheSession cache;
        public RelatorioController(IMemoryCache _cache, IHttpContextAccessor _contextAccessor)
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
        public IActionResult Index()
        {
            Login wsLogin = cache.Get<Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login");

            ICollection<Navigation> listaWsNavigation = new Collection<Navigation>();
            ICollection<Simulate> listaWsSimulate = new Collection<Simulate>();
            ICollection<Agreement> listaWsAgreement = new Collection<Agreement>();
            /*
            Parallel.Invoke(
                            () => listaWsNavigation = HttpHelper.GET<ICollection<Navigation>>(Utils.API.Ibi.Uri.GetNavigationByPeriod(DateTime.Today.AddDays(-1), DateTime.Now)),
                            () => listaWsSimulate = HttpHelper.GET<ICollection<Simulate>>(Utils.API.Ibi.Uri.GetSimulateByPeriod(DateTime.Today.AddDays(-1), DateTime.Now)),
                            () => listaWsAgreement = HttpHelper.GET<ICollection<Agreement>>(Utils.API.Ibi.Uri.GetAgreementByPeriod(DateTime.Today.AddDays(-1), DateTime.Now))
                            );
            */

            IList<object> list = new List<object> { listaWsNavigation, listaWsSimulate, listaWsAgreement };
            return View(list);
        }

        [Route("Login")]
        public ActionResult Login()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginRelatorio login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    /* Utils.API.Ibi.Login wsLogin = HttpHelper.GET<Login>(Utils.API.Ibi.Uri.GetWebSiteLogin(login.User, login.Password));
                    if (wsLogin != null && !string.IsNullOrEmpty(wsLogin.UserLogin))
                        cache.AddCache("wsLogin", wsLogin);
                    else
                    {
                        TempData["Message"] = "Ops! Não foi possível realizar seu login. Tente Novamente.";
                        return RedirectToAction("Login");
                    }
                    if (wsLogin.TipoLogin == 1)*/
                    return RedirectToAction("Index", "Relatorio");
                }
                else
                {
                    TempData["Message"] = "Ops! Não foi possível realizar seu login. Tente Novamente.";
                    return RedirectToAction("Login");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return RedirectToAction("Login");
        }


        [Route("Cpf")]
        [HttpPost]
        public IActionResult CPF(Relatorio relatorio)
        {
            Login wsLogin = cache.Get<Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login");

            ICollection<Navigation> listaWsNavigation = new Collection<Navigation>();
            DateTime dataInicio, dataFim;
            if (DateTime.TryParse(relatorio.DataInicio, out dataInicio) && DateTime.TryParse(relatorio.DataFim, out dataFim))
            {
                // Parallel.Invoke(() => listaWsNavigation = HttpHelper.GET<ICollection<Navigation>>(Utils.API.Ibi.Uri.GetNavigationByPeriod(dataInicio, dataFim)));
            }
            else
            {
                ViewBag.Message = "Ops! Não foi possível realizar a busca com os dados informados. Tente novamente.";
            }

            //if(relatorio.CPF)

            IList<object> list = new List<object> { new Relatorio(), listaWsNavigation };
            return View(list);
        }

        [Route("Cpf")]
        public IActionResult CPF()
        {
            Login wsLogin = cache.Get<Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login");
            ICollection<Navigation> listaWsNavigation = new Collection<Navigation>();
            //Parallel.Invoke(() => listaWsNavigation = HttpHelper.GET<ICollection<Navigation>>(Utils.API.Ibi.Uri.GetNavigationByPeriod(DateTime.Today, DateTime.Now)));

            IList<object> list = new List<object> { new Relatorio(), listaWsNavigation };
            return View(list);
        }

        [Route("Simulacoes-P2")]
        [HttpPost]
        public IActionResult Simulacao(Relatorio relatorio)
        {
            Login wsLogin = cache.Get<Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login");

            ICollection<Simulate> listaWsSimulate = new Collection<Simulate>();
            DateTime dataInicio, dataFim;
            if (DateTime.TryParse(relatorio.DataInicio, out dataInicio) && DateTime.TryParse(relatorio.DataFim, out dataFim))
            {
                // Parallel.Invoke(() => listaWsSimulate = HttpHelper.GET<ICollection<Simulate>>(Utils.API.Ibi.Uri.GetSimulateByPeriod(dataInicio, dataFim)));
            }
            else
            {
                ViewBag.Message = "Ops! Não foi possível realizar a busca com os dados informados. Tente novamente.";
            }

            IList<object> list = new List<object> { new Relatorio(), listaWsSimulate.Where(p => p.Product.ProductType == "P2").ToList() };
            return View(list);
        }

        [Route("Simulacoes-P2")]
        public IActionResult Simulacao()
        {
            Login wsLogin = cache.Get<Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login");
            ICollection<Simulate> listaWsSimulate = new Collection<Simulate>();
            // Parallel.Invoke(() => listaWsSimulate = HttpHelper.GET<ICollection<Simulate>>(Utils.API.Ibi.Uri.GetSimulateByPeriod(DateTime.Today, DateTime.Now)));

            IList<object> list = new List<object> { new Relatorio(), listaWsSimulate.Where(p => p.Product.ProductType == "P2").ToList() };
            return View(list);
        }

        [Route("Acordos-P2")]
        [HttpPost]
        public IActionResult Acordo(Relatorio relatorio)
        {
            Login wsLogin = cache.Get<Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login");

            ICollection<Agreement> listaWsAgreement = new Collection<Agreement>();
            DateTime dataInicio, dataFim;
            if (DateTime.TryParse(relatorio.DataInicio, out dataInicio) && DateTime.TryParse(relatorio.DataFim, out dataFim))
            {
                //Parallel.Invoke(() => listaWsAgreement = HttpHelper.GET<ICollection<Agreement>>(Utils.API.Ibi.Uri.GetAgreementByPeriod(dataInicio, dataFim)));
            }
            else
            {
                ViewBag.Message = "Ops! Não foi possível realizar a busca com os dados informados. Tente novamente.";
            }

            IList<object> list = new List<object> { new Relatorio(), listaWsAgreement.Where(p => p.Product.ProductType == "P2").ToList() };
            return View(list);
        }

        [Route("Acordos-P2")]
        public IActionResult Acordo()
        {
            Login wsLogin = cache.Get<Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login");
            ICollection<Agreement> listaWsAgreement = new Collection<Agreement>();
            //Parallel.Invoke(() => listaWsAgreement = HttpHelper.GET<ICollection<Agreement>>(Utils.API.Ibi.Uri.GetAgreementByPeriod(DateTime.Today, DateTime.Now)));

            IList<object> list = new List<object> { new Relatorio(), listaWsAgreement.Where(p => p.Product.ProductType == "P2").ToList() };
            return View(list);
        }

        #region P1
        [Route("Simulacoes-P1")]
        [HttpPost]
        public IActionResult SimulacaoP1(Relatorio relatorio)
        {
            Login wsLogin = cache.Get<Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login");

            ICollection<Simulate> listaWsSimulate = new Collection<Simulate>();
            DateTime dataInicio, dataFim;
            if (DateTime.TryParse(relatorio.DataInicio, out dataInicio) && DateTime.TryParse(relatorio.DataFim, out dataFim))
            {
                // Parallel.Invoke(() => listaWsSimulate = HttpHelper.GET<ICollection<Simulate>>(Utils.API.Ibi.Uri.GetSimulateByPeriod(dataInicio, dataFim)));
            }
            else
            {
                ViewBag.Message = "Ops! Não foi possível realizar a busca com os dados informados. Tente novamente.";
            }

            IList<object> list = new List<object> { new Relatorio(), listaWsSimulate.Where(p => p.Product.ProductType == "P1").ToList() };
            return View(list);
        }

        [Route("Simulacoes-P1")]
        public IActionResult SimulacaoP1()
        {
            Login wsLogin = cache.Get<Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login");
            ICollection<Simulate> listaWsSimulate = new Collection<Simulate>();
            //Parallel.Invoke(() => listaWsSimulate = HttpHelper.GET<ICollection<Simulate>>(Utils.API.Ibi.Uri.GetSimulateByPeriod(DateTime.Today, DateTime.Now)));

            IList<object> list = new List<object> { new Relatorio(), listaWsSimulate.Where(p => p.Product.ProductType == "P1").ToList() };
            return View(list);
        }

        #endregion

    }
}