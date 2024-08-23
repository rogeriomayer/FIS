using Microsoft.AspNetCore.Mvc;
using FMC.WebSite.FIS.Models;
using FMC.WebSite.FIS.Utils;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Threading.Tasks;
using FMC.WebSite.FIS.Utils.API.Ibi;
using ServiceStack;

namespace FMC.WebSite.FIS.Controllers
{

    public class NegociarAgoraController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string _key;
        private CacheSession cache;
        private HttpContextAccessor context;

        public NegociarAgoraController(IMemoryCache _cache, IHttpContextAccessor _contextAccessor)
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



            //if (_contextAccessor.HttpContext.Request == null || 
            //    _contextAccessor.HttpContext.Request.Cookies == null || 
            //    _contextAccessor.HttpContext.Request.Cookies.Where(p => p.Key == "NETSESSID")?.FirstOrDefault().Value == null || 
            //    _contextAccessor.HttpContext.Request.Cookies.Where(p => p.Key == "NETSESSID")?.FirstOrDefault().Value != _key)
            //{
            //    _contextAccessor.HttpContext.Response.Cookies.Append("NETSESSID", _key);
            //}
        }
        public IActionResult Index(string d, string u)
        {
            string routeName = ControllerContext.RouteData.Routers[1].ToString();
            string buckt = d;
            string bradescoUDI = u;

            if (_contextAccessor.HttpContext.Session.GetString("buckt") == null)
            {
                if (buckt != null)
                    _contextAccessor.HttpContext.Session.SetString("buckt", buckt);
            }

            if (_contextAccessor.HttpContext.Session.GetString("portal") == null ||
                routeName.Contains("negocie") ||
                routeName.Contains("bradesco") ||
                routeName.Contains("bradescard") ||
                routeName.Contains("cartoes-bradescard") ||
                routeName.Contains("simplic") ||
                routeName.Contains("pepsico") ||
                routeName.Contains("multiloja") ||
                routeName.Contains("moneyman") ||
                routeName.Contains("credjet")
               )
            {
                _contextAccessor.HttpContext.Session.SetString("portal", routeName);
                if (!string.IsNullOrEmpty(bradescoUDI))
                    _contextAccessor.HttpContext.Session.SetString("BradescoUberlandia", u);
            }

            IList<object> list = new List<object> { new ConsultaCpfCnpj(), routeName };
            return View(list);
        }

        // POST: Contato/Create
        [HttpPost]
        public IActionResult Index(ConsultaCpfCnpj _model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    cache.Remove(new List<string> {
                        "model",
                        "simulaParcelamento" ,
                        "parcelamento",
                        "produto",
                        "ibiLogo",
                        "ibiUr80",
                        "ibiUr84",
                        "naccPessoa",
                        "billet",
                        "boleto",
                        "EnderecoAtualizado",
                        "Agreement",
                        "Address",
                        "Product"
                    });
                    cache.AddCache("model", _model);

                    
                    var navigation = new Navigation()
                    {
                        Cpf = Regex.Replace(_model.CpfCnpj, @"\D", ""),
                        FlNegotiateNow = true,
                        DtInsert = DateTime.Now,
                        DsOrigem = _contextAccessor.HttpContext.Session.GetString("portal") == null ? "/" : _contextAccessor.HttpContext.Session.GetString("portal")
                    };
                    
                    Navigation wsNavigation = AfinzAPI.SetNavigation(navigation);

                    cache.AddCache("Navigation", wsNavigation);

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
        //public ActionResult Index(ConsultaCpfCnpj model)-
    }
}