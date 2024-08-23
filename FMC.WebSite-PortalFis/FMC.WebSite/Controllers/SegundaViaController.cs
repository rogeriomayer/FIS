using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FMC.WebSite.FIS.Models;
using FMC.WebSite.FIS.Utils;
using FMC.WebSite.FIS.Utils.API.Acordo;
using FMC.WebSite.FIS.Utils.API.Ibi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ServiceStack.Text;

namespace FMC.WebSite.FIS.Controllers
{
    [Route("Segunda-Via")]
    public class SegundaViaController : Controller
    {
        private readonly IMemoryCache _cache;
        private IHttpContextAccessor _contextAccessor;
        private readonly string _key;
        private CacheSession cache;
        readonly IList<string> listStatacorIbi = new List<string> { "1", "2", "5", "9", "K", "I", "F", "G" };
        public SegundaViaController(IMemoryCache _cache, IHttpContextAccessor _contextAccessor)
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
            IList<object> list = new List<object> { new ConsultaCpfCnpj() };
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ConsultaCpfCnpj _model)
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
                         "billet",
                        "boleto",
                        "Navigation",
                        "BoletoEmail"
                    });
                    cache.AddCache("model", _model);

                    #region Log Navigation

                    //URAClient client = new URAClient();
                    Navigation navigation = new Navigation()
                    {
                        Cpf = Regex.Replace(_model.CpfCnpj, @"\D", ""),
                        FlNegotiateNow = false,
                        DtInsert = DateTime.Now
                    };
                    Navigation wsNavigation = AfinzAPI.SetNavigation(navigation);
                    cache.AddCache("Navigation", wsNavigation);

                    #endregion End Log Navigation
                    //TempData["model"] = JsonConvert.SerializeObject(model);
                    return RedirectToAction("Acordo", "SegundaVia");
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

        [Route("Acordo")]
        public ActionResult Acordo()
        {
            ConsultaCpfCnpj _model = cache.Get<ConsultaCpfCnpj>("model");
            IList<AgreementPortal> listAgreementNacc = new List<AgreementPortal>();

            ICollection<CardAgreement> Agreements = new Collection<CardAgreement>();

            if (_model != null && !string.IsNullOrEmpty(_model.CpfCnpj))
            {
                string cpf = Regex.Replace(_model.CpfCnpj, @"\D", "");

                //GetAgreementNACCResponse agreementNacc = new GetAgreementNACCResponse();

                //Utils.API.Acordo.GetAgreementNACCResponse agreementNacc = new Utils.API.Acordo.GetAgreementNACCResponse();
                IList<AgreementPortal> agreementNacc = new List<AgreementPortal>();

                //agreementIbi = HttpHelper.POST<Utils.API.Acordo.GetAgreementIBIResponse>(Utils.API.Acordo.Uri.GetAgreementIBI(), new Utils.API.Acordo.GetAgreementIBIRequest(cpf, string.Empty, 10), HttpHelper.TypeSubmit.Xml);
                try
                {
                    //agreementNacc = HttpHelper.GET<IList<AgreementPortal>>(Utils.API.Acordo.Uri.GetAgreementNACC(cpf))

                }
                catch (Exception ex) { }

                listAgreementNacc = agreementNacc;

                if (cpf.Count() == 11)
                {
                    /*
                    Parallel.ForEach(ibiUr86.Detail, (card) =>
                    {
                        string nrCard = card.NumeroCartao?.Trim().Length == 19 ? card.NumeroCartao?.Trim().Substring(3) : card.NumeroCartao?.Trim();
                        Agreement Agreement = null//HttpHelper.GET<ICollection<Agreement>>(Utils.API.Ibi.Uri.GetAgreementTodayByCard(nrCard)).FirstOrDefault();

                        if (Agreement != null)
                            Agreements.Add(new CardAgreement() { Agreement = Agreement, Card = card.NumeroCartao?.Trim() });
                    });
                    */
                }
            }

            //if ((listAgreementIbi == null && listAgreementNacc == null) || listAgreementIbi.Count == 0 && listAgreementNacc.Count == 0)
            //{
            //    return RedirectToAction("Nada-Consta", "Segunda-Via");
            //}


            IList<object> data = new List<object> { _model, listAgreementNacc, new AcordoSegundaVia(), new BoletoEmail(), Agreements };
            return View(data);
        }

        [Route("Gerar-Boleto")]
        public IActionResult GerarBoleto()
        {
            try
            {
                /*
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                Navigation wsNavigation = cache.Get<Navigation>("Navigation");
                string nrCard = "";//ur86Detail.NumeroCartao?.Trim().Length == 19 ? ur86Detail.NumeroCartao?.Trim().Substring(3) : ur86Detail.NumeroCartao?.Trim();
                if (wsNavigation != null)
                {

                    string cpf = Regex.Replace(model.CpfCnpj, @"\D", "");
                    if (ibiUR86 != null)
                    {
                        ICollection<Agreement> Agreements = HttpHelper.GET<ICollection<Agreement>>(Utils.API.Ibi.Uri.GetAgreementTodayByCard(nrCard));
                        UR86.UR86Detail uR86Detail = ibiUR86.Detail.Where(p => p.NumeroCartao.Contains(ur86Detail.NumeroCartao?.Trim()) && p.SaldoDevedor > 0).FirstOrDefault();
                        if (Agreements != null && Agreements.Count > 0)
                        {
                            Agreement wsA = Agreements.OrderByDescending(p => p.DtInsert).FirstOrDefault();
                            DateTime dtVencimento = wsA.VlEntrace == 0 ? (DateTime)wsA.DtFirstParcel : wsA.DtEntrace;
                            Decimal vlBoleto = wsA.VlEntrace == 0 ? wsA.VlParcel : wsA.VlEntrace;
                            BilletResponse boleto = HttpHelper.GET<BilletResponse>(Utils.API.Ibi.Uri.GetPDF(dtVencimento.ToString("yyyy-MM-dd"), vlBoleto.ToString("N2"), uR86Detail.NumeroConta, cpf, "P"));

                            #region Log Billet
                            if (wsNavigation != null)
                            {
                                Billet Billet = new Billet()
                                {
                                    Account = uR86Detail.NumeroConta,
                                    Age = Convert.ToInt32(uR86Detail.DiasAtraso),
                                    CodeBar = boleto.Line,
                                    VlBillet = vlBoleto,
                                    DtInsert = DateTime.Now,
                                    CPF = cpf,
                                    Email = String.Empty,
                                    FlPrint = false
                                };
                                var wsBillet = HttpHelper.POST<Billet>(Utils.API.Ibi.Uri.SetBillet(), Billet);
                                cache.AddCache("Billet", wsBillet);
                            }
                            #endregion Log Billet

                            cache.AddCache("boleto", boleto);
                            if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || boleto == null)
                                return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");

                            return RedirectToAction("BoletoGerado", "SegundaVia");

                            var pdf = FisAPI.GetBilletPDF("cpf", "codBillet", DateTime.Now, 1, "");
                            MemoryStream pdfStream = new MemoryStream();
                            pdfStream.Write(pdf, 0, pdf.Length);
                            pdfStream.Position = 0;
                            return new FileStreamResult(pdfStream, "application/pdf") { FileDownloadName = "BOLETO_AFINZ.pdf" };

                        }
                        else if (ibiUr83 != null && listStatacorIbi.Contains(ibiUr83.StatusAcordo))
                        {
                            Decimal vlBoleto = 0;
                            DateTime dtVencimento = DateTime.Today.AddDays(1);
                            if (ibiUr83.ValorEntrada == 0 || (Convert.ToDateTime(ibiUr83.DataEntradaAcordo) < DateTime.Today && ibiUr83.IndicadorEntradaAtraso == "N"))
                            {
                                //parcela
                                if (ibiUr83.ValorParcelaAtraso > 0)
                                {
                                    vlBoleto = ibiUr83.ValorParcelaAtraso;
                                }
                                else
                                {
                                    if (ibiUr83.ValorParcela > 0)
                                    {
                                        dtVencimento = Convert.ToDateTime(DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + ibiUr83.DiaVencimento);
                                        if (dtVencimento < DateTime.Today)
                                            dtVencimento = dtVencimento.AddMonths(1);

                                        vlBoleto = ibiUr83.ValorParcela;
                                    }
                                }
                            }
                            else
                            {
                                //entrada
                                if (ibiUr83.ValorEntrada > 0)
                                {
                                    dtVencimento = Convert.ToDateTime(ibiUr83.DataEntradaAcordo);
                                    if (dtVencimento < DateTime.Today)
                                        dtVencimento = DateTime.Today;
                                    vlBoleto = ibiUr83.ValorEntrada;
                                }
                            }

                            BilletResponse boleto = HttpHelper.GET<BilletResponse>(Utils.API.Ibi.Uri.GetPDF(dtVencimento.ToString("yyyy-MM-dd"), vlBoleto.ToString("N2"), uR86Detail.NumeroConta, cpf, "P"));
                            #region Log Billet
                            if (wsNavigation != null)
                            {
                                Billet billet = new Billet()
                                {
                                    Account = uR86Detail.NumeroConta,
                                    Age = Convert.ToInt32(uR86Detail.DiasAtraso),
                                    CodeBar = boleto.Line,
                                    VlBillet = vlBoleto,
                                    DtInsert = DateTime.Now,
                                    CPF = cpf,
                                    Email = String.Empty,
                                    FlPrint = false
                                };

                                var wsBillet = HttpHelper.POST<Billet>(Utils.API.Ibi.Uri.SetBillet(), billet);
                                cache.AddCache("Billet", wsBillet);
                            }
                            #endregion Log Billet
                            cache.AddCache("boleto", boleto);

                            if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || boleto == null)
                                return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");

                            return RedirectToAction("BoletoGerado", "SegundaVia");
                            //return boleto;
                            //MemoryStream pdfStream = new MemoryStream();
                            //pdfStream.Write(boleto.PDF, 0, boleto.PDF.Length);
                            //pdfStream.Position = 0;
                            //return new FileStreamResult(pdfStream, "application/pdf") { FileDownloadName = "BOLETO_BRADESCARD.pdf" };
                        }
                        else
                        {
                            BilletResponse boleto = HttpHelper.GET<BilletResponse>(Utils.API.Ibi.Uri.GetPDF(DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"), uR86Detail.SaldoDevedor.ToString("N2"), uR86Detail.NumeroConta, cpf, "P"));
                            #region Log Billet
                            if (wsNavigation != null)
                            {
                                Billet billet = new Billet()
                                {
                                    Account = uR86Detail.NumeroConta,
                                    Age = Convert.ToInt32(uR86Detail.DiasAtraso),
                                    CodeBar = boleto.Line,
                                    VlBillet = uR86Detail.SaldoDevedor,
                                    DtInsert = DateTime.Now,
                                    CPF = cpf,
                                    Email = String.Empty,
                                    FlPrint = false
                                };
                                var wsBillet = HttpHelper.POST<Billet>(Utils.API.Ibi.Uri.SetBillet(), billet);
                                cache.AddCache("Billet", wsBillet);
                            }
                            #endregion Log Billet
                            cache.AddCache("boleto", boleto);

                            if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || boleto == null)
                                return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");

                            return RedirectToAction("BoletoGerado", "SegundaVia");
                            var pdf = FisAPI.GetBilletPDF("CPF", "codBillet", DateTime.Today, 1, "");
                            MemoryStream pdfStream = new MemoryStream();
                            pdfStream.Write(pdf, 0, pdf.Length);
                            pdfStream.Position = 0;
                            return new FileStreamResult(pdfStream, "application/pdf") { FileDownloadName = "BOLETO_AFINZ.pdf" };
                        }
                    }
                }
                //cache.AddCache("AcordoSegundaVia", acordoSegundaVia);
                */
                return RedirectToAction("Index", "SegundaVia");
            }
            catch
            {
                return RedirectToAction("Index", "SegundaVia");
            }
            finally
            {
                cache.Reset();
            }
            return null;
        }

        [Route("Gerar-Boleto")]
        [HttpPost]
        public IActionResult GerarBoleto(object ur86Detail)
        {
            Navigation wsNavigation = cache.Get<Navigation>("Navigation");
            if (wsNavigation != null)
            {
                cache.AddCache("ur86Detail", ur86Detail);
                return RedirectToAction("GerarBoleto", "SegundaVia");
            }
            else
            {
                return RedirectToAction("Index", "SegundaVia");
            }
        }

        [Route("Boleto-Gerado")]
        public ActionResult BoletoGerado()
        {
            BilletResponse boleto = cache.Get<BilletResponse>("boleto");
            string html = "";
            if (boleto != null)
            {
                html = "<div class=\"box-boleto\">";
                html += "<p>";
                html += "Seu boleto foi gerado com sucesso.";
                html += "</p>";
                html += "<p>Lembramos que ele <b>só estará disponível para pagamento em até 24 horas</b>, assim que o registro do código de barras ser efetivado pelo banco.</p>";
                html += "<p>Clique no botão abaixo para imprimir o seu boleto.</p>";
                html += "</div>";
            }
            else
            {
                html = "<div class=\"box-boleto\">";
                html += "<p style='color: red'>Aconteceu algum problema durante a geração do seu boleto, tente novamente</p>";
                html += "</div>";
            }
            return Content(html, "text/html");
        }

        [Route("Imprimir-Boleto")]
        public ActionResult ImprimirBoleto()
        {
            BilletResponse boleto = cache.Get<BilletResponse>("boleto");
            Billet wsBillet = cache.Get<Billet>("Billet");

            var pdf = FisAPI.GetBilletPDF("CPF", "codBillet", DateTime.Today, 1, "");
            string html = "";
            if (boleto != null && pdf != null)
            {
                wsBillet.FlPrint = true;
                //HttpHelper.POST<Billet>(Utils.API.Ibi.Uri.SetBillet(), wsBillet);
                MemoryStream pdfStream = new MemoryStream();
                pdfStream.Write(pdf, 0, pdf.Length);
                pdfStream.Position = 0;
                return new FileStreamResult(pdfStream, "application/pdf") { FileDownloadName = "BOLETO_AFINZ.pdf" };
            }
            else
            {
                return RedirectToAction("Index", "SegundaVia");
            }
        }


        [Route("Boleto-Email")]
        public bool BoletoEmail()
        {
            try
            {
                /*
                Utils.API.Ibi.UR86 ibiUR86 = cache.Get<Utils.API.Ibi.UR86>("ibiUr86");
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                BoletoEmail boletoEmail = cache.Get<BoletoEmail>("BoletoEmail");
                Navigation wsNavigation = cache.Get<Navigation>("Navigation");
                if (wsNavigation != null)
                {
                    if (!string.IsNullOrEmpty(boletoEmail.Email) && Util.IsEmail(boletoEmail.Email) && boletoEmail.Email == boletoEmail.EmailAgain && !string.IsNullOrEmpty(boletoEmail.Card))
                    {
                        string cpf = Regex.Replace(model.CpfCnpj, @"\D", "");
                        if (ibiUR86 != null)
                        {
                            UR86.UR86Detail uR86Detail = ibiUR86.Detail.Where(p => p.NumeroCartao?.Trim() == boletoEmail.Card?.Trim() && p.SaldoDevedor > 0).FirstOrDefault();
                            string nrCard = boletoEmail.Card?.Trim().Length == 19 ? boletoEmail.Card?.Trim().Substring(3) : boletoEmail.Card?.Trim();
                            ICollection<Agreement> Agreements = HttpHelper.GET<ICollection<Agreement>>(Utils.API.Ibi.Uri.GetAgreementTodayByCard(nrCard));
                            UR83 ibiUr83 = HttpHelper.GET<UR83>(Utils.API.Ibi.Uri.GetUR83(nrCard));
                            if (Agreements != null && Agreements.Count > 0)
                            {
                                Agreement wsA = Agreements.OrderByDescending(p => p.DtInsert).FirstOrDefault();
                                DateTime dtVencimento = wsA.VlEntrace == 0 ? (DateTime)wsA.DtFirstParcel : wsA.DtEntrace;
                                Decimal vlBoleto = wsA.VlEntrace == 0 ? wsA.VlParcel : wsA.VlEntrace;

                                string x = HttpHelper.GET<string>(Utils.API.Ibi.Uri.SendBilletWebSite(dtVencimento.ToString("yyyy-MM-dd"), vlBoleto.ToString("N2"), uR86Detail.NumeroConta, cpf, boletoEmail.Email));
                                #region Log Billet
                                if (wsNavigation != null)
                                {
                                    Billet billet = new Billet()
                                    {
                                        Account = uR86Detail.NumeroConta,
                                        Age = Convert.ToInt32(uR86Detail.DiasAtraso),
                                        CodeBar = x,
                                        VlBillet = vlBoleto,
                                        DtInsert = DateTime.Now,
                                        CPF = cpf,
                                        Email = boletoEmail.Email,
                                        FlPrint = false
                                    };

                                    var wsBillet = HttpHelper.POST<Billet>(Utils.API.Ibi.Uri.SetBillet(), billet);
                                    cache.AddCache("Billet", wsBillet);
                                }
                                #endregion Log Billet
                                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || string.IsNullOrEmpty(x))
                                    return false;
                                return true;
                            }
                            else if (ibiUr83 != null && listStatacorIbi.Contains(ibiUr83.StatusAcordo))
                            {
                                Decimal vlBoleto = 0;
                                DateTime dtVencimento = DateTime.Today.AddDays(1);
                                if (ibiUr83.ValorEntrada == 0 || (Convert.ToDateTime(ibiUr83.DataEntradaAcordo) < DateTime.Today && ibiUr83.IndicadorEntradaAtraso == "N"))
                                {
                                    //parcela
                                    if (ibiUr83.ValorParcelaAtraso > 0)
                                    {
                                        vlBoleto = ibiUr83.ValorParcelaAtraso;
                                    }
                                    else
                                    {
                                        if (ibiUr83.ValorParcela > 0)
                                        {
                                            dtVencimento = Convert.ToDateTime(DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + ibiUr83.DiaVencimento);
                                            if (dtVencimento < DateTime.Today)
                                                dtVencimento = dtVencimento.AddMonths(1);

                                            vlBoleto = ibiUr83.ValorParcela;
                                        }
                                    }
                                }
                                else
                                {
                                    //entrada
                                    if (ibiUr83.ValorEntrada > 0)
                                    {
                                        dtVencimento = Convert.ToDateTime(ibiUr83.DataEntradaAcordo);
                                        if (dtVencimento < DateTime.Today)
                                            dtVencimento = DateTime.Today;
                                        vlBoleto = ibiUr83.ValorEntrada;
                                    }
                                }

                                // Decimal vlBoleto = ibiUr83.ValorEntrada == 0 ? (ibiUr83.ValorParcelaAtraso > 0) ? ibiUr83.ValorParcelaAtraso : ibiUr83.ValorParcela : ibiUr83.ValorEntrada;
                                string x = HttpHelper.GET<string>(Utils.API.Ibi.Uri.SendBilletWebSite(dtVencimento.ToString("yyyy-MM-dd"), vlBoleto.ToString("N2"), uR86Detail.NumeroConta, cpf, boletoEmail.Email));
                                #region Log Billet
                                if (wsNavigation != null)
                                {

                                    Billet billet = new Billet()
                                    {
                                        Account = uR86Detail.NumeroConta,
                                        Age = Convert.ToInt32(uR86Detail.DiasAtraso),
                                        CodeBar = x,
                                        VlBillet = vlBoleto,
                                        DtInsert = DateTime.Now,
                                        CPF = cpf,
                                        Email = boletoEmail.Email,
                                        FlPrint = false
                                    };

                                    var wsBillet = HttpHelper.POST<Billet>(Utils.API.Ibi.Uri.SetBillet(), billet);
                                    cache.AddCache("Billet", wsBillet);
                                }
                                #endregion Log Billet
                                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || string.IsNullOrEmpty(x))
                                    return false;
                                return true;
                            }
                            else
                            {
                                string x = HttpHelper.GET<string>(Utils.API.Ibi.Uri.SendBilletWebSite(DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"), uR86Detail.SaldoDevedor.ToString("N2"), uR86Detail.NumeroConta, cpf, boletoEmail.Email));
                                #region Log Billet
                                if (wsNavigation != null)
                                {
                                    Billet billet = new Billet()
                                    {
                                        Account = uR86Detail.NumeroConta,
                                        Age = Convert.ToInt32(uR86Detail.DiasAtraso),
                                        CodeBar = x,
                                        VlBillet = uR86Detail.SaldoDevedor,
                                        DtInsert = DateTime.Now,
                                        CPF = cpf,
                                        Email = boletoEmail.Email,
                                        FlPrint = false
                                    };
                                    var wsBillet = HttpHelper.POST<Billet>(Utils.API.Ibi.Uri.SetBillet(), billet);
                                    cache.AddCache("Billet", wsBillet);
                                }
                                #endregion Log Billet

                                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || string.IsNullOrEmpty(x))
                                    return false;

                                return true;
                            }
                        }
                    }
                    //cache.AddCache("AcordoSegundaVia", acordoSegundaVia);
                }
                */
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                cache.Reset();
            }
        }

        [Route("Boleto-Email")]
        [HttpPost]
        public IActionResult BoletoEmail(BoletoEmail boletoEmail)
        {
            try
            {
                Navigation wsNavigation = cache.Get<Navigation>("Navigation");
                if (wsNavigation != null)
                {
                    //var form =Request.Form();
                    //                var form = Request.Form;
                    cache.AddCache("BoletoEmail", boletoEmail);
                    return RedirectToAction("BoletoEmail", "SegundaVia");
                }
                else
                {
                    return RedirectToAction("Index", "SegundaVia");
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            return null;
        }




        [Route("Nada-Consta")]
        public IActionResult NadaConsta()
        {
            ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
            if (model == null || string.IsNullOrEmpty(model.CpfCnpj))
            {
                return RedirectToAction("Index", "Segunda-Via");
            }
            IList<object> data = new List<object> { model };
            return View(data);
        }

    }
}