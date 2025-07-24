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
using Newtonsoft.Json;

namespace FMC.Shortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _cache;
        public IHttpContextAccessor _contextAccessor;
        private readonly string _key;
        private CacheSession cache;
        public HomeController(IMemoryCache _cache, IHttpContextAccessor _contextAccessor)

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

        [Route("/{code?}")]
        public IActionResult Index(string code)
        {
            string ip = "Desconhecido|Desconhecido";
            try
            {
                string navegador = "Desconhecido";
                string sistemaOperacional = "Desconhecido";

                foreach (var header in Request.Headers.ToList())
                {
                    if (header.Key.ToString().Contains("User-Agent"))
                    {

                        var userAgent = header.Value.ToString();

                        // Navegador
                        if (userAgent.Contains("Chrome") && !userAgent.Contains("Edge") && !userAgent.Contains("Edg"))
                            navegador = "Chrome";
                        else if (userAgent.Contains("Firefox"))
                            navegador = "Firefox";
                        else if (userAgent.Contains("Safari") && !userAgent.Contains("Chrome"))
                            navegador = "Safari";
                        else if (userAgent.Contains("Edg") || userAgent.Contains("Edge"))
                            navegador = "Edge";
                        else if (userAgent.Contains("MSIE") || userAgent.Contains("Trident"))
                            navegador = "Internet Explorer";

                        // Sistema Operacional
                        if (userAgent.Contains("Windows NT 10.0"))
                            sistemaOperacional = "Windows 10";
                        else if (userAgent.Contains("Windows NT 6.3"))
                            sistemaOperacional = "Windows 8.1";
                        else if (userAgent.Contains("Windows NT 6.1"))
                            sistemaOperacional = "Windows 7";
                        else if (userAgent.Contains("iPhone") || userAgent.Contains("iPad"))
                            sistemaOperacional = "iOS";
                        else if (userAgent.Contains("Macintosh") || userAgent.Contains("Mac OS X"))
                            sistemaOperacional = "macOS";
                        else if (userAgent.Contains("Android"))
                            sistemaOperacional = "Android";
                        else if (userAgent.Contains("iPhone") || userAgent.Contains("iPad"))
                            sistemaOperacional = "iOS";
                        else if (userAgent.Contains("Linux"))
                            sistemaOperacional = "Linux";
                    }

                    ip = sistemaOperacional + "|" + navegador;

                }
            }
            catch { }
            if (string.IsNullOrEmpty(code))
                code = "-";

            try
            {
                if (code.Contains("robots.txt"))
                    return new RedirectResult("https://negociadorcredz.fmcbrasil.com.br");

                ip += "|" + Request.QueryString.ToString().Replace("?", "");

                Short shortener = HttpHelper.GET<Utils.API.Shortener.Short>(Utils.API.Shortener.Uri.GetByCode(code, string.IsNullOrEmpty(ip) ? "Desconhecido|Desconhecido" : ip.Trim()));
                if (shortener != null && !string.IsNullOrEmpty(shortener.URL))
                {
                    var uri = new System.Uri(shortener.URL);
                    return new RedirectResult(uri.AbsoluteUri);
                }
            }
            catch (Exception ex)
            {
                var log = ex.Message;
                return new RedirectResult("https://negociadorcredz.fmcbrasil.com.br?d=9" + log);
                //IList<object> list = new List<object> { log };
                //return View(list);
            }
            //}
            //else
            //{
            //    return new RedirectResult("https://negociadorcredz.fmcbrasil.com.br/d=d");
            //}
            return View();
        }

        [Route("/shorturl/{url?}")]
        public JsonResult ShortUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                try
                {

                    string shortener = HttpHelper.POST<string>(Utils.API.Shortener.Uri.ShortURL(), url);
                    if (!string.IsNullOrEmpty(shortener))
                    {
                        return Json(shortener);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { });
                }
            }
            return Json(new { });
        }

        //[HttpPost]
        //[Route("/shorturl")]
        //public JsonResult ShortUrl(string url)
        //{
        //    if (!string.IsNullOrEmpty(url))
        //    {
        //        try
        //        {
        //            string shortener = HttpHelper.POST<string>(Utils.API.Shortener.Uri.ShortURL(), url);
        //            if (!string.IsNullOrEmpty(shortener))
        //            {
        //                return Json(shortener);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json(new { });
        //        }
        //    }
        //    return Json(new { });
        //}

        [Route("Login")]
        public ActionResult Login()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //URAClient client = new URAClient();
                    //WebSiteLogin wsLogin = client.GetWebSiteLoginAsync(login.User, login.Password).Result;
                    Utils.API.Shortener.UserLogin userLogin = null;
                    try
                    {
                        userLogin = HttpHelper.POST<Utils.API.Shortener.UserLogin>(Utils.API.Shortener.Uri.Login(), login);
                    }
                    catch
                    {
                        TempData["Message"] = "Ops! Não foi possível realizar seu login. Tente Novamente.";
                        return RedirectToAction("Login", "Home");
                    }
                    //Utils.API.Ibi.WebSiteLogin wsLogin = HttpHelper.GET<WebSiteLogin>(Utils.API.Ibi.Uri.GetWebSiteLogin(login.User, login.Password));
                    //client.Close();
                    if (userLogin != null && !string.IsNullOrEmpty(userLogin.DsUser))
                    {
                        cache.AddCache("userLogin", userLogin);
                        _contextAccessor.HttpContext.Session.SetString("NmUser_" + _contextAccessor.HttpContext.Session.Id, userLogin.DsName);
                        if (userLogin.DtAlterPass == null)
                            return RedirectToAction("AlterarSenha", "Home");

                    }
                    else
                    {
                        TempData["Message"] = "Ops! Não foi possível realizar seu login. Tente Novamente.";
                        return RedirectToAction("Login", "Home");
                    }
                    if (userLogin.IdUserProfile > 0)
                        return RedirectToAction("Index", "Home");
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

        [Route("Sair")]
        public ActionResult Sair()
        {
            cache.Remove("userLogin");
            return RedirectToAction("Login", "Home");
        }
    }
}
