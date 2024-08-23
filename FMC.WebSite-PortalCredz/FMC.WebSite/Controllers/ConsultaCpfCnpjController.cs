using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FMC.WebSite.FIS.Models;
using FMC.WebSite.FIS.Utils;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using System.IO;
using FMC.WebSite.FIS.Utils.API.Ibi;
using System.Net.Http;
using System.Web.UI;
using System.Collections.ObjectModel;
using FMC.WebSite.FIS.Controllers.Class;

namespace FMC.WebSite.FIS.Controllers
{
    [Route("Consulta-Documento")]
    public class ConsultaCpfCnpjController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string _key;
        private CacheSession cache;
        private HttpContextAccessor context;

        public ConsultaCpfCnpjController(IMemoryCache _cache, IHttpContextAccessor _contextAccessor)
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

        private bool Autenticated()
        {
            bool Autenticated = cache.Get<bool>("Autenticated");
            return Autenticated;
        }
        public ActionResult Index()
        {
            try
            {
                cache.Remove(new List<string> {
                        "simulaParcelamento" ,
                        "parcelamento",
                        "billet",
                        "boleto",
                        "MessageEnderecoUpdate",
                        "MessageSuccess",
                        "Simulate",
                        "idAcordo",
                        "termo",
                        "TentativaValidacao",
                        "Validacao"
            });
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                if (model == null)
                {
                    return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");
                }
                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || model == null || string.IsNullOrEmpty(model.CpfCnpj) || !Autenticated())
                {
                    return RedirectToAction("Index", "Home");
                }

                //Se for IBI, encerra operação
                Produto _produto = cache.Get<Produto>("produto");
                //if (!VerifyOperation.ValidOperation())
                //    return RedirectToAction("Encerrado", "ConsultaCpfCnpj");

                string cpfCnpj = Regex.Replace(model.CpfCnpj, @"[^\d]", "");

                //Itens para LOG
                IList<Product> listWsProduct = new List<Product>();
                Navigation wsNavigation = cache.Get<Navigation>("Navigation");

                PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");
                if (cache.Get<Agreement>("Agreement") != null && cache.Get<Agreement>("Agreement").DtInsert > DateTime.Now.AddMinutes(-20))
                {
                    cache.Remove("Pessoa");
                    cache.Remove("Agreement");
                    pessoa = FisAPI.GetPerson(cpfCnpj, 3);
                    cache.AddCache<PersonResponse>("Pessoa", pessoa);
                }


                if (cpfCnpj.Count() == 11)
                {
                    Parallel.ForEach(pessoa.Cards.ToList(), (CardResponse card) =>
                    {
                        if (card.Age > 1)
                        {
                            #region Log Product IBI
                            var product = new Product()
                            {
                                IdNavigation = wsNavigation.IdNavigation,
                                Age = Convert.ToInt32(card.Age),
                                Account = card.Account,
                                VlFull = card.VlFull,
                                VlMinimum = card.VlMinimum,
                                DtInsert = DateTime.Now,
                                ProductType = card.IdProductType.ToString()
                            };
                            var wsProduct = AfinzAPI.SetProduct(product);
                            listWsProduct.Add(wsProduct);
                            #endregion Log Product IBI
                        }
                    });
                }
                cache.AddCache("Product", listWsProduct.ToList());

                if (pessoa != null && pessoa.Cards.Any())
                {
                    IList<object> data = new List<object> { model, pessoa };
                    return View(data);
                }
                else
                {
                    return RedirectToAction("NadaConsta", "ConsultaCpfCnpj");
                }
            }
            catch (Exception e)
            {
                //throw e;
                return RedirectToAction("Encerrado", "ConsultaCpfCnpj");
            }
        }

        [HttpPost]
        public ActionResult Index(ConsultaCpfCnpj dados)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    cache.AddCache("model", dados);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [Route("Confirmacao-De-Endereco")]
        public IActionResult ConfirmacaoDeEndereco()
        {
            try
            {

                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                if (model == null)
                    return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");

                bool enderecoAtualizado = cache.Get<bool>("EnderecoAtualizado");
                if (enderecoAtualizado)
                    RedirectToAction("Finalizar");
                Produto _produto = cache.Get<Produto>("produto");

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || _produto == null)
                    return RedirectToAction("Index", "Home");

                //if (!VerifyOperation.ValidOperation())
                //   return RedirectToAction("Encerrado", "ConsultaCpfCnpj");

                List<string> MessageEnderecoUpdate = cache.Get<List<string>>("MessageEnderecoUpdate");
                List<string> MessageSuccess = cache.Get<List<string>>("MessageSuccess");

                if (MessageEnderecoUpdate != null)
                {
                    ViewBag.MessageEnderecoUpdate = MessageEnderecoUpdate;
                    cache.Remove("MessageEnderecoUpdate");
                }
                if (MessageSuccess != null)
                {
                    ViewBag.MessageSuccess = MessageSuccess;
                    cache.Remove("MessageSuccess");
                }
                //NaccService.Pessoa naccPessoa = cache.Get<NaccService.Pessoa>("naccPessoa");
                //Utils.API.Nacc.Pessoa naccPessoa = cache.Get<Utils.API.Nacc.Pessoa>("naccPessoa");

                Models.Endereco endereco = new Models.Endereco();
                IList<string> email = new List<string>();

                if (_produto.NomeProduto == "IBI" && !string.IsNullOrEmpty(_produto.CodProduto))
                {
                    string cpf = (model.CpfCnpj).Replace(".", "").Replace("-", "");
                    PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");

                    if (pessoa == null || string.IsNullOrEmpty(pessoa.CPF))
                    {
                        /*
                        PersonResponse person = new PersonResponse
                        {
                            IdPessoa = 0,
                            Cpf = cpf,
                            Nome = pessoa.Nome

                        };
                        var post = HttpHelper.POST<long>(Utils.API.Ibi.Uri.SetDataPerson(), person);
                        pessoa = HttpHelper.GET<PersonResponse>(Utils.API.Ibi.Uri.GetPerson(model.CpfCnpj));
                        */
                    }
                    if (pessoa != null)
                    {
                        var end = pessoa.Address;
                        if (end != null)
                        {
                            endereco.Rua = end.Address;
                            endereco.Numero = end.NrAddress;
                            endereco.Bairro = end.District;
                            endereco.Cep = end.Cep;
                            endereco.Complemento = end.Complement;
                            endereco.Cidade = end.City;
                            endereco.Estado = end.UF;
                        }
                        var phones = pessoa.Phones;
                        endereco.Telefones = pessoa.Phones;
                        if (pessoa.Email.Count > 0)
                            email = pessoa.Email;
                        cache.AddCache("Pessoa", pessoa);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                EnderecoUpdate endUpdate = cache.Get<EnderecoUpdate>("EnderecoUpdate");
                IList<object> data = new List<object> { model, endereco, cache.Get<Produto>("produto"), endUpdate, email };
                return View(data);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [Route("Atualiza-Endereco")]
        public IActionResult AtualizaEndereco(Models.Endereco endereco, string email)
        {
            return null;
            /*
            try
            {
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                if (model == null)
                    return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || !Autenticated())
                    return RedirectToAction("Index", "Home");

                //Recuperar Objetos do Cache
                Produto _produto = cache.Get<Produto>("produto");
                PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");

                List<string> messageSuccess = new List<string>();
                //IbiService.URAClient client = new IbiService.URAClient();
                //Atualiza endereços
                if (_produto.NomeProduto == "IBI")
                {

                    //Se for IBI, encerra operação
                    if (!VerifyOperation.ValidOperation())
                        return RedirectToAction("Encerrado", "ConsultaCpfCnpj");

                    if (pessoa == null || string.IsNullOrEmpty(pessoa.CPF))
                    {
                        PersonResponse person = new PersonResponse
                        {
                            IdPessoa = 0,
                            Cpf = Regex.Replace(model.CpfCnpj, @"\D", ""),
                            Nome = pessoa.Nome
                        };
                        HttpHelper.POST<long>(Utils.API.Ibi.Uri.SetDataPerson(), person);
                        pessoa = HttpHelper.GET<PersonResponse>(Utils.API.Ibi.Uri.GetPerson(Regex.Replace(model.CpfCnpj, @"\D", "")));
                    }
                    if (pessoa != null)
                    {
                        if (pessoa.Endereco.IdEndereco > 0)
                        {
                            pessoa.Endereco.Cep = (string.IsNullOrEmpty(endereco.Cep)) ? "" : Regex.Replace(endereco.Cep, @"\D", "");
                            pessoa.Endereco.Logradouro = string.IsNullOrEmpty(endereco.Rua) ? "" : endereco.Rua;
                            pessoa.Endereco.Numero = string.IsNullOrEmpty(endereco.Numero) ? "" : endereco.Numero;
                            pessoa.Endereco.Complemento = string.IsNullOrEmpty(endereco.Complemento) ? "" : endereco.Complemento;
                            pessoa.Endereco.Bairro = string.IsNullOrEmpty(endereco.Bairro) ? "" : endereco.Bairro;
                            pessoa.Endereco.Cidade = string.IsNullOrEmpty(endereco.Cidade) ? "" : endereco.Cidade;
                            pessoa.Endereco.Estado = string.IsNullOrEmpty(endereco.Estado) ? "" : endereco.Estado;
                        }
                        else
                        {
                            pessoa.Endereco = new Utils.API.Ibi.Endereco
                            {
                                Logradouro = endereco.Rua,
                                Numero = endereco.Numero,
                                Complemento = endereco.Complemento,
                                Bairro = endereco.Bairro,
                                Cidade = endereco.Cidade,
                                Estado = endereco.Estado,
                                Cep = Regex.Replace(endereco.Cep, @"\D", "")
                            };
                        }

                        if (pessoa.Emails.Where(p => p.Email == email).FirstOrDefault() == null)
                        {
                            if (!string.IsNullOrEmpty(email))
                                pessoa.Emails.Add(new Utils.API.Ibi.Mail { Email = email, Data = DateTime.Now });
                        }
                        else
                        {
                            var mail = pessoa.Emails.Where(p => p.Email == email).FirstOrDefault();
                            mail.Data = DateTime.Now;
                        }

                    }
                    //long idAccount = client.SetDataPersonAsync(pessoa).Result;
                    long idAccount = HttpHelper.POST<long>(Utils.API.Ibi.Uri.SetDataPerson(), pessoa);
                    if (idAccount > 0)
                        messageSuccess.Add("Dados atualizado com sucesso!");

                    _produto.IdAccount = idAccount;
                    cache.AddCache("produto", _produto);

                    EnderecoUpdate endUpdate = cache.Get<EnderecoUpdate>("EnderecoUpdate");
                    if (endUpdate == null)
                        endUpdate = new EnderecoUpdate() { Endereco = true };
                    else
                        endUpdate.Endereco = true;

                    cache.AddCache("Pessoa", pessoa);
                    cache.AddCache("MessageSuccess", messageSuccess);
                    cache.AddCache("EnderecoUpdate", endUpdate);
                }

                #region Log Atualizou Endereço
                Address wsAddress = cache.Get<Address>("Address");
                if (wsAddress == null)
                {
                    Navigation wsNavigation = cache.Get<Navigation>("Navigation");
                    Address address = new Address { DtInsert = DateTime.Now, FlUpdateAddress = true, FlContinue = false, IdNavigation = wsNavigation.IdNavigation };
                    var _wsAddress = HttpHelper.POST<Address>(Utils.API.Ibi.Uri.SetAddress(), address);
                    cache.AddCache("Address", _wsAddress);
                }
                #endregion

                return RedirectToAction("ConfirmacaoDeEndereco");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
            */
        }

        [Route("Endereco-Atualizado")]
        public IActionResult EnderecoAtualizado()
        {
            try
            {
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                if (model == null)
                    return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || !Autenticated())
                    return RedirectToAction("Index", "Home");

                EnderecoUpdate endUpdate = cache.Get<EnderecoUpdate>("EnderecoUpdate");

                PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");
                Produto _produto = cache.Get<Produto>("produto");

                //if (!VerifyOperation.ValidOperation())
                //    return RedirectToAction("Encerrado", "ConsultaCpfCnpj");

                List<string> messageError = new List<string>();
                if (endUpdate == null)
                {
                    messageError.Add("Para continuar, é necessário atualizar seu endereço.");
                    messageError.Add("É necessário possuir pelo menos um telefone para contato.");
                }
                if (endUpdate != null && !endUpdate.Endereco)
                {
                    messageError.Add("Para continuar, é necessário atualizar seu endereço.");
                }
                if (endUpdate != null && !endUpdate.Telefone)
                {
                    if (
                        (pessoa != null && pessoa.Phones != null && pessoa.Phones.Count > 0)
                       )
                        endUpdate.Telefone = true;
                    else
                        messageError.Add("É necessário possuir pelo menos um telefone para contato.");
                }
                if (endUpdate != null && endUpdate.Endereco && endUpdate.Telefone)
                {
                    //Endereço atualizado Gerar cache
                    cache.AddCache("EnderecoAtualizado", true);

                    #region Atualizar Clique no botão Continuar
                    //IbiService.Address wsAddress = cache.Get<IbiService.Address>("Address");
                    Address wsAddress = cache.Get<Address>("Address");
                    if (wsAddress != null)
                    {
                        wsAddress.FlContinue = true;
                        //IbiService.URAClient client = new IbiService.URAClient();
                        //var _wsAddress = client.SetAddressAsync(wsAddress).Result;
                        try
                        {
                            //var _wsAddress = HttpHelper.POST<Address>(Utils.API.Ibi.Uri.SetAddress(), wsAddress);
                            // cache.AddCache("Address", _wsAddress);
                        }
                        catch
                        {

                        }
                    }
                    #endregion
                    return RedirectToAction("Finalizar");
                }
                else
                {
                    cache.AddCache("MessageEnderecoUpdate", messageError);
                    return RedirectToAction("ConfirmacaoDeEndereco");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("ConfirmacaoDeEndereco");
            }

        }


        [HttpPost]
        [Route("Simular-Negociacao")]
        public IActionResult SimularNegociacao(CardResponse conta = null)
        {
            try
            {
                //if (!VerifyOperation.ValidOperation())
                //    return RedirectToAction("Encerrado", "ConsultaCpfCnpj");

                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                if (model == null)
                    return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");

                if (conta != null && !string.IsNullOrEmpty(conta.Account))
                    cache.AddCache("produto", new Produto() { CodProduto = conta.Account, NomeProduto = "IBI" });

                return RedirectToAction(nameof(FormaDePagamento));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        [Route("Forma-De-Pagamento")]
        public IActionResult FormaDePagamento(SimulaParcelamento simula)
        {
            string message = "";
            try
            {
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                if (model == null)
                    return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");

                Produto _produto = cache.Get<Produto>("produto");

                IList<object> data = new List<object>
                    {
                        cache.Get<ConsultaCpfCnpj>("model"),
                        cache.Get<PersonResponse>("Pessoa").Cards.Where(p => p.Account == _produto.CodProduto).FirstOrDefault(),
                        cache.Get<AgreementSimulateResponse>("parcelamento"),
                        cache.Get<ParcelamentoFaturaResponse>("simulaParcelamentoFatura"),
                        simula
                    };

                if (simula.DataEntrada == null)
                    simula.DataEntrada = DateTime.Today.DayOfWeek == DayOfWeek.Sunday ? DateTime.Today.AddDays(3).ToString("dd/MM/yyyy") : DateTime.Today.AddDays(4).ToString("dd/MM/yyyy");
                //if (Convert.ToDateTime(simula.DataEntrada) > DateTime.Today.AddDays(20))
                //    simula.DataEntrada = DateTime.Today.DayOfWeek == DayOfWeek.Sunday ? DateTime.Today.AddDays(19).ToString("dd/MM/yyyy") : DateTime.Today.AddDays(20).ToString("dd/MM/yyyy");

                if (simula == null || simula.DataEntrada == null || simula.Entrada == null)
                {
                    ViewData["Message"] = new List<string> { "Informe uma valor de entrada e uma data de entrada válidos!" };
                    return View(data);
                }
                if (!Utils.Util.IsNumeric(simula.Entrada))
                {
                    ViewData["Message"] = new List<string> { "Valor de entrada inválido!" };
                    return View(data);
                }

                if (!Utils.Util.IsDate(simula.DataEntrada))
                {
                    ViewData["Message"] = new List<string> { "Data de entrada inválida!" };
                    return View(data);
                }

                cache.Remove("simulaParcelamento");
                cache.Remove("parcelamento");
                cache.Remove("simulaParcelamentoFatura");

                simula.FixedEntraceValue = Convert.ToDecimal(simula.Entrada) > 0 ? true : false;

                cache.AddCache("simulaParcelamento", simula);

                return RedirectToAction("FormaDePagamento");

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Route("Forma-De-Pagamento")]
        public IActionResult FormaDePagamento()
        {
            CardResponse conta = new CardResponse();

            try
            {
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");
                ICollection<Discount> listDiscount = cache.Get<ICollection<Discount>>("Discount");

                if (model == null || pessoa == null)
                    return RedirectToAction(nameof(SessaoExpirada));

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || !Autenticated())
                    return RedirectToAction(nameof(Index), "Home");

                IList<string> message = new List<string>();
                Produto _produto = cache.Get<Produto>("produto");
                //        if (!VerifyOperation.ValidOperation())
                //            return RedirectToAction(nameof(Encerrado));

                if (_contextAccessor.HttpContext.Session.GetString("_sID") == null || model == null || pessoa == null)
                {
                    return RedirectToAction(nameof(Index), "Home");
                }

                conta = pessoa.Cards.Where(p => p.Account == _produto.CodProduto).FirstOrDefault();

                SimulaParcelamento simulaParcelamento = cache.Get<SimulaParcelamento>("simulaParcelamento");

                if (simulaParcelamento == null)
                {
                    decimal pctDiscount = 0;

                    if (listDiscount == null)
                        listDiscount = FisAPI.GetDiscount(3, "");

                    if (listDiscount != null && listDiscount.Count() > 0)
                    {
                        cache.Remove("Discount");
                        cache.AddCache<ICollection<Discount>>("Discount", listDiscount);
                        var discount = listDiscount.Where(p => (conta.Age >= p.MinAge && conta.Age <= p.MaxAge) && p.MinParcel > 1 && p.IdProductType == 3).FirstOrDefault();
                        if (discount != null) pctDiscount = discount.MaxDiscount;
                    }


                    //decimal vlEntrada = Convert.ToInt32((conta.VlFull - (conta.VlFull * (pctDiscount / 100))) / 2);
                    decimal vlEntrada = 50;
                    DateTime dtEntrada = DateTime.Today.AddDays(7);

                    simulaParcelamento = new SimulaParcelamento()
                    {
                        DataEntrada = dtEntrada.DayOfWeek == DayOfWeek.Sunday ? dtEntrada.AddDays(1).ToString("dd/MM/yyyy") : dtEntrada.ToString("dd/MM/yyyy"),
                        Entrada = vlEntrada < 50 ? Convert.ToDecimal(50).ToString("N2") : vlEntrada.ToString("N2"),
                        FixedEntraceValue = true
                        //Parcela = "0"
                    };
                    cache.AddCache<SimulaParcelamento>("simulaParcelamento", simulaParcelamento);
                }
                else
                {

                }
                if (Convert.ToDateTime(simulaParcelamento.DataEntrada) > DateTime.Today.AddDays(30))
                {
                    simulaParcelamento.DataEntrada = DateTime.Today.AddDays(30).ToString("dd/MM/yyyy");
                    cache.Remove("simulaParcelamento");
                    cache.AddCache("simulaParcelamento", simulaParcelamento);
                    message.Add("A data de entrada foi ajustada para a data máxima permitida " + simulaParcelamento.DataEntrada + " !");
                }

                if (Convert.ToInt32(conta.Age) < 2)
                    return RedirectToAction(nameof(CentralCobranca));

                #region VERIFICAR ACORDO REALIZADO HOJE
                ICollection<AgreementResponse> listAgreement = new HashSet<AgreementResponse>();

                listAgreement = conta.StatusLeadResponse.Where(sl => sl.AgreementResponse != null).Select(p => p.AgreementResponse).ToList();

                AgreementResponse agreement = null;
                if (listAgreement.Count() > 0)
                    agreement = listAgreement.OrderByDescending(p => p.IdAgreement).FirstOrDefault();

                #endregion
                if (agreement != null && !conta.AvailableBilling)
                    return RedirectToAction("Acordo", "SegundaVia");

                SimulacaoAcordo simulacaoAcordo = new SimulacaoAcordo();
                AgreementSimulateRequest agreementSimulateRequest = new AgreementSimulateRequest()
                {
                    CPF = pessoa.CPF,
                    Age = conta.Age,
                    Product = conta.AccountNumber, //conta.Account,
                    PctDiscount = AppSettings.Desconto,
                    NrParcel = 25
                };

                if (simulacaoAcordo.AccountIBI(_produto, conta, agreementSimulateRequest, simulaParcelamento, message, cache))
                {
                    IList<object> data = new List<object>
                    {
                        cache.Get<ConsultaCpfCnpj>("model"),
                        conta,
                        cache.Get<AgreementSimulateResponse>("parcelamento"),
                        cache.Get<ParcelamentoFaturaResponse>("simulaParcelamentoFatura"),
                        simulaParcelamento
                    };
                    ViewData["Message"] = message;
                    return View(data);
                }
                else
                {
                    IList<object> data = new List<object>
                    {
                        cache.Get<ConsultaCpfCnpj>("model"),
                        conta,
                        cache.Get<AgreementSimulateResponse>("parcelamento"),
                        cache.Get<ParcelamentoFaturaResponse>("simulaParcelamentoFatura"),
                        simulaParcelamento
                    };
                    message.Add("Condições de parcelamento não aceitas!");
                    message.Add("Informe outro valor e data de entrada!");
                    ViewData["Message"] = message;
                    return View(data);
                }
            }
            catch (Exception e)
            {
                IList<object> data = new List<object>
                    {
                        cache.Get<ConsultaCpfCnpj>("model"),
                        conta,
                        cache.Get<AgreementSimulateResponse>("parcelamento"),
                        cache.Get<ParcelamentoFaturaResponse>("simulaParcelamentoFatura"),
                        cache.Get<SimulaParcelamento>("simulaParcelamento")
                    };
                ViewData["Message"] = new List<string> { "Condições de parcelamento não aceitas!", "Informe outro valor e data de entrada!" };
                return View(data);
                //return RedirectToAction(nameof(CentralCobranca));
            }
        }

        [Route("Central-De-Cobranca")]
        public IActionResult CentralCobranca()
        {
            IList<object> data = new List<object> { new ConsultaCpfCnpj() };
            return View(data);
        }

        [HttpPost]
        [Route("Termos-E-Condicoes")]
        public IActionResult TermosECondicoes(string totalParcel)
        {
            try
            {
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                if (model == null)
                    return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");

                Produto _produto = cache.Get<Produto>("produto");

                //if (!VerifyOperation.ValidOperation())
                //    return RedirectToAction("Encerrado", "ConsultaCpfCnpj");

                SimulaParcelamento simulaParcelamento = cache.Get<SimulaParcelamento>("simulaParcelamento");

                PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");
                CardResponse conta = new CardResponse();


                TermoECondicao termo = cache.Get<TermoECondicao>("termo");

                if (_produto != null && _produto.NomeProduto == "IBI")
                {
                    conta = pessoa.Cards.Where(p => p.Account == _produto.CodProduto).FirstOrDefault();
                    cache.AddCache("totalParcel", totalParcel);

                    #region LOG AGREEMENT IBI
                    //Insert Log Agreement
                    IList<Product> listWsProduct = cache.Get<List<Product>>("Product");
                    Product wsProduct = listWsProduct.Where(p => p.Account == _produto.CodProduto).FirstOrDefault();
                    Agreement wsAgreement = cache.Get<Agreement>("Agreement");
                    Simulate wsSimulate = cache.Get<Simulate>("Simulate");
                    decimal vlDiscount = (wsSimulate != null) ? wsSimulate.VlDiscount : 0;
                    DateTime? dtFirstParcel = (wsSimulate != null) ? wsSimulate.DtFirstParcel : DateTime.Today.AddDays(4);

                    //if (wsAgreement == null)
                    //{
                    if (termo.ValueEntrance > 0 && termo.Age > AppSettings.MaxPromisse)
                        dtFirstParcel = termo.DateParcel;

                    var agreement = new Agreement()
                    {
                        IdProduct = wsProduct.IdProduct,
                        IdAccount = _produto.IdAccount,
                        VlEntrace = termo.ValueEntrance,
                        DtEntrace = Convert.ToDateTime(termo.DateEntranceParcel),
                        NrParcel = termo.Age <= AppSettings.MaxPromisse ? 0 : termo.NrParcel,
                        VlParcel = termo.ValueParcel,
                        DtFirstParcel = dtFirstParcel,
                        FlPromisse = termo.Age <= AppSettings.MaxPromisse ? true : false,
                        FlAccept = true,
                        VlDiscount = vlDiscount,
                        DtInsert = DateTime.Now,
                    };
                    var _wsAgreement = AfinzAPI.SetAgreement(agreement);
                    cache.AddCache("Agreement", _wsAgreement);
                    //}
                    //End Log
                    #endregion LOG AGREEMENT IBI

                    //return RedirectToAction("Confirmacao-De-Endereco", "Consulta-Documento");

                    var agreementSimulateRequest = new AgreementSimulateRequest()
                    {
                        /*
                        ComplementData = new List<ComplementData>()
                        {
                            new ComplementData() { Name = "negociacao_id", Value = conta.ComplementData.Where(p => p.Name == "negociacao_id").FirstOrDefault().Value },
                            new ComplementData() { Name = "id", Value = conta.ComplementData.Where(p => p.Name == "id_parcela_original").FirstOrDefault().Value },
                            new ComplementData() { Name = "numero", Value = conta.ComplementData.Where(p => p.Name == "numero_parcela_original").FirstOrDefault().Value },
                            new ComplementData() { Name = "vencimento", Value = conta.DtDue.Value.ToString("yyyy-MM-dd") },
                            new ComplementData() { Name = "valor", Value = conta.VlDue.ToString("N2") }
                        },
                        */
                        ParcelaCredz = conta.ParcelaCredz,

                        DtEntrace = termo.DateEntranceParcel,
                        VlEntrace = termo.ValueEntrance,
                        Age = conta.Age,
                        NrParcel = termo.NrParcel + 1,
                        PctDiscount = vlDiscount,
                        CPF = pessoa.CPF,
                        Product = conta.Account,
                    };

                    var acordoCredz = FisAPI.SetAgreementCredz(agreementSimulateRequest);
                    cache.AddCache("ACORDOCREDZ", acordoCredz);
                    try
                    {
                        string descripton = String.Format("Acordo via portal com entrada para o dia {0} no valor de R${1}", termo.DateEntranceParcel.ToString("dd/MM/yyyy"), termo.ValueEntrance.ToString("N2"));
                        if (termo.NrParcel > 1)
                            descripton += String.Format(" e {0} parcelas de {1}", termo.NrParcel, termo.ValueParcel.ToString("N2"));

                        var statusLead = new StatusLead()
                        {
                            IdLead = conta.IdLead,
                            IdStatus = 66,
                            DsDescription = descripton,
                            DtInsert = DateTime.Now,
                            IdUserLogin = 1,
                            Agreement = new List<AgreementAdd>()
                            {
                                {
                                    Util.CreateAgreementAdd(acordoCredz)
                                }
                            }
                        };

                        var newStatuLead = FisAPI.PostStatusLead(statusLead, pessoa.CPF, pessoa.Phones.FirstOrDefault().NrPhone, 3, "");

                        if (newStatuLead != null)
                            cache.AddCache<StatusLeadResponse>("NEW_STATUSLEAD", newStatuLead);

                    }
                    catch { }

                    return RedirectToAction("Finalizar", "Consulta-Documento");
                }
                return RedirectToAction("CentralCobranca");
            }
            catch (Exception e)
            {
                return RedirectToAction("FormaDePagamento");
            }
        }

        [Route("Finalizar")]
        public IActionResult Finalizar()
        {
            try
            {
                string titulo = "Negociação Finalizada! Segue abaixo boleto para pagamento.";
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                if (model == null)
                    return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || !Autenticated())
                    return RedirectToAction("Index", "Home");

                StatusLeadResponse statusLead = cache.Get<StatusLeadResponse>("NEW_STATUSLEAD");
                PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");
                BilletResponse boleto = null;

                if (statusLead != null)
                {
                    boleto = statusLead.AgreementResponse.AgreementParcelResponse.Where(p => p.NrParcel == 0).FirstOrDefault().BilletResponse.FirstOrDefault();

                    if (boleto == null)
                    {

                        try
                        {
                            var billetRequest = new BilletRequest()
                            {
                                IdAgreement = statusLead.AgreementResponse.IdAgreement,
                                NrParcel = 0,
                                IdPromisse = 0,
                                CdAgreement = statusLead.AgreementResponse.CdAgreement,
                                VlBillet = statusLead.AgreementResponse.VlEntrace,
                                DtBillet = statusLead.AgreementResponse.DtEntrace,
                                CPF = cache.Get<PersonResponse>("Pessoa").CPF
                            };

                            boleto = FisAPI.AddBillet(billetRequest, 1, "");

                        }
                        catch (Exception ex)
                        {

                        }
                    }


                    if (boleto != null)
                    {
                        cache.AddCache("boleto", boleto);
                        Navigation wsNavigation = cache.Get<Navigation>("Navigation");
                        Produto _produto = cache.Get<Produto>("produto");
                        var conta = pessoa.Cards.Where(p => p.Account.Contains(_produto.CodProduto)).FirstOrDefault();

                        if (wsNavigation != null)
                        {
                            string cpf = Regex.Replace(model.CpfCnpj, @"\D", "");
                            Billet billet = new Billet()
                            {
                                Account = conta.Account,
                                CPF = cpf,
                                FlPrint = true,
                                Age = Convert.ToInt32(conta.Age),
                                CodeBar = boleto.Line,
                                VlBillet = boleto.VlBillet,
                                DtInsert = DateTime.Now,
                            };
                            var wsBillet = AfinzAPI.SetBillet(billet);
                        }
                    }
                }

                //IList<object> data = new List<object> { model, boleto, statusLead.AgreementResponse, statusLead.AgreementResponse.AgreementParcelResponse.FirstOrDefault(), titulo };
                IList<object> data = new List<object> { model, boleto, titulo, pessoa.Cards.Count() };

                return View(data);
            }
            catch (Exception e)
            {
                ViewData["Message"] = new List<string> { "Não foi possível obter o boleto para esta conta!" };

                return View("Encerrado", new List<object> { cache.Get<ConsultaCpfCnpj>("model") });
            }
            finally
            {
                cache.Reset();
            }
        }
        [HttpPost]
        [Route("Finalizar")]
        public IActionResult Finalizar(CardResponse conta = null)
        {
            try
            {
                /// clique botão segunda via
                string titulo = "Segunda Via";
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                if (model == null)
                    return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || !Autenticated())
                    return RedirectToAction("Index", "Home");

                BilletResponse boletoGerado = new BilletResponse();


                PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");
                conta = pessoa.Cards.Where(p => p.Account.Contains(conta.Account)).FirstOrDefault();
                var acordo = conta.StatusLeadResponse.OrderByDescending(p => p.AgreementResponse.IdAgreement).Select(p => p.AgreementResponse).FirstOrDefault();
                var parcelaAtual = acordo.AgreementParcelResponse.Where(p => p.Status == "EmAberto" && p.DtParcel >= DateTime.Today.AddDays(-10)).OrderBy(p => p.DtParcel).FirstOrDefault();

                cache.Remove("produto");
                cache.AddCache("produto", new Produto() { CodProduto = conta.Account, NomeProduto = "IBI" });

                BilletResponse boleto = null;
                if (parcelaAtual != null && parcelaAtual.BilletResponse.Count > 0)
                {
                    boleto = parcelaAtual.BilletResponse.OrderByDescending(p => p.IdBillet).FirstOrDefault();
                }
                else
                {
                    var billetRequest = new BilletRequest()
                    {
                        IdProduct = conta.IdProduct,
                        IdAgreement = acordo.IdAgreement,
                        NrParcel = parcelaAtual.NrParcel + 1,
                        IdPromisse = 0,
                        CdAgreement = acordo.CdAgreement,
                        VlBillet = parcelaAtual.VlParcel,
                        DtBillet = DateTime.Today,
                        IdAgreementParcel = parcelaAtual.IdAgreementParcel,
                        CPF = pessoa.CPF
                    };
                    boleto = FisAPI.AddBillet(billetRequest, 3, "");
                }
                if (boleto != null)
                {
                    cache.AddCache("boleto", boleto);
                    boletoGerado = boleto;

                    Navigation wsNavigation = cache.Get<Navigation>("Navigation");
                    if (wsNavigation != null)
                    {
                        string cpf = Regex.Replace(model.CpfCnpj, @"\D", "");
                        Billet billet = new Billet()
                        {
                            Account = conta.Account,
                            CPF = cpf,
                            FlPrint = true,
                            Age = Convert.ToInt32(conta.Age),
                            CodeBar = boleto.Line,
                            VlBillet = boleto.VlBillet,
                            DtInsert = DateTime.Now,
                        };
                        var wsBillet = AfinzAPI.SetBillet(billet);
                    }
                }

                //IList<object> data = new List<object> { model, boletoGerado, acordo, parcelaAtual, titulo };
                IList<object> data = new List<object> { model, boletoGerado, titulo, pessoa.Cards.Count() };

                return View(data);
            }
            catch (Exception e)
            {
                ViewData["Message"] = new List<string> { "Não foi possível obter o boleto para esta conta!" };

                return View("Encerrado", new List<object> { cache.Get<ConsultaCpfCnpj>("model") });

                // return View("Encerrado", new List<object> { cache.Get<ConsultaCpfCnpj>("model") });
            }

        }

        [Route("Obrigado")]
        public IActionResult Obrigado()
        {
            ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
            if (model == null)
                return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");
            if (model == null || string.IsNullOrEmpty(model.CpfCnpj))
            {
                return RedirectToAction("Index", "Home");
            }
            IList<object> data = new List<object> { model };
            return View(data);
        }

        [Route("Acordo-Concretizado")]
        public IActionResult AcordoConcretizado()
        {
            return null;
            /*
            ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
            if (model == null)
                return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");
            if (model == null || string.IsNullOrEmpty(model.CpfCnpj))
            {
                return RedirectToAction("Index", "Home");
            }

            Produto _produto = cache.Get<Produto>("produto");
            PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");
            Utils.API.Ibi.Conta conta = new Utils.API.Ibi.Conta();
            conta = pessoa.Contas.Where(p => p.NrCartao == _produto.CodProduto).FirstOrDefault();

            ICollection<Agreement> listAgreement = cache.Get<ICollection<Agreement>>("ListAgreement");

            IList<object> data = new List<object> { model, pessoa, conta, listAgreement };
            return View(data);
            */
        }

        [Route("Sessao-Expirada")]
        public IActionResult SessaoExpirada()
        {
            IList<object> data = new List<object> { new ConsultaCpfCnpj() };
            return View(data);
        }

        [Route("Nada-Consta")]
        public IActionResult NadaConsta()
        {
            ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
            if (model == null || string.IsNullOrEmpty(model.CpfCnpj))
            {
                return RedirectToAction("Index", "Home");
            }
            IList<object> data = new List<object> { model };
            return View(data);
        }

        [Route("Acordo")]
        public IActionResult Acordo()
        {
            try
            {
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                if (model == null)
                    return RedirectToAction("SessaoExpirada", "ConsultaCpfCnpj");

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || !Autenticated())
                    return RedirectToAction("Index", "Home");

                Produto _produto = cache.Get<Produto>("produto");

                //if (!VerifyOperation.ValidOperation())
                //    return RedirectToAction("Encerrado", "ConsultaCpfCnpj");

                string cpfCnpj = Regex.Replace(model.CpfCnpj, @"[^\d]", "");


                BilletResponse boletoGerado = new BilletResponse();

                string linhaDigitavel = null;

                if (_produto != null && _produto.NomeProduto == "IBI")
                {
                    PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");
                    CardResponse conta = pessoa.Cards.Where(p => p.CardNumber.Contains(_produto.CodProduto)).FirstOrDefault();

                    if (conta != null)
                    {
                        cache.AddCache("boleto", boletoGerado);
                    }
                }

                //DADOS PARA ACEITAÇÃO DO TERMO E FINALIZAÇÃO DO ACORDO
                IList<object> data = new List<object> { model, _produto, linhaDigitavel };
                return View(data);
            }
            catch (Exception e)
            {
                return RedirectToAction("CentralCobranca");
            }
        }

        [Route("Imprimir")]
        public IActionResult Imprimir(string urlPDF)

        {
            try
            {
                BilletResponse boleto = cache.Get<BilletResponse>("boleto");

                if (!string.IsNullOrEmpty(urlPDF))
                {
                    return Redirect(urlPDF);
                }
                else if (!string.IsNullOrEmpty(boleto.URL))
                {
                    return Redirect(boleto.URL);
                }
                else
                {
                    ViewData["Message"] = new List<string> { "Não foi possível obter o boleto para esta conta!" };
                    return View("Encerrado", new List<object> { cache.Get<ConsultaCpfCnpj>("model") });
                }

            }
            catch
            {
                ViewData["Message"] = new List<string> { "Não foi possível obter o boleto para esta conta!" };
                return View("Encerrado", new List<object> { cache.Get<ConsultaCpfCnpj>("model") });
            }
        }

        [HttpGet]
        [Route("Consulta-Cep")]
        public JsonResult ConsultaCep(string cep)
        {
            GoogleMapsApi.EnderecoGoogleMaps endereco = GoogleMapsApi.GetEnderecoByCep(cep);
            return Json(endereco);
        }

        [HttpPost]
        [Route("Remove-Telefone")]
        public JsonResult RemoveTelefone(string phone)
        {
            return null;
            /*
            try
            {
                string nphone = Regex.Replace(phone, @"\D", "");
                //IbiService.Pessoa pessoa = cache.Get<IbiService.Pessoa>("pessoa");
                PersonResponse pessoa = cache.Get<PersonResponse>("pessoa");

                var t = pessoa.Phones.Where(p => p.NrPhone == nphone).FirstOrDefault();
                pessoa.Phones.Remove(t);
                //IbiService.URAClient client = new IbiService.URAClient();
                //long idAccount = client.SetDataPersonAsync(pessoa).Result;
                long idAccount = HttpHelper.POST<long>(Utils.API.Ibi.Uri.SetDataPerson(), pessoa);

                if (idAccount > 0)
                    return Json(true);
                else
                    return Json(false);
            }
            catch (Exception)
            {
                return Json(false);
            }
            */
        }

        [HttpPost]
        [Route("Adicionar-Telefone")]
        public JsonResult AdicionaTelefone(string phone)
        {
            return null;
            /*
            try
            {
                Produto _produto = cache.Get<Produto>("produto");
                string nphone = Regex.Replace(phone, @"\D", "");
                bool idAccount = false;
                PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");
                if (_produto.NomeProduto == "IBI")
                {
                    //IbiService.Pessoa pessoa = cache.Get<IbiService.Pessoa>("pessoa");
                    if (pessoa.Phones != null)
                    {
                        var t = pessoa.Phones.Where(p => p.NrTelefone == nphone).FirstOrDefault();
                        if (t == null)
                        {
                            pessoa.Phones.Add(new Utils.API.Ibi.Telefone
                            {
                                TipoTelefone = (phone.Length == 10) ? 1 : 2,
                                NrTelefone = nphone
                            });
                        }
                    }
                    else
                    {
                        pessoa.Phones = new List<Utils.API.Ibi.Telefone>
                        {
                            new Utils.API.Ibi.Telefone
                            {
                                TipoTelefone = (phone.Length == 10) ? 1 : 2,
                                NrTelefone = nphone
                            }
                        };
                    }

                    //IbiService.URAClient client = new IbiService.URAClient();
                    //idAccount = client.SetDataPersonAsync(pessoa).Result != 0;
                    idAccount = HttpHelper.POST<long>(Utils.API.Ibi.Uri.SetDataPerson(), pessoa) != 0;

                }
                if (idAccount)
                {
                    cache.AddCache("Pessoa", pessoa);
                    return Json(true);
                }
                else
                    return Json(false);
            }
            catch (Exception)
            {
                return Json(false);
            }
            */
        }

        [HttpGet]
        [Route("Get-Parcelas")]
        public JsonResult GetParcelas(string valor)
        {
            string vl = (valor != null) ? valor.Replace(".", "").Replace(",", "") : "0";

            IList<object> l0 = new List<Object>{
                              new { value = 0 , text = "A Vista"  }
                                };

            IList<object> l1 = new List<Object>{
                               new { value = 0 , text = "A Vista" },
                               new { value = 1 , text = "1 parcela" },
                               new { value = 2 , text = "2 parcelas"},
                               new { value = 3 , text = "3 parcelas"},
                               new { value = 4 , text = "4 parcelas"},
                               new { value = 5 , text = "5 parcelas"},
                               new { value = 6 , text = "6 parcelas"},
                               new { value = 7 , text = "7 parcelas"},
                               new { value = 8 , text = "8 parcelas"},
                               new { value = 9 , text = "9 parcelas"},
                               new { value = 10 , text = "10 parcelas"},
                               new { value = 11 , text = "11 parcelas"},
                               new { value = 12 , text = "12 parcelas"},
                               new { value = 13 , text = "13 parcelas"},
                               new { value = 14 , text = "14 parcelas"},
                               new { value = 15 , text = "15 parcelas"},
                               new { value = 16 , text = "16 parcelas"},
                               new { value = 17 , text = "17 parcelas"},
                               new { value = 18 , text = "18 parcelas"},
                               new { value = 19 , text = "19 parcelas"},
                               new { value = 20 , text = "20 parcelas"},
                               new { value = 21 , text = "21 parcelas"},
                               new { value = 22 , text = "22 parcelas"},
                               new { value = 23 , text = "23 parcelas"},
                               new { value = 24 , text = "24 parcelas"} };

            //if (Convert.ToInt64(vl) / 100 > 0)
            return Json(l1);
            //else
            //    return Json(l0);


        }

        [HttpPost]
        [Route("Envia-Sms")]
        public JsonResult EnviarSms(string phone)
        {
            try
            {
                string nphone = Regex.Replace(phone, @"\D", "");
                Produto _produto = cache.Get<Produto>("produto");
                if (_produto.NomeProduto == "IBI")
                {
                    try
                    {
                        PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");
                        BilletResponse boleto = cache.Get<BilletResponse>("boleto");

                        ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                        Navigation wsNavigation = cache.Get<Navigation>("Navigation");
                        if (wsNavigation != null)
                        {
                            if (!string.IsNullOrEmpty(nphone))
                            {
                                string cpf = Regex.Replace(model.CpfCnpj, @"\D", "");
                                if (pessoa != null)
                                {
                                    var conta = pessoa.Cards.Where(p => p.Account.Contains(_produto.CodProduto)).FirstOrDefault();
                                    if (conta != null)
                                    {
                                        //var ibiClient = new URAClient();
                                        //string x = ibiClient.SendBilletWebSiteAsync(DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"), uR86Detail.SaldoDevedor.ToString("N2"), uR86Detail.NumeroConta, cpf, boletoEmail.Email).Result;

                                        string cpfCnpj = Regex.Replace(model.CpfCnpj, @"[^\d]", "");

                                        Agreement agreement = cache.Get<Agreement>("Agreement");
                                        if (agreement != null)
                                        {
                                            DateTime dtEntrace = agreement.DtEntrace;
                                            decimal value = (agreement.VlEntrace > 0) ? agreement.VlEntrace : agreement.VlParcel;
                                        }
                                        /*
                                        var billetRequest = new SendSMSRequest
                                        {
                                            idProduct = agreement.IdProduct,
                                            cpf = cpf,
                                            codBillet = boleto.CdAgreement,
                                            parcel = boleto.Parcel,
                                            dtPayment = boleto.DtBillet,
                                            phone = nphone,
                                            idUserLogin = 1
                                        };
                                        var billetResponse = FisAPI.SendBilletSMS(billetRequest, 1, "");

                                        */

                                        var ret = FisAPI.SendBilletSMS
                                            (
                                            new SMSRequest() { phone = nphone, message = "Segue linha digitavel para pagamento " + (boleto != null ? boleto.Line : cache.Get<string>("linhaDigitavel")) }
                                            , 3, "");

                                        #region Log Billet
                                        if (wsNavigation != null && ret.ToUpper() == "OK")
                                        {
                                            Billet billet = new Billet()
                                            {
                                                Account = conta.Account,
                                                CPF = cpfCnpj,
                                                FlPrint = false,
                                                Age = Convert.ToInt32(conta.Age),
                                                CodeBar = boleto.Line,
                                                VlBillet = boleto.VlBillet,
                                                DtInsert = DateTime.Now,
                                                Phone = nphone
                                            };
                                            var wsBillet = AfinzAPI.SetBillet(billet);
                                        }
                                        #endregion Log Billet

                                        return Json(true);
                                    }
                                }
                            }
                            //cache.AddCache("AcordoSegundaVia", acordoSegundaVia);
                        }
                        return Json(false);
                    }
                    catch
                    {
                        return Json(false);
                    }
                }

                return Json(true);

                return Json(false);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }

        [HttpPost]
        [Route("Envia-Email")]
        public JsonResult EnviarEmail(string email)
        {
            try
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                {
                    Produto _produto = cache.Get<Produto>("produto");
                    if (_produto.NomeProduto == "IBI")
                    {
                        try
                        {
                            PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");
                            BilletResponse boleto = cache.Get<BilletResponse>("boleto");

                            ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                            Navigation wsNavigation = cache.Get<Navigation>("Navigation");
                            if (wsNavigation != null)
                            {
                                if (!string.IsNullOrEmpty(email) && Util.IsEmail(email))
                                {
                                    string cpf = Regex.Replace(model.CpfCnpj, @"\D", "");
                                    if (pessoa != null)
                                    {
                                        var conta = pessoa.Cards.Where(p => p.Account.Contains(_produto.CodProduto)).FirstOrDefault();
                                        if (conta != null)
                                        {
                                            //var ibiClient = new URAClient();
                                            //string x = ibiClient.SendBilletWebSiteAsync(DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"), uR86Detail.SaldoDevedor.ToString("N2"), uR86Detail.NumeroConta, cpf, boletoEmail.Email).Result;
                                            Agreement agreement = cache.Get<Agreement>("Agreement");
                                            DateTime dtEntrace = agreement != null ? agreement.DtEntrace : DateTime.Today.AddDays(4);
                                            decimal value = agreement != null ? ((agreement.VlEntrace > 0) ? agreement.VlEntrace : agreement.VlParcel) : conta.VlFull;
                                            string cpfCnpj = Regex.Replace(model.CpfCnpj, @"[^\d]", "");

                                            var billetRequest = new SendEmailRequest
                                            {
                                                idProduct = agreement != null ? agreement.IdProduct : conta.IdProduct,
                                                cpf = cpf,
                                                nrConta = conta.AccountNumber,
                                                codBillet = boleto != null ? boleto.IdBillet.ToString() : "",
                                                parcel = boleto != null ? boleto.Parcel : 0,
                                                dtPayment = boleto != null ? boleto.DtBillet : DateTime.Today.AddDays(4),
                                                vlBillet = boleto != null ? boleto.VlBillet : conta.VlFull,
                                                line = boleto != null ? boleto.Line : cache.Get<string>("linhaDigitavel"),
                                                urlPdf = boleto.URL,
                                                email = email,
                                                idUserLogin = 1
                                            };
                                            var billetResponse = FisAPI.SendBilletEmail(billetRequest, 3, "");

                                            #region Log Billet
                                            if (wsNavigation != null)
                                            {
                                                Billet billet = new Billet()
                                                {
                                                    Account = conta.Account,
                                                    CPF = cpfCnpj,
                                                    FlPrint = false,
                                                    Age = Convert.ToInt32(conta.Age),
                                                    CodeBar = billetResponse.Line,
                                                    VlBillet = billetResponse.VlBillet,
                                                    DtInsert = DateTime.Now,
                                                    Email = email
                                                };
                                                var wsBillet = AfinzAPI.SetBillet(billet);
                                            }
                                            #endregion Log Billet

                                            return Json(true);
                                        }
                                    }
                                }
                                //cache.AddCache("AcordoSegundaVia", acordoSegundaVia);
                            }
                            return Json(false);
                        }
                        catch
                        {
                            return Json(false);
                        }
                    }

                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }

        [Route("Atendimento-Encerrado")]
        public ActionResult Encerrado()
        {
            IList<object> data = new List<object> { new ConsultaCpfCnpj() };
            return View(data);
        }

        [HttpPost]
        [Route("Termos-Do-Acordo")]
        public ActionResult TermosDoAcordo(string totalParcel)
        {
            try
            {
                Produto _produto = cache.Get<Produto>("produto");
                SimulaParcelamento simulaParcelamento = cache.Get<SimulaParcelamento>("simulaParcelamento");

                PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");
                CardResponse conta = new CardResponse();

                TermoECondicao termo = new TermoECondicao();

                if (_produto != null && _produto.NomeProduto == "IBI")
                {
                    //Se for IBI, encerra operação
                    // if (!VerifyOperation.ValidOperation())
                    //     return RedirectToAction("Encerrado", "ConsultaCpfCnpj");


                    conta = pessoa.Cards.Where(p => p.Account == _produto.CodProduto).FirstOrDefault();
                    if (Convert.ToInt32(conta.Age) < 2)
                        return RedirectToAction("CentralCobranca");

                    AgreementSimulateResponse parcelamento = cache.Get<AgreementSimulateResponse>("parcelamento");

                    ParcelResponse parcela = null;
                    if (parcelamento != null)
                        parcela = parcelamento.ParcelResponse.Where(p => p.NrParcel.ToString() == totalParcel).FirstOrDefault();

                    DateTime dataEntrada = DateTime.Now;
                    if (simulaParcelamento != null)
                        if (!DateTime.TryParse(simulaParcelamento.DataEntrada, out dataEntrada)) { }
                    string vlEntrada = parcela != null ? parcela.ValueEntrace.ToString("N2") : "0";
                    vlEntrada = Regex.Replace(vlEntrada, @"\D", "");

                    termo = new TermoECondicao
                    {
                        Age = Convert.ToInt32(conta.Age),
                        NrParcel = Convert.ToInt32(totalParcel),
                        DateEntranceParcel = dataEntrada,
                        DateParcel = parcela != null ? parcela.DtParcel : DateTime.Today
                    };

                    if (parcelamento != null && conta.Age > AppSettings.MaxPromisse)
                    {
                        if (parcela != null)
                        {
                            //Discount discount = new Discount();
                            //if (simulaParcelamento != null)
                            //    discount = HttpHelper.GET<Discount>(Utils.API.Ibi.Uri.GetDiscount(conta.Age.ToString(), simulaParcelamento.Parcela.ToString(), conta.TipoConta));

                            //if (discount == null)
                            //    discount = new IbiService.Discount();
                            //    discount = new Discount();

                            decimal cetAnual = parcela.VlYearCET;
                            decimal cetMensal = parcela.VlMonthCET;


                            termo.CETAnual = cetAnual;
                            termo.CETMensal = cetMensal;
                            termo.ValueParcel = parcela.VlParcel;

                            termo.VlCETAnual = Convert.ToDecimal(parcela.NrParcel * parcela.VlParcel * cetAnual) / 100;
                            termo.VlCETMensal = Convert.ToDecimal(parcela.NrParcel * parcela.VlParcel * (cetMensal));

                        }
                        if (vlEntrada != "0")
                        {
                            termo.DateParcel = dataEntrada.AddMonths(1);
                            if (parcelamento != null)
                                termo.PercentageTax = parcelamento.PctInterest;
                            termo.ValueEntrance = parcela.ValueEntrace;
                        }
                        else
                        {
                            termo.DateParcel = parcela.DtParcel;
                            if (parcelamento != null)
                                termo.PercentageTax = parcelamento.PctInterest;
                        }
                    }
                    else
                    {

                    }


                    //Para promessa
                    int NrParcelLog = termo.NrParcel;
                    bool FlPromisseLog = false;

                    if (termo.Age <= AppSettings.MaxPromisse)
                    {
                        if (termo.NrParcel == 0)
                            termo.ValueEntrance = conta.VlMinimum;
                        else if (termo.NrParcel == 1)
                            termo.ValueEntrance = conta.VlFull;
                        else
                            termo.ValueEntrance = Convert.ToDecimal(simulaParcelamento.Entrada);

                        NrParcelLog = 0;
                        FlPromisseLog = true;
                    }

                    string html = "<div class=\"content-lightbox padding-top-40 box-forma-de-pagamento\" id=\"consulta-cpf-termos-e-condicoes\">";
                    html += "<div class=\"box-termos\">";
                    if (termo.Age > AppSettings.MaxPromisse)
                    {
                        /*
                        //termo do acordo
                        html += "<p>";
                        html += "As condições anteriormente propostas são válidas apenas para esta negociação realizada neste canal na data de ";
                        html += DateTime.Now.ToString("dd/MM/yyyy") + ". O desconto concedido somente será valido se o pagamento for feito em sua totalidade e até a data de vencimento. " +
                            "Caso não ocorra o pagamento da primeira parcela, o acordo será quebrado e a cobrança retomada em sua totalidade.</p>";
                        html += "<p>Após o pagamento da primeira parcela sua negociação será processada e a reabilitação de seu CPF junto aos órgãos de proteção ao crédito(SPC / SERASA) ocorrerá em até 5 dias úteis.</p>";
                        html += "<p>Se houver dúvidas, por favor, entre em contato com a nossa Central de Atendimento:</p>";
                        //pegar telefone da Afinz
                        html += "<p> 4003 4031(Capitais e Regiões Metropolitanas) ou 0800 880 4031(demais regiões).</p>";
                        html += "<p>" +
                            "<br/><strong> Lembrando </strong><br/><br/>";
                        */

                        if (termo.NrParcel == 0)
                        {
                            html += "Fica formalizado o ACORDO à vista no valor de " +
                                termo.ValueParcel.ToString("C2") + " com vencimento para o dia " +
                                termo.DateEntranceParcel.Date.ToString("dd/MM/yyyy") + ".";
                        }
                        else
                        {
                            if (termo.ValueEntrance == 0 && termo.NrParcel > 1)
                            {
                                html += "Fica formalizado o ACORDO SEM ENTRADA com primeiro vencimento para o dia " +
                                    termo.DateEntranceParcel.Date.ToString("dd/MM/yyyy") + " no valor de " + termo.ValueParcel.ToString("C2") + ",  mais " + (termo.NrParcel - 1).ToString();

                                if (termo.NrParcel - 1 > 1)
                                    html += " parcelas de ";
                                else
                                    html += " parcela de ";

                                html += termo.ValueParcel.ToString("C2") + " e demais vencimentos para todo dia " + termo.DateParcel.Day.ToString() + ".";
                            }
                            else
                            {
                                html += "Fica formalizado o ACORDO COM ENTRADA para dia ";
                                html += termo.DateEntranceParcel.Date.ToString("dd /MM/yyyy");
                                html += " no valor de ";
                                html += termo.ValueEntrance.ToString("C2");
                                html += " mais ";
                                html += termo.NrParcel;

                                if (termo.NrParcel > 1)
                                    html += " parcelas de ";
                                else
                                    html += " parcela de ";

                                html += termo.ValueParcel.ToString("C2");
                                html += " com vencimentos para todo dia ";
                                html += termo.DateParcel.Day;
                                html += ".";
                            }
                        }

                        html += "<br />Lembrando que só ocorrerá a efetivação do acordo e a retirada da negativação do seu CPF dos orgão de proteção de crédito após constar o pagamento ";
                        if (termo.NrParcel == 0)
                            html += "do boleto do acordo!";
                        else
                            html += "do boleto da entrada do acordo!";


                        html += "</p>";

                        html += "<p><br/>Agradecemos sua atenção. Tenha ";
                        if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
                        {
                            html += "uma Boa Tarde.";
                        }
                        else if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour <= 23)
                        {
                            html += "uma Boa Noite.";
                        }
                        else
                        {
                            html += "um Bom Dia.";
                        }
                        html += "</p>";
                    }
                    else
                    {
                        if (Convert.ToInt16(totalParcel) <= 1)
                        {
                            html += "<p>";
                            html += "Ao clicar em CONFIRMAR, sua PROMESSA será registrada em nossos sistemas.";
                            html += "</p>";
                            html += "<p>";
                            html += "Será formalizado uma PROMESSA de pagamento de sua fatura no valor de ";
                            html += termo.ValueEntrance.ToString("C2");
                            html += " com pagamento até o dia ";
                            html += termo.DateEntranceParcel.Date.ToString("dd/MM/yyyy");
                            html += ".";
                            html += "</p>";
                            html += "<p>Tem certeza que deseja formalizar esta PROMESSA?</p>";
                        }
                        else
                        {
                            var parcelamentoOpcao = cache.Get<ParcelamentoFaturaResponse>("simulaParcelamentoFatura").responseData.listaParcelamentoOpcao.Where(p => p.quantidadeParcelas == totalParcel).FirstOrDefault();
                            html += "<p>";
                            html += "Ao clicar em CONFIRMAR, o PARCELAMENTO DE SUA FATURA será registrada em nossos sistemas.";
                            html += "</p>";
                            html += "<p>";
                            html += "Será formalizado o PARCELAMENTO DE SUA FATURA com entrada de ";
                            html += termo.ValueEntrance.ToString("C2") + " + " + parcelamentoOpcao.quantidadeParcelas;
                            html += " parcelas de R$" + parcelamentoOpcao.valorParcela;
                            html += " O pagamento da entrada deve ser realizado até o dia ";
                            html += termo.DateEntranceParcel.Date.ToString("dd/MM/yyyy");
                            html += " para o parcelamento ser efetivado.";
                            html += "</p>";
                            html += "<p>Tem certeza que deseja formalizar o PARCELAMENTO DE SUA FATURA?</p>";
                        }
                    }

                    html += "</div>";
                    html += "</div>";

                    cache.AddCache("termo", termo);
                    return Content(html, "text/html");
                }
                else
                {
                    return Content("<h2>Falha ao gerar os termos do acordo.<br/>Tente novamente mais tarde</h2>", "text/html");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route("Autenticar-Acesso")]
        public async Task<ActionResult> Validacao(ValidationAccess _validation)
        {
            try
            {
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");
                int total = cache.Get<int>("TentativaValidacao") != null && cache.Get<int>("TentativaValidacao") > 0 ? cache.Get<int>("TentativaValidacao") : 0;
                cache.Remove("TentativaValidacao");
                cache.AddCache<int>("TentativaValidacao", total + 1);

                if (model == null)
                    return RedirectToAction("Index", "Home");

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || model == null || string.IsNullOrEmpty(model.CpfCnpj))
                    return RedirectToAction("Index", "Home");

                //if (!VerifyOperation.ValidOperation())
                ///    return RedirectToAction("Encerrado", "ConsultaCpfCnpj");

                string cpf = Regex.Replace(model.CpfCnpj, @"[^\d]", "");

                if (ModelState.IsValid)
                {

                    //if (string.IsNullOrEmpty(_validation.Nome))
                    //{
                    //Inserir Tabela de acesso inválido
                    //    TempData["Message"] = "Dados inválidos. Tente novamente.";
                    //    return RedirectToAction("Validacao");
                    //}

                    //var isCaptchaValid = await IsCaptchaValid(_validation.GoogleCaptchaToken);
                    //if (isCaptchaValid)
                    //{

                    if (pessoa != null && pessoa.Cards.Any())
                    {
                        string nome = pessoa.Name.Trim();
                        //string dtNasc = ur80.DataNascimentoTitular;
                        if (!string.IsNullOrEmpty(nome))
                        {
                            string[] primeiroNome = pessoa.Name.Trim().Split(' ');
                            if (_validation.Nome == primeiroNome[0])
                            {
                                cache.Remove("Validacao");
                                cache.AddCache<bool>("Validacao", true);

                                return RedirectToAction("ValidacaoData");
                            }
                            else
                            {
                                cache.Remove("Validacao");
                                cache.AddCache<bool>("Validacao", false);

                                return RedirectToAction("ValidacaoData");

                                //TempData["Message"] = "Nome incorreto. Por favor, selecione o nome que corresponda ao seu primeiro nome.";
                                //return RedirectToAction("Validacao");
                            }
                        }
                        else
                        {
                            return RedirectToAction("Encerrado", "ConsultaCpfCnpj");
                        }
                    }
                    else
                    {
                        return RedirectToAction("NadaConsta");
                    }
                    //}
                    //else
                    //{
                    //    TempData["Message"] = "Validação Google reCAPTCHA inválida. Você parece ser um robô ou automação!";
                    //    return RedirectToAction("Validacao");
                    //}
                }
                else
                {
                    TempData["Message"] = "Dados inválidos. Tente novamente.";
                    return RedirectToAction("Validacao");
                }
            }
            catch (Exception ex) { }
            IList<object> data = new List<object> { new ConsultaCpfCnpj() };
            return View(data);
        }

        [HttpPost]
        [Route("Autenticar-Acesso-Data")]
        public async Task<ActionResult> ValidacaoData(ValidationDate _validation)
        {
            try
            {
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                PersonResponse pessoa = cache.Get<PersonResponse>("Pessoa");

                if (model == null)
                    return RedirectToAction("Index", "Home");

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || model == null || string.IsNullOrEmpty(model.CpfCnpj))
                    return RedirectToAction("Index", "Home");


                if (ModelState.IsValid)
                {

                    if (pessoa != null && pessoa.Cards.Any())
                    {
                        if (_validation.Day == pessoa.DtBirth.Day.ToString() &&
                            _validation.Month == pessoa.DtBirth.Month.ToString() &&
                            _validation.Year == pessoa.DtBirth.Year.ToString() &&
                            cache.Get<bool>("Validacao"))
                        {
                            cache.AddCache("Autenticated", true);
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            //Inserir Tabela de acesso inválido
                            TempData["Message"] = "Dados inválidos. Favor confirme seus dados para acessar a negociação!";
                            return RedirectToAction("Validacao");
                        }
                    }
                    else
                    {
                        return RedirectToAction("NadaConsta");
                    }
                }
                else
                {
                    TempData["Message"] = "Dados inválidos. Tente novamente."; return RedirectToAction("Validacao");
                }
            }
            catch (Exception ex) { }
            IList<object> data = new List<object> { new ConsultaCpfCnpj() };
            return View(data);
        }


        [Route("Autenticar-Acesso-Data")]
        public ActionResult ValidacaoData()
        {
            try
            {
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                if (model == null)
                    return RedirectToAction("Index", "Home");

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || model == null || string.IsNullOrEmpty(model.CpfCnpj))
                    return RedirectToAction("Index", "Home");

                //Se for IBI, encerra operação
                //if (!VerifyOperation.ValidOperation())
                //    return RedirectToAction("Encerrado", "ConsultaCpfCnpj");

                //ViewBag.Recaptcha = ReCaptcha.GetHtml(AppSettings.ReCaptchaSecretKey);
                ViewBag.publicKey = AppSettings.ReCaptchaPublicKey;
                string cpf = Regex.Replace(model.CpfCnpj, @"[^\d]", "");

                PersonResponse pessoa;
                if (cache.Get<PersonResponse>("Pessoa") == null || cache.Get<PersonResponse>("Pessoa").CPF != cpf)
                    pessoa = FisAPI.GetPerson(cpf, 3);
                else
                    pessoa = cache.Get<PersonResponse>("Pessoa");

                if (pessoa.Cards.Any())
                {
                    IList<object> data = new List<object> { new ConsultaCpfCnpj(), new ValidationDate() };

                    if (TempData["Message"] != null)
                    {
                        ViewBag.Message = TempData["Message"];
                    }
                    return View(data);
                }
                else
                {
                    return RedirectToAction("NadaConsta", "ConsultaCpfCnpj");
                }

            }
            catch (Exception ex) { }
            return RedirectToAction("Encerrado", "ConsultaCpfCnpj");
        }

        [Route("Autenticar-Acesso")]
        public ActionResult Validacao()
        {
            try
            {
                ConsultaCpfCnpj model = cache.Get<ConsultaCpfCnpj>("model");
                if (model == null)
                    return RedirectToAction("Index", "Home");

                if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("_sID")) || model == null || string.IsNullOrEmpty(model.CpfCnpj))
                    return RedirectToAction("Index", "Home");

                if (cache.Get<int>("TentativaValidacao") > 3)
                    return RedirectToAction(nameof(CentralCobranca));

                //Se for IBI, encerra operação
                //if (!VerifyOperation.ValidOperation())
                //    return RedirectToAction("Encerrado", "ConsultaCpfCnpj");

                //ViewBag.Recaptcha = ReCaptcha.GetHtml(AppSettings.ReCaptchaSecretKey);
                ViewBag.publicKey = AppSettings.ReCaptchaPublicKey;
                string cpf = Regex.Replace(model.CpfCnpj, @"[^\d]", "");

                PersonResponse pessoa;
                //if (cache.Get<PersonResponse>("Pessoa") == null || cache.Get<PersonResponse>("Pessoa").CPF != cpf)
                pessoa = FisAPI.GetPerson(cpf, 3);
                //else
                //    pessoa = cache.Get<PersonResponse>("Pessoa");
                if (pessoa.Cards.Any(x => x.Age > 77) || pessoa.Cards.Any(c => c.StatusLeadResponse.Any(p => p.AgreementResponse.Status.Contains("EmAndamento") || p.AgreementResponse.Status.Contains("EmAberto"))))
                {
                    //pessoa.Contas = contas.ToList();

                    cache.AddCache("Pessoa", pessoa);
                    string[] primeiroNome = pessoa.Name.Trim().Split(' ');
                    ICollection<string> nomes = AfinzAPI.GetPIDNames(primeiroNome[0]);
                    nomes.Add(primeiroNome[0]);

                    Random rnd = new Random();
                    nomes = nomes.Select(x => new
                    {
                        value = x,
                        order = rnd.Next()
                    })
                        .OrderBy(x => x.order).Select(x => x.value).ToList();

                    IList<object> data = new List<object> { new ConsultaCpfCnpj(), new ValidationAccess(), nomes };

                    if (TempData["Message"] != null)
                    {
                        ViewBag.Message = TempData["Message"];
                    }
                    return View(data);
                }
                else
                {
                    return RedirectToAction("NadaConsta", "ConsultaCpfCnpj");
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("CPF não encontrado"))
                {
                    return RedirectToAction("NadaConsta", "ConsultaCpfCnpj");
                }
            }
            return RedirectToAction("Encerrado", "ConsultaCpfCnpj");
        }

        [HttpGet]
        [Route("TokenVerify")]
        private async Task<bool> IsCaptchaValid(string response)
        {
            try
            {
                string secret = AppSettings.ReCaptchaSecretKey;
                using (var client = new HttpClient())
                {

                    var verify = await client.GetStringAsync($"{AppSettings.ReCaptchaApi}?secret={secret}&response={response}");
                    var captchaResult = JsonConvert.DeserializeObject<TokenResponseModel>(verify);

                    return captchaResult.Success
                        && captchaResult.Action == "homepage"
                        && captchaResult.Score > 0;//Convert.ToDecimal(0.5);

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("CartoesCredz")]
        public ActionResult CartoesCredz(string cartao)
        {
            try
            {
                string html = "<div class=\"content-lightbox padding-top-40 box-forma-de-pagamento\" id=\"cartoes-credz\">";

                html += "<div class=\"box-termos\">";

                var list = FisAPI.GetProductSpecification(3);
                foreach (var item in list.Select(p => p.UrlImage).Distinct().ToList())
                {
                    if (!item.Contains("cardgenerico-v2.png"))
                        html += "<p> <img src=\"" + item + "\" style=\"max-width:280px;\" /> </p>";
                }
                // html += "<p>Se houver dúvidas, entre em contato com a nossa Central de Atendimento:</p>";
                // html += "<p> 4003 4031(Capitais e Regiões Metropolitanas) </p>";
                // html += "<p> 0800 880 4031(demais regiões).</p>";

                html += "</div>";

                html += "</div>";

                return Content(html, "text/html");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}