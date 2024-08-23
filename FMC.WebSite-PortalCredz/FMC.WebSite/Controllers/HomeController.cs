using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMC.WebSite.FIS.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using FMC.WebSite.FIS.Utils;
using FMC.WebSite.FIS.Utils.API.Ibi;
using System.Text.RegularExpressions;

namespace FMC.WebSite.FIS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string _key;
        private CacheSession cache;
        private HttpContextAccessor context;

        public HomeController(IMemoryCache _cache, IHttpContextAccessor _contextAccessor)
        {
            this._cache = _cache;
            this._contextAccessor = _contextAccessor;

            if (_contextAccessor.HttpContext.Session.GetString("_sID") != _contextAccessor.HttpContext.Session.Id)
            {
                _contextAccessor.HttpContext.Session.SetString("_sID", _contextAccessor.HttpContext.Session.Id);
                _key = _contextAccessor.HttpContext.Session.Id;
                //cache = new CacheSession(_cache, _key);
                cache = new CacheSession(_contextAccessor, _key);

            }
            else
            {
                _key = _contextAccessor.HttpContext.Session.GetString("_sID");
                cache = new CacheSession(_contextAccessor, _key);
            }
        }

        public IActionResult Index(string id, string d, string u, string cpf)
        {
            string routeName = ControllerContext.RouteData.Routers[1].ToString();
            string buckt = d;

            if (!string.IsNullOrEmpty(cpf))
            {
                string CPF = Regex.Replace(cpf, @"\D", "");
                if (!string.IsNullOrEmpty(CPF))
                {
                    try
                    {
                        ConsultaCpfCnpj _model = new ConsultaCpfCnpj { CpfCnpj = CPF };
                        cache.AddCache("model", _model);
                        ValidarAcesso(_model, "Y");
                        return RedirectToAction("Validacao", "ConsultaCpfCnpj");
                    }
                    catch { }
                }
            }


            if (_contextAccessor.HttpContext.Session.GetString("portal") == null || routeName.Contains("negocie"))
            {
                if (!string.IsNullOrEmpty(id))
                    _contextAccessor.HttpContext.Session.SetString("portal", id);
                else if (!string.IsNullOrEmpty(d))
                    _contextAccessor.HttpContext.Session.SetString("portal", d);
            }

            IList<object> list = new List<object> { new ConsultaCpfCnpj(), routeName };
            return View(list);
        }


        private void ValidarAcesso(ConsultaCpfCnpj _model, string redirect = "")
        {
            try
            {
                cache.Remove(new List<string> {
                        "model",
                        "simulaParcelamento" ,
                        "parcelamento",
                        "produto",
                        "billet",
                        "boleto",
                        "EnderecoAtualizado",
                        "Agreement",
                        "Address",
                        "Product",
                        "Autenticated",
                        "TentativaValidacao",
                        "Validacao"
                    });
                cache.AddCache("model", _model);

                var navigation = new Navigation()
                {
                    Cpf = Regex.Replace(_model.CpfCnpj, @"\D", ""),
                    FlNegotiateNow = true,
                    DtInsert = DateTime.Now,
                    DsOrigem = _contextAccessor.HttpContext.Session.GetString("portal") == null ? "/" : _contextAccessor.HttpContext.Session.GetString("portal"),
                    Redirect = redirect
                };
                Navigation wsNavigation = AfinzAPI.SetNavigation(navigation);

                cache.AddCache("Navigation", wsNavigation);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPost]
        public IActionResult Index(ConsultaCpfCnpj _model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    ValidarAcesso(_model);
                    return RedirectToAction("Validacao", "ConsultaCpfCnpj");
                }
                catch (Exception e)
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
