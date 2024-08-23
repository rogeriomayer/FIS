using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FMC.Fis.Models;
using FMC.Fis.Utils;
using FMC.Fis.Utils.API.Fis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FMC.Fis.Controllers
{
    public class CNABController : Controller
    {
        private readonly IMemoryCache _cache;
        public IHttpContextAccessor _contextAccessor;
        private readonly string _key;
        private CacheSession cache;
        private readonly IList<string> statusAgreement = new List<string> { "1", "2", "5", "9", "K", "I", "F", "G" };

        public CNABController(IMemoryCache _cache, IHttpContextAccessor _contextAccessor)
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
            return View();
        }

        #region "Download"

        [Route("CNAB/Download-CNAB")]
        public IActionResult DownloadCNAB()
        {
            try
            {
                UserLogin userLogin = cache.Get<UserLogin>("userLogin");
                if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");

                //billetResponse = HttpHelper.POST<BilletResponse>(Utils.API.Fis.Uri.BilletP2(), billet);


                //if (billet == null)
                //    ViewBag.Message = "Ops! Não existe boleto para impressão ou o tempo para impressão já expirou, tente novamente.";

                IList<object> list = new List<object> { new RemessaPeriodo(), new Collection<RemessasResponse>() };
                return View(list);
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpPost]
        [Route("CNAB/Download-CNAB")]
        public IActionResult DownloadCNAB(RemessaPeriodo remessaPeriodo)
        {
            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");

            ICollection<RemessasResponse> remessasResponse = new Collection<RemessasResponse>();
            try
            {
                IList<object> list = new List<object>();

                //Consulta WebService

                DateTime DtInicial = DateTime.Today;
                DateTime DtFinal = DateTime.Now;

                if (remessaPeriodo.DataInicial != null)
                    DtInicial = (DateTime)remessaPeriodo.DataInicial;

                if (remessaPeriodo.DataFinal != null)
                    DtFinal = (DateTime)remessaPeriodo.DataFinal.Value.AddDays(1).AddMinutes(-1);

                remessasResponse = HttpHelper.GET<ICollection<RemessasResponse>>(Utils.API.Fis.Uri.Remessa(DtInicial, DtFinal));
                cache.AddCache("RemessasResponse", remessasResponse);

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || remessasResponse == null)
                {
                    ViewBag.Message = "Ops! Não foi possível encontrar nenhuma informação, verifique os dados e tente novamente.";
                    list = new List<object> { new RemessaPeriodo(), remessasResponse };
                    return View(list);
                }

                cache.AddCache("RemessasResponse", remessasResponse);
                list = new List<object> { new RemessaPeriodo(), remessasResponse };
                return View(list);
                //return RedirectToAction("BoletoConcluido");
            }
            catch
            {
                ViewBag.Message = "Ops! Não foi possível gerar o boleto, aconteceu alguma falha durante o processo. Tente novamente.";
                IList<object> list = new List<object> { new RemessaPeriodo(), remessasResponse };
                return View(list);
            }
        }

        [HttpPost]
        [Route("CNAB/Arquivo-Selecionado")]
        public IActionResult ProdutoSelecionado(long idRemessa)
        {
            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");

            ICollection<RemessasResponse> remessasResponse = cache.Get<ICollection<RemessasResponse>>("RemessasResponse");
            if (remessasResponse != null && remessasResponse.Count > 0)
            {



                RemessasResponse remessa = remessasResponse.Where(p => p.IdRemessa == idRemessa).FirstOrDefault();
                if (remessa == null)
                {
                    TempData["Message"] = "Ops! Aconteceu algum problema ao selecionar o produto. Ele não foi localizado neste cliente, tente novamente.";
                    return RedirectToAction("DownloadCNAB");
                }

                RemessaResponse remessaResponse = HttpHelper.GET<RemessaResponse>(Utils.API.Fis.Uri.Remessa(remessa.IdRemessa.ToString()));

                if (remessaResponse != null && remessaResponse.File != null)
                {
                    HttpHelper.GET<bool>(Utils.API.Fis.Uri.SetDownloadRemessa(remessa.IdRemessa.ToString(), userLogin.IdUser.ToString()));

                    MemoryStream fileStream = new MemoryStream();
                    fileStream.Write(remessaResponse.File, 0, remessaResponse.File.Length);
                    fileStream.Position = 0;
                    return new FileStreamResult(fileStream, "text/plain") { FileDownloadName = remessaResponse.NomeArquivo };
                }
                else
                {
                    return RedirectToAction("DownloadCNAB");
                }
            }

            TempData["Message"] = "Ops! Aconteceu algum problema ao selecionar o produto. Ele não foi localizado neste cliente, tente novamente.";
            return RedirectToAction("DownloadCNAB");

        }
        #endregion

        #region "Upload"
        [Route("CNAB/Upload-CNAB")]
        public IActionResult UploadCNAB()
        {
            try
            {
                UserLogin userLogin = cache.Get<UserLogin>("userLogin");
                if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");

                var retornoResponse = HttpHelper.GET<ICollection<RetornoResponse>>(Utils.API.Fis.Uri.Retorno(10));
                cache.AddCache("RetornoResponse", retornoResponse);

                return View(retornoResponse);
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpPost]
        [Route("CNAB/Upload-CNAB")]
        public IActionResult UploadCNAB(IList<IFormFile> formFiles)
        {

            UserLogin userLogin = cache.Get<UserLogin>("userLogin");
            if (userLogin == null || string.IsNullOrEmpty(userLogin.DsName)) return RedirectToAction("Login", "Home");

            ICollection<RetornoResponse> retornoResponse = new Collection<RetornoResponse>();
            try
            {
                var files = Request.Form.Files;
                if (files.Count > 0)
                {
                    var file = files.FirstOrDefault();
                    if (file.FileName.ToUpper().EndsWith(".RET"))
                    {
                        byte[] bytesFile;

                        using (var target = new MemoryStream())
                        {
                            file.CopyTo(target);
                            bytesFile = target.ToArray();
                        }

                        RetornoRequest retorno = new RetornoRequest()
                        {
                            NomeArquivo = file.FileName,
                            IdUserLogin = userLogin.IdUser,
                            Arquivo = bytesFile
                        };
                        var ret = HttpHelper.POST<bool, RetornoRequest>(Utils.API.Fis.Uri.Retorno(), retorno);

                        retornoResponse = HttpHelper.GET<ICollection<RetornoResponse>>(Utils.API.Fis.Uri.Retorno(10));

                        cache.AddCache("RetornoResponse", retornoResponse);
                        return View(retornoResponse);
                    }

                }

                ViewBag.Message = "Ops! Selecione um arquivo com extensão .RET e tente novamente.";
                return View(retornoResponse);


            }
            catch
            {
                ViewBag.Message = "Ops! Não foi possível gerar o boleto, aconteceu alguma falha durante o processo. Tente novamente.";
                IList<object> list = new List<object> { new RemessaPeriodo(), retornoResponse };
                return View(list);
            }
        }
        #endregion
    }
}