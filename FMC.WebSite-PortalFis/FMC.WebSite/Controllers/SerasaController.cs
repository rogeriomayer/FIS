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
    [Route("Serasa")]
    public class SerasaController : Controller
    {
        private readonly IMemoryCache _cache;
        private IHttpContextAccessor _contextAccessor;
        private readonly string _key;
        private CacheSession cache;
        readonly IList<string> listStatacorIbi = new List<string> { "1", "2", "5", "9", "K", "I", "F", "G" };
        public SerasaController(IMemoryCache _cache, IHttpContextAccessor _contextAccessor)
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

            ICollection<SerasaNavigation> listaWsNavigation = new Collection<SerasaNavigation>();
            ICollection<SerasaSimulate> listaWsSimulate = new Collection<SerasaSimulate>();
            ICollection<SerasaAgreement> listaWsAgreement = new Collection<SerasaAgreement>();

            /*
            Parallel.Invoke(
                            () => listaWsNavigation = HttpHelper.GET<ICollection<SerasaNavigation>>(Utils.API.Serasa.Uri.GetSerasaNavigationByPeriod(DateTime.Today.AddDays(-1), DateTime.Now)),
                            () => listaWsSimulate = HttpHelper.GET<ICollection<SerasaSimulate>>(Utils.API.Serasa.Uri.GetSerasaSimulateByPeriod(DateTime.Today.AddDays(-1), DateTime.Now)),
                            () => listaWsAgreement = HttpHelper.GET<ICollection<SerasaAgreement>>(Utils.API.Serasa.Uri.GetSerasaAgreementByPeriod(DateTime.Today.AddDays(-1), DateTime.Now))
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
                    Utils.API.Ibi.Login wsLogin = null;// HttpHelper.GET<Login>(Utils.API.Ibi.Uri.GetWebSiteLogin(login.User, login.Password));
                    if (wsLogin != null && !string.IsNullOrEmpty(wsLogin.UserLogin))
                        cache.AddCache("wsLogin", wsLogin);
                    else
                    {
                        TempData["Message"] = "Ops! Não foi possível realizar seu login. Tente Novamente.";
                        return RedirectToAction("Login");
                    }
                    if (wsLogin.TipoLogin == 1)
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

            ICollection<SerasaNavigation> listaWsNavigation = new Collection<SerasaNavigation>();
            DateTime dataInicio, dataFim;
            if (DateTime.TryParse(relatorio.DataInicio, out dataInicio) && DateTime.TryParse(relatorio.DataFim, out dataFim))
            {
                //Parallel.Invoke(() => listaWsNavigation = HttpHelper.GET<ICollection<SerasaNavigation>>(Utils.API.Serasa.Uri.GetSerasaNavigationByPeriod(dataInicio, dataFim)));
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
            ICollection<SerasaNavigation> listaWsNavigation = new Collection<SerasaNavigation>();
            
            //Parallel.Invoke(() => listaWsNavigation = HttpHelper.GET<ICollection<SerasaNavigation>>(Utils.API.Serasa.Uri.GetSerasaNavigationByPeriod(DateTime.Today, DateTime.Now)));

            IList<object> list = new List<object> { new Relatorio(), listaWsNavigation };
            return View(list);
        }

        [Route("Simulacoes")]
        [HttpPost]
        public IActionResult Simulacao(Relatorio relatorio)
        {
            Login wsLogin = cache.Get<Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login");

            ICollection<SerasaSimulate> listaWsSimulate = new Collection<SerasaSimulate>();
            DateTime dataInicio, dataFim;
            if (DateTime.TryParse(relatorio.DataInicio, out dataInicio) && DateTime.TryParse(relatorio.DataFim, out dataFim))
            {
                //Parallel.Invoke(() => listaWsSimulate = HttpHelper.GET<ICollection<SerasaSimulate>>(Utils.API.Serasa.Uri.GetSerasaSimulateByPeriod(dataInicio, dataFim)));
            }
            else
            {
                ViewBag.Message = "Ops! Não foi possível realizar a busca com os dados informados. Tente novamente.";
            }

            IList<object> list = new List<object> { new Relatorio(), listaWsSimulate };
            return View(list);
        }

        [Route("Simulacoes")]
        public IActionResult Simulacao()
        {
            Login wsLogin = cache.Get<Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login", "Relatorio");
            ICollection<SerasaSimulate> listaWsSimulate = new Collection<SerasaSimulate>();
            
            //Parallel.Invoke(() => listaWsSimulate = HttpHelper.GET<ICollection<SerasaSimulate>>(Utils.API.Serasa.Uri.GetSerasaSimulateByPeriod(DateTime.Today, DateTime.Now)));

            IList<object> list = new List<object> { new Relatorio(), listaWsSimulate };
            return View(list);
        }

        [Route("Acordos")]
        [HttpPost]
        public IActionResult Acordo(Relatorio relatorio)
        {
            Login wsLogin = cache.Get<Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login", "Relatorio");

            ICollection<SerasaAgreement> listaWsAgreement = new Collection<SerasaAgreement>();
            DateTime dataInicio, dataFim;
            if (DateTime.TryParse(relatorio.DataInicio, out dataInicio) && DateTime.TryParse(relatorio.DataFim, out dataFim))
            {
               // Parallel.Invoke(() => listaWsAgreement = HttpHelper.GET<ICollection<SerasaAgreement>>(Utils.API.Serasa.Uri.GetSerasaAgreementByPeriod(dataInicio, dataFim)));
            }
            else
            {
                ViewBag.Message = "Ops! Não foi possível realizar a busca com os dados informados. Tente novamente.";
            }

            //if(relatorio.CPF)

            IList<object> list = new List<object> { new Relatorio(), listaWsAgreement };
            return View(list);
        }

        [Route("Acordos")]
        public IActionResult Acordo()
        {
            Login wsLogin = cache.Get<Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login", "Relatorio");
            ICollection<SerasaAgreement> listaWsAgreement = new Collection<SerasaAgreement>();
           
            //Parallel.Invoke(() => listaWsAgreement = HttpHelper.GET<ICollection<SerasaAgreement>>(Utils.API.Serasa.Uri.GetSerasaAgreementByPeriod(DateTime.Today, DateTime.Now)));

            IList<object> list = new List<object> { new Relatorio(), listaWsAgreement };
            return View(list);
        }


        [Route("Nao-Registrado")]
        public IActionResult NaoRegistrado()
        {
            Login wsLogin = cache.Get<Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login", "Relatorio");
            ICollection<SerasaAgreement> listaWsAgreement = new Collection<SerasaAgreement>();
            ICollection<SerasaAgreement> listWsAgreementNotRegistered = new Collection<SerasaAgreement>();
            //Parallel.Invoke(() => listaWsAgreement = HttpHelper.GET<ICollection<SerasaAgreement>>(Utils.API.Serasa.Uri.GetSerasaAgreementByPeriod(DateTime.Today, DateTime.Now)));

            Parallel.ForEach(listaWsAgreement, (SerasaAgreement wsAgr) =>
            {
                try
                {
                    /*UR83 ibiUr83 = HttpHelper.GET<UR83>(Utils.API.Ibi.Uri.GetUR83(wsAgr.SerasaProduct.DsProduct));
                    if (!listStatacorIbi.Contains(ibiUr83.StatusAcordo))
                        if (wsAgr != null)
                            listWsAgreementNotRegistered.Add(wsAgr);*/
                }
                catch (Exception ex)
                {
                    if (wsAgr != null)
                        listWsAgreementNotRegistered.Add(wsAgr);
                }
            });

            //foreach (var item in listaWsAgreement)
            //{
            //    UR80 ibiUr80 = HttpHelper.GET<UR80>(Utils.API.Ibi.Uri.GetUR80(item.SerasaProduct.DsProduct));
            //    if (!listStatacorIbi.Contains(ibiUr80.StatusAcordo))
            //    {
            //        listWsAgreementNotRegistered.Add(item);
            //    }
            //}

            IList<object> list = new List<object> { new Relatorio(), listWsAgreementNotRegistered };
            return View(list);
        }
    }
}