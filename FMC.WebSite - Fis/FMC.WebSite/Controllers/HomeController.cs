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

namespace FMC.Fis.Controllers
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
        public IActionResult Index()
        {
            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login");

            //Parallel.Invoke(
            //                () => listaWsNavigation = HttpHelper.GET<ICollection<WebSiteNavigation>>(Utils.API.Ibi.Uri.GetWebSiteNavigationByPeriod(DateTime.Today.AddDays(-1), DateTime.Now)),
            //                () => listaWsSimulate = HttpHelper.GET<ICollection<WebSiteSimulate>>(Utils.API.Ibi.Uri.GetWebSiteSimulateByPeriod(DateTime.Today.AddDays(-1), DateTime.Now)),
            //                () => listaWsAgreement = HttpHelper.GET<ICollection<WebSiteAgreement>>(Utils.API.Ibi.Uri.GetWebSiteAgreementByPeriod(DateTime.Today.AddDays(-1), DateTime.Now))
            //                );

            //Parallel.ForEach(listaWsAgreement, (WebSiteAgreement wsAgr) => {
            //    wsAgr.WebSiteProduct = HttpHelper.GET<WebSiteProduct>(Utils.API.Ibi.Uri.GetWebSiteProduct(wsAgr.IdWebSiteProduct.ToString()));
            //});
            IList<object> list = new List<object> { };
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
        public ActionResult Login(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //URAClient client = new URAClient();
                    //WebSiteLogin wsLogin = client.GetWebSiteLoginAsync(login.User, login.Password).Result;
                    Utils.API.Fis.UserLogin userLogin = null;
                    try
                    {
                        userLogin = HttpHelper.POST<UserLogin, Login>(Utils.API.Fis.Uri.Login(), login);
                    }
                    catch
                    {
                        TempData["Message"] = "Ops! Não foi possível realizar seu login. Tente Novamente.";
                        return RedirectToAction("Login", "Home");
                    }
                    //Utils.API.Ibi.WebSiteLogin wsLogin = HttpHelper.GET<WebSiteLogin>(Utils.API.Ibi.Uri.GetWebSiteLogin(login.User, login.Password));
                    //client.Close();
                    if (userLogin != null && !string.IsNullOrEmpty(userLogin.DsName))
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
                    if (userLogin.Profile != null && userLogin.Profile.IdUserProfile > 0)
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


        [Route("Alterar-Senha")]
        public ActionResult AlterarSenha()
        {
            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login");

            ViewBag.Message = TempData["Message"];
            IList<object> list = new List<object> { new Login() };
            return View(list);
        }

        [HttpPost]
        [Route("Alterar-Senha")]
        public ActionResult AlterarSenha(Login login)
        {
            try
            {

                UserLogin userLogin = cache.Get<UserLogin>("userLogin");
                if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login");

                if (string.IsNullOrEmpty(login.Password))
                {
                    TempData["Message"] = "Ops! Não foi possível alterar sua senha pois é preciso informar qual será a nova senha.";
                    return RedirectToAction("AlterarSenha", "Home");
                }
                login.User = userLogin.DsName;
                try
                {
                    login.User = userLogin.DsName;
                    bool changed = HttpHelper.POST<bool, Login>(Utils.API.Fis.Uri.ChangePassword(), login);
                }
                catch
                {
                    TempData["Message"] = "Ops! A senha digitada é inválida, ela precisa ter no mínimo uma letra maiúscula, um caracter especial e um número. Tente Novamente.";
                    return RedirectToAction("AlterarSenha", "Home");
                }
                //Utils.API.Ibi.WebSiteLogin wsLogin = HttpHelper.GET<WebSiteLogin>(Utils.API.Ibi.Uri.GetWebSiteLogin(login.User, login.Password));
                //client.Close();
                TempData["Message"] = "Ok! Senha alterada com sucesso. No próximo login, utilize a nova senha informada.";
                return RedirectToAction("AlterarSenha", "Home");

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
