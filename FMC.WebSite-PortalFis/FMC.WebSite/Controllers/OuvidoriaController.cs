using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMC.WebSite.FIS.Models;
using FMC.WebSite.FIS.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FMC.WebSite.FIS.Controllers
{
    [Route("Ouvidoria")]
    public class OuvidoriaController : Controller
    {
        private readonly IMemoryCache _cache;
        private IHttpContextAccessor _contextAccessor;
        private readonly string _key;
        private CacheSession cache;
        public OuvidoriaController(IMemoryCache _cache, IHttpContextAccessor _contextAccessor)
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
            //if (_contextAccessor.HttpContext.Request == null ||
            //    _contextAccessor.HttpContext.Request.Cookies == null ||
            //    _contextAccessor.HttpContext.Request.Cookies.Where(p => p.Key == "NETSESSID")?.FirstOrDefault().Value == null ||
            //    _contextAccessor.HttpContext.Request.Cookies.Where(p => p.Key == "NETSESSID")?.FirstOrDefault().Value != _key)
            //{
            //    _contextAccessor.HttpContext.Response.Cookies.Append("NETSESSID", _key);
            //}
        }

        public IActionResult Index()
        {
            Utils.API.Ibi.Login wsLogin = cache.Get<Utils.API.Ibi.Login>("wsLogin");
            if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login");

            ViewBag.Success = TempData["Success"];
            ViewBag.Error = TempData["Error"];

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Ouvidoria ouvidoria)
        {
            try
            {
                Utils.API.Ibi.Login wsLogin = cache.Get<Utils.API.Ibi.Login>("wsLogin");
                if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin)) return RedirectToAction("Login");

                ouvidoria.DataCadastro = DateTime.Now;
                if (ModelState.IsValid)
                {

                    //URAClient client = new URAClient();
                    Utils.API.Ibi.Ouvidoria obj = new Utils.API.Ibi.Ouvidoria
                    {
                        Nome = ouvidoria.Nome,
                        Email = ouvidoria.Email,
                        Tipo = ouvidoria.Tipo,
                        Mensagem = ouvidoria.Descricao,
                        DtInsert = ouvidoria.DataCadastro
                    };
                    Utils.API.Ibi.Ouvidoria wsOuvidoria = null;// = HttpHelper.POST<Utils.API.Ibi.Ouvidoria>(Utils.API.Ibi.Uri.SetOuvidoria(), obj);
                    if (wsOuvidoria != null && wsOuvidoria.IdOuvidoria > 0)
                    {
                        cache.AddCache("wsOuvidoria", wsOuvidoria);
                        TempData["Success"] = "Ok! Sua mensagem foi registrada com sucesso.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Error"] = "Ops! Não foi possível registrar sua mensagem. Tente Novamente.";
                    }

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Login");
            }
        }

        [Route("Login")]
        public ActionResult Login()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [Route("Relatorio")]
        public ActionResult Relatorio()
        {
            try
            {
                Utils.API.Ibi.Login wsLogin = cache.Get<Utils.API.Ibi.Login>("wsLogin");
                if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin) || wsLogin.TipoLogin != 1) return RedirectToAction("Login");

                ICollection<Utils.API.Ibi.Ouvidoria> wsOuvidoria = null;// HttpHelper.GET<ICollection<Utils.API.Ibi.Ouvidoria>>(Utils.API.Ibi.Uri.GetOuvidoria());
                IList<object> data = new List<object> { wsOuvidoria };
                return View(data);
            }
            catch (Exception)
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        [Route("Detalhar/{id}")]
        public ActionResult Detalhar(string id)
        {
            try
            {
                Utils.API.Ibi.Login wsLogin = cache.Get<Utils.API.Ibi.Login>("wsLogin");
                if (wsLogin == null || string.IsNullOrEmpty(wsLogin.UserLogin) || wsLogin.TipoLogin != 1) return RedirectToAction("Login");

                Utils.API.Ibi.Ouvidoria wsOuvidoria = null;// HttpHelper.GET<Utils.API.Ibi.Ouvidoria>(Utils.API.Ibi.Uri.GetOuvidoriaById(id));
                wsOuvidoria.Status = 1;
                wsOuvidoria.DtVisualized = DateTime.Now;

                //var a = HttpHelper.POST<Utils.API.Ibi.Ouvidoria>(Utils.API.Ibi.Uri.SetOuvidoriaStatus(), wsOuvidoria);

                IList<object> data = new List<object> { wsOuvidoria };
                return View(data);
            }
            catch (Exception)
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginOuvidoria login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utils.API.Ibi.Login wsLogin = null;// HttpHelper.GET<Utils.API.Ibi.Login>(Utils.API.Ibi.Uri.GetWebSiteLogin(login.User, login.Password));
                    if (wsLogin != null && !string.IsNullOrEmpty(wsLogin.UserLogin))
                        cache.AddCache("wsLogin", wsLogin);
                    else
                    {
                        TempData["Message"] = "Ops! Não foi possível realizar seu login. Tente Novamente.";
                        return RedirectToAction("Login");
                    }
                    if (wsLogin.TipoLogin == 1)
                        return RedirectToAction("Relatorio");
                    else
                        return RedirectToAction("Index");
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

        }

        [HttpPost]
        [Route("Atualizar-Status")]
        public JsonResult AtualizarStatus(string idStatus, string idOuvidoria)
        {
            try
            {
                Utils.API.Ibi.Ouvidoria ouvidoria = null;// HttpHelper.GET<Utils.API.Ibi.Ouvidoria>(Utils.API.Ibi.Uri.GetOuvidoriaById(idOuvidoria));

                if (ouvidoria != null)
                {
                    ouvidoria.Status = Convert.ToInt32(idStatus);
                    if (idStatus == "2")
                    {
                        ouvidoria.DtAnalysis = DateTime.Now;
                    }
                    else if (idStatus == "3")
                    {
                        ouvidoria.DtImportant = DateTime.Now;
                    }
                    else if (idStatus == "4")
                    {
                        ouvidoria.DtClose = DateTime.Now;
                    }
                    //if (HttpHelper.POST<Utils.API.Ibi.Ouvidoria>(Utils.API.Ibi.Uri.SetOuvidoriaStatus(), ouvidoria) != null)
                    //    return Json(true);
                }
                return Json(false);

            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}