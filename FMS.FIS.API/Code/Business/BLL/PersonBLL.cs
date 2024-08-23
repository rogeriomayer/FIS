using FMC.FIS.API.Code.Api.Recupera;
using FMC.FIS.API.Code.Api.OneB2K;
using FMC.FIS.API.Code.Business.DAO;
using FMC.FIS.API.Models.Customer;
using FMC.FIS.API.Models.FIS;
using FMC.FIS.BLL;
using FMC.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using FMC.FIS.API.Code.Api.Cobmais;

namespace FMC.FIS.API.Code.Business.BLL
{
    public class PersonBLL : BLL<Person, PersonDAO>
    {
        public Models.Customer.PersonResponse GetByCPF(string cpf, Constants.ProductType productType)
        {
            try
            {
                var person = persistence.GetByCPF(cpf);

                if (person != null)
                {
                    return this.CreatePersonResponse(person, productType);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePerson(PersonUpdateRequest personUpdateRequest, Constants.ProductType productType)
        {

            var personBll = new PersonBLL();
            var person = personBll.GetBykey(personUpdateRequest.IdPerson);

            /*
            if (productType == Constants.ProductType.AFINZ)
                UpdatePersonAfinz(person.NrCNPJCPF, personUpdateRequest);
            */

            if (person.Email.Where(p => p.DsEmail.Trim().ToLower() == personUpdateRequest.Email.ToLower().Trim()).Count() == 0)
            {
                person.Email.Add
                    (
                        new Email()
                        {
                            DsEmail = personUpdateRequest.Email.ToLower().Trim(),
                            IdOrigem = 2,
                            DtInsert = DateTime.Now
                        }
                    );

            }
            else
            {
                var email = person.Email.Where(p => p.DsEmail.Trim().ToLower() == personUpdateRequest.Email.ToLower().Trim()).FirstOrDefault();
                email.DtInsert = DateTime.Now;
            }

            //var address = person.Address.Where(p => p.IdAddress == personUpdateRequest.AddressUpdateRequest.IdAddress).FirstOrDefault();

            var address = person.Address.OrderByDescending(p => p.DtInsert).FirstOrDefault();

            if (address == null)
                address = new Address();

            address.IdPerson = person.IdPerson;
            address.DsCep = personUpdateRequest.AddressUpdateRequest.Cep.Trim().Replace("-", "").Replace(".", "");
            address.DsAddress = personUpdateRequest.AddressUpdateRequest.Address.Trim();
            address.NrAddress = personUpdateRequest.AddressUpdateRequest.NrAddress.Trim();
            address.DsComplement = personUpdateRequest.AddressUpdateRequest.Complement.Trim();
            address.DsCity = personUpdateRequest.AddressUpdateRequest.City.Trim();
            address.DsDistrict = personUpdateRequest.AddressUpdateRequest.District.Trim();
            address.DsUF = personUpdateRequest.AddressUpdateRequest.UF.Trim();
            address.DtInsert = DateTime.Now;

            if (address.IdAddress == 0)
                person.Address.Add(address);

            foreach (var phoneUpdate in personUpdateRequest.PhoneUpdateRequest.Where(p => p.NrPhone.Trim().Length >= 10).ToList())
            {
                Phone phone = person.Phone.Where(p => TextUtil.RemovePhoneMask(p.NrPhone) == TextUtil.RemovePhoneMask(phoneUpdate.NrPhone)).FirstOrDefault();

                if (!phoneUpdate.Remove)
                {
                    if (phone == null)
                    {
                        phone = new Phone()
                        {
                            Blacklist = false,
                            DtInsert = DateTime.Now,
                            IdPhoneType = GetPhoneType(phoneUpdate.PhoneType),
                            NrPhone = TextUtil.RemovePhoneMask(phoneUpdate.NrPhone),
                            IdPerson = person.IdPerson,
                            IdOrigem = 2,
                            IdPhoneStatus = phoneUpdate.IdPhoneStatus
                        };
                        person.Phone.Add(phone);
                    }
                    else
                    {
                        phone.IdPhoneType = GetPhoneType(phoneUpdate.PhoneType);
                        phone.DtInsert = DateTime.Now;
                    }
                }
                else
                {
                    person.Phone.Remove(phone);
                }
            }

            personBll.Update(person);


        }

        private KeyValuePair<int, string> GetPhoneTypeCredz(int idPhoneType)
        {
            if (idPhoneType == 1)
                return new KeyValuePair<int, string>(1, "Residencial");
            else if (idPhoneType == 2)
                return new KeyValuePair<int, string>(2, "Comercial");
            else if (idPhoneType == 3)
                return new KeyValuePair<int, string>(3, "Móvel");
            else
                return new KeyValuePair<int, string>(4, "Outros");
        }

        private int GetPhoneType(string phoneType)
        {
            if (phoneType.ToUpper().Contains("RES"))
                return 1;
            else if (phoneType.ToUpper().Contains("COM"))
                return 2;
            else if (phoneType.ToUpper().Contains("CEL"))
                return 3;
            else
                return 4;
        }

        private Models.Customer.PersonResponse CreatePersonResponse(Person person, Constants.ProductType productType)
        {
            var personResponse = new Models.Customer.PersonResponse();

            IList<CardResponse> cards = new List<CardResponse>();

            //preenche dados da base/mailing
            personResponse.IdPerson = person.IdPerson;
            personResponse.CPF = person.NrCNPJCPF;
            personResponse.Name = person.DsName;
            personResponse.DtBirth = person.DtBirth.HasValue ? person.DtBirth.Value : new DateTime(1990, 01, 01);
            personResponse.Address = new AddressResponse();
            if (person.Address.Count > 0)
            {
                var maxAddress = person.Address.OrderByDescending(p => p.DtInsert).FirstOrDefault();
                personResponse.Address.Address = maxAddress.DsAddress;
                personResponse.Address.Cep = maxAddress.DsCep;
                personResponse.Address.District = maxAddress.DsDistrict;
                personResponse.Address.NrAddress = maxAddress.NrAddress;
                personResponse.Address.City = maxAddress.DsCity;
                personResponse.Address.Complement = maxAddress.DsComplement;
                personResponse.Address.UF = maxAddress.DsUF;
            }

            personResponse.Email = person.Email.Count() > 0 ? person.Email.Select(p => p.DsEmail).ToList() : new List<string>();

            personResponse.Phones = person.Phone.Where(p => p.NrPhone.Trim().Length >= 10).Select(
                                            p => new PhoneResponse()
                                            {
                                                IdPhone = p.IdPhone,
                                                NrPhone = p.NrPhone,
                                                PhoneType = p.PhoneType.DsPhoneType,
                                                Blacklist = p.Blacklist,
                                                DtPhone = p.DtInsert,
                                                IdPhoneType = p.IdPhoneType,
                                                IdPhoneStatus = p.IdPhoneStatus
                                            }
                                    ).ToList();

            foreach (var product in person.Product)
            {
                var card = new CardResponse();

                foreach (var lead in product.Lead.OrderBy(p => p.IdLead).ToList())
                {
                    card.IdLead = lead.IdLead;
                    card.VlDue = lead.DebitBalance;
                    card.VlFull = lead.DebitBalance;
                    card.VlMinimum = lead.DebitBalance;

                    card.Age = lead.Age;
                    card.DtLead = lead.DtInsert;
                    card.DtDue = lead.DtInsert.AddDays(lead.Age * -1);


                    lead.StatusLead.ToList().ForEach
                         (
                             p => card.StatusLeadResponse.Add
                             (
                                StatusLeadBLL.CreateStatusLeadResponse(p)
                             )
                         );
                }


                card.Account = product.DsProduct;
                card.IdProduct = product.IdProduct;
                card.IdProductType = product.IdProductType;
                card.CardImage = product.IdProductSpecification.HasValue ? product.ProductSpecification.UrlImage : "";
                card.CardName = product.ProductSpecification.Description;
                //card.AvailableBilling = AvailableBilling(product, agreement == null || agreement.Status == "Cancelado");

                cards.Add(card);
            }

            personResponse.Cards = cards;



            if (productType == Constants.ProductType.CREDZ)
            {
                FillPersonDataCredz(person.NrCNPJCPF, personResponse);
                FillCardsCredz(person.NrCNPJCPF, personResponse);
                //FillAgreementCredz(person.NrCNPJCPF, personResponse.);
            }
            else if (productType == Constants.ProductType.AFINZ)
            {
                //busca dados pessoais do Recupera para preencher a lead
                /////FillPersonDataAfinz(person.NrCNPJCPF.Trim(), personResponse);
                //busca dados dividas das contas Afins
                /////FillCardsAfinz(person.NrCNPJCPF.Trim(), personResponse);

            }
            else
            {
                FillDataOneB2k(personResponse, person);
            }

            return personResponse;
        }

        private void FillDataOneB2k(Models.Customer.PersonResponse personResponse, Person person)
        {
            IList<CardResponse> cards = new List<CardResponse>();

            var listCardOneB2K = new List<CardOneB2k>();

            try
            {
                var portador = Code.Api.OneB2K.OneB2KApi.GetPortador(person.NrCNPJCPF, "", false);

                foreach (var card in portador.responseData.listaPortadores.Where(p => !string.IsNullOrEmpty(p.nrConta)).OrderByDescending(p => p.nrConta).ToList())
                {
                    if (listCardOneB2K.Where(p => p.Account == card.nrConta).Count() <= 0)
                    {
                        if (!string.IsNullOrEmpty(card.nrConta))
                        {
                            string conta = card.nrConta;

                            var cartao = portador.responseData.listaPortadores.Where(p => p.cartao != null &&
                                                                                         p.cartao.nrCartao.StartsWith(conta.Substring(0, 4))).OrderByDescending(p => p.cartao.nrCartao)
                                                                                         .FirstOrDefault();


                            //Models.OneB2K.ResumoFatura fatura = null;
                            Models.OneB2K.DadosCadastraisRespose dadosCadastraisRespose = null;
                            Models.OneB2K.ResumoSaldoAtualizadoResponse resumoSaldoAtualizado = null;
                            Models.OneB2K.DevedorResponse devedor = null;
                            decimal vlFull = decimal.Zero;

                            try
                            {
                                dadosCadastraisRespose = Code.Api.OneB2K.OneB2KApi.GetDadosCadastrais(conta);
                                vlFull = dadosCadastraisRespose != null ? Convert.ToDecimal(dadosCadastraisRespose.responseData.informacoesGerais.vlSaldoAtual + dadosCadastraisRespose.responseData.informacoesGerais.vlSaldoParceladoPendente) : vlFull;
                            }
                            catch { }

                            try
                            {
                                devedor = Code.Api.OneB2K.OneB2KApi.GetDevedor(conta, DateTime.Today.AddDays(0));
                                vlFull = devedor != null ? Convert.ToDecimal(devedor.responseData.vlTotalFechadoContaDesagio.Trim().Replace(".", "").Replace(",", "")) / Convert.ToDecimal(100.00) : vlFull;
                            }
                            catch { }
                            if (devedor == null)
                                try
                                {
                                    resumoSaldoAtualizado = Code.Api.OneB2K.OneB2KApi.GetResumoSaldoAtualizado(conta, DateTime.Today.AddDays(0));
                                    vlFull = resumoSaldoAtualizado != null ? Convert.ToDecimal(resumoSaldoAtualizado.responseData.projPayOffAmt.Trim().Replace(".", "").Replace(",", "")) / Convert.ToDecimal(100.00) : vlFull;
                                }
                                catch { }

                            listCardOneB2K.Add
                                (
                                    new CardOneB2k()
                                    {
                                        Account = conta,
                                        NrCard = cartao.cartao.nrCartao,
                                        VlFull = vlFull,
                                        VlMinimum = dadosCadastraisRespose != null ? Convert.ToDecimal(dadosCadastraisRespose.responseData.informacoesGerais.vlSaldoAtual) : 0,
                                        Age = dadosCadastraisRespose != null ? dadosCadastraisRespose.responseData.diasAtraso : 0
                                    }
                                );
                        }
                    }
                }
            }
            catch { }

            /*
            foreach (var product in person.Product)
            {
                var card = new CardResponse();

                foreach (var lead in product.Lead.OrderBy(p => p.IdLead).ToList())
                {
                    card.IdLead = lead.IdLead;
                    card.VlDue = lead.DebitBalance;
                    card.VlFull = lead.DebitBalance;
                    card.VlMinimum = lead.DebitBalance;

                    card.Age = lead.Age;
                    card.DtLead = lead.DtInsert;
                    card.DtDue = lead.DtInsert.AddDays(lead.Age * -1);
                    lead.StatusLead.ToList().ForEach
                         (
                             p => card.StatusLeadResponse.Add
                             (
                                new StatusLeadResponse()
                                {
                                    IdStatusLead = p.IdStatusLead,
                                    IdStatus = p.IdStatus,
                                    Status = p.Status.DsStatus,
                                    Description = p.DsDescription,
                                    DtStatusLead = p.DtInsert,
                                    FlEfective = p.Status.FlEfective,
                                    IdUserLogin = p.IdUserLogin,
                                    UserLogin = new FisLoginBLL().GetBykey(p.IdUserLogin).DsName,
                                    PromisseResponse =
                                    p.Promisse.Select(pr => new PromisseResponse()
                                    {
                                        IdPromisse = pr.IdPromisse,
                                        DtPromisse = pr.DtPromisse,
                                        DtInsert = pr.DtInsert,
                                        VlPromisse = pr.VlPromisse,
                                    }).FirstOrDefault(),
                                    AgreementResponse =
                                    p.Agreement.Select
                                    (
                                        a => new AgreementResponse()
                                        {
                                            IdAgreement = a.IdAgreement,
                                            IdStatusLead = a.IdStatusLead,
                                            QtParcel = a.QtParcel,
                                            VlEntrace = a.VlEntrace,
                                            DtEntrace = a.DtEntrace,
                                            VlParcel = a.VlParcel,
                                            VlAgreement = a.VlAgreement,
                                            PcDiscount = a.PcDiscount,
                                            CdAgreement = a.CdAgreement,
                                            CdParcelPlan = a.CdParcelPlan,
                                            CdPaymentOption = a.CdPaymentOption,
                                            DtInsert = a.DtInsert,
                                            Status = "EmAberto",
                                            AgreementParcelResponse = a.AgreementParcel.Select
                                                (
                                                    ap => new AgreementParcelResponse()
                                                    {
                                                        NrParcel = ap.NrParcel,
                                                        DtParcel = ap.DtParcel,
                                                        Status = "EmAberto",
                                                        VlParcel = ap.VlParcel
                                                    }
                                                ).ToList()
                                        }
                                    ).FirstOrDefault()
                                }
                             )
                         );


                }

                AgreementResponse agreement = card.StatusLeadResponse.Select(p => p.AgreementResponse).FirstOrDefault();


                if (agreement == null)
                {
                    agreement = new AgreementResponse();
                    //FillAgreement(person.NrCNPJCPF.Trim(), DateTime.Today, 0, product.DsProduct, agreement);
                }
                
                ///else
                    ///FillAgreement(person.NrCNPJCPF.Trim(), DateTime.Today, 0, product.DsProduct, agreement);
                

                card.Account = product.DsProduct;
                card.CardName = "Cartão CredZ Loja X"; //--Não temos o nome do produto em nenhuma API
                card.CardNumber = product.DsProduct;
                card.AccountNumber = product.DsProduct;
                card.IdProduct = product.IdProduct;
                card.IdProductType = product.IdProductType;
                card.AvailableBilling = AvailableBilling(product, agreement == null || agreement.Status == "Cancelado");

                cards.Add(card);
            }

            personResponse.Cards = cards;

            */

            foreach (var card in personResponse.Cards)
            {
                CardOneB2k carOneb2k = null;
                decimal fullCompare = 0;

                do
                {
                    carOneb2k = listCardOneB2K.Where(p => p.VlFull >= card.VlDue - fullCompare && p.VlFull <= card.VlDue + fullCompare).OrderByDescending(p => p.VlFull).FirstOrDefault();
                    fullCompare++;
                } while (carOneb2k == null && listCardOneB2K.Count > 0);

                if (carOneb2k != null)
                {
                    card.CardNumber = carOneb2k.NrCard;
                    card.AccountNumber = carOneb2k.Account;
                    card.VlFull = carOneb2k.VlFull;//card.VlFull < carOneb2k.VlFull ? carOneb2k.VlFull : card.VlFull;
                    card.VlMinimum = carOneb2k.VlMinimum <= 0 ? card.VlMinimum : carOneb2k.VlMinimum;
                    card.Age = carOneb2k.Age > 0 ? carOneb2k.Age : card.Age;
                }
            }
        }

        private void FillPersonDataCredz(string cpf, Models.Customer.PersonResponse personResponse)
        {
            var personApi = CobmaisAPI.GetPessoa(cpf);

            if (personApi != null)
            {
                //personResponse.DtBirth = personApi.data_nascimento;
                //personResponse.RG = personApi.FirstOrDefault().RG;

                foreach (var email in personApi.emails)
                    if (!personResponse.Email.Contains(email.email))
                        personResponse.Email.Add(email.email);

                var endereco = personApi.enderecos.FirstOrDefault();

                personResponse.Address = new AddressResponse()
                {
                    Address = endereco.logradouro,
                    Cep = endereco.cep,
                    District = endereco.bairro,
                    NrAddress = endereco.numero,
                    City = endereco.cidade,
                    Complement = endereco.complemento,
                    UF = endereco.uf
                };

                foreach (var phone in personApi.telefones)
                {
                    var ph = personResponse.Phones.Where(p => p.NrPhone == phone.numero).FirstOrDefault();
                    var phoneType = GetPhoneTypeCredz(phone.id_tipo);

                    if (ph == null)
                    {
                        personResponse.Phones.Add
                            (
                                new PhoneResponse()
                                {
                                    IdPhone = 0,
                                    Blacklist = false,
                                    DtPhone = DateTime.Today,
                                    NrPhone = phone.numero,
                                    PhoneType = phoneType.Value,
                                    IdPhoneType = phoneType.Key,
                                    IdPhoneStatus = phone.contato ? 1 : phone.ativo ? 3 : 4
                                }
                            );
                    }
                    else
                    {
                        ph.NrPhone = phone.numero;
                        ph.PhoneType = phoneType.Value;
                        ph.IdPhoneType = phoneType.Key;
                        ph.IdPhoneStatus = phone.contato ? 1 : phone.ativo ? 3 : 4;
                    }
                }
            }
        }

        private void FillCardsCredz(string cpf, Models.Customer.PersonResponse personResponse)
        {
            var cobmaisContracts = CobmaisAPI.GetContratos(cpf, "0", "0");

            if (cobmaisContracts != null && cobmaisContracts.Count > 0)
            {
                foreach (var contract in cobmaisContracts)
                {
                    CardResponse card = personResponse.Cards.Where(p => p.Account == contract.numero_contrato).FirstOrDefault();

                    if (card != null)
                    {
                        var parcelas = contract.parcelas.ToList();

                        if (parcelas != null && parcelas.Count > 0)
                        {
                            var valor = parcelas.Sum(p => p.valor);
                            var vencimento = parcelas.OrderBy(p => p.vencimento).FirstOrDefault().vencimento;

                            card.VlDue = valor;
                            card.DtDue = vencimento;

                            var saldoTotal = contract.dados_adicionais.Where(p => p.nome == "Saldo Total").FirstOrDefault();
                            card.VlFull = saldoTotal != null ? Convert.ToDecimal(saldoTotal.valor) : valor;
                            var minimo = contract.dados_adicionais.Where(p => p.nome == "Valor de Pagamento Mínimo").FirstOrDefault();
                            card.VlMinimum = minimo != null ? Convert.ToDecimal(minimo.valor) : valor;
                            card.Age = DateTime.Today.Subtract(vencimento).Days;

                            card.ComplementData.Add(new ComplementData() { Name = "id_parcela_original", Value = parcelas.OrderBy(p => p.vencimento).FirstOrDefault().id.ToString() });
                            card.ComplementData.Add(new ComplementData() { Name = "negociacao_id", Value = contract.negociacao_id.ToString() });
                            card.ComplementData.Add(new ComplementData() { Name = "numero_parcela_original", Value = parcelas.OrderBy(p => p.vencimento).FirstOrDefault().numero.ToString() });
                        }

                        string finalCartao = contract.dados_adicionais.Where(p => p.nome == "Final Número Cartão").Count() > 0 ? contract.dados_adicionais.Where(p => p.nome == "Final Número Cartão").FirstOrDefault().valor : "";
                        card.CardNumber = Convert.ToInt64(contract.numero_contrato).ToString().Substring(0, 6).PadRight(finalCartao.Length >= 3 ? 12 : 16, '*') + finalCartao;
                        card.CardName = String.IsNullOrEmpty(contract.filial_descricao) ? (string.IsNullOrEmpty(card.CardName) ? "CredZ" : card.CardName) : contract.filial_descricao;
                        card.AvailableBilling = true;

                        ///validar acordos
                        FillAgreementCredz(cpf, ref card);
                    }
                }
            }
            else
            {
                personResponse.Cards.ToList().ForEach(p => p.AvailableBilling = false);
            }
        }

        private void FillAgreementCredz(string cpf, ref Models.Customer.CardResponse cardResponse)
        {
            try
            {
                var acordos = CobmaisAPI.GetAcordos(cpf);
                var idProduct = cardResponse.IdProduct;

                IList<int> statusAtivos = new List<int>() { 1, 3, 6, 10 };

                if (acordos != null && acordos.Count > 0)
                {
                    var acordo = acordos.OrderByDescending(p => p.id).FirstOrDefault();

                    AgreementResponse agreementResponse = cardResponse.StatusLeadResponse.Where(p => p.AgreementResponse != null && p.AgreementResponse.CdAgreement == acordo.id.ToString()).OrderByDescending(p => p.IdStatusLead).Select(p => p.AgreementResponse).FirstOrDefault();

                    cardResponse.AvailableBilling = !statusAtivos.Contains(acordo.status_id);

                    if (agreementResponse != null && agreementResponse.CdAgreement == acordo.id.ToString())
                    {
                        if (agreementResponse.IdAgreementStatus != acordo.status_id)
                            new AgreementBLL().UpdateAgreementStatus(agreementResponse.IdAgreement, acordo.status_id);

                        if (statusAtivos.Contains(acordo.status_id))
                        {

                            cardResponse.AvailableBilling = false;
                            if (acordo.parcelas_novas.Count > 0)
                            {
                                agreementResponse.VlEntrace = acordo.parcelas_novas.Where(p => p.parcela == "1").FirstOrDefault().valor;
                                agreementResponse.DtEntrace = acordo.parcelas_novas.Where(p => p.parcela == "1").FirstOrDefault().vencimento;
                                agreementResponse.PcDiscount = acordo.desconto_principal + acordo.desconto_juros + acordo.desconto_multa + acordo.desconto_honorario;
                                agreementResponse.QtParcel = acordo.quantidade_parcelas;
                                if (acordo.quantidade_parcelas > 1 && acordo.parcelas_novas.Count > 1)
                                    agreementResponse.VlParcel = acordo.parcelas_novas.Where(p => p.parcela == "2").FirstOrDefault().valor;
                                agreementResponse.VlAgreement = acordo.parcelas_novas.Sum(p => p.valor);
                                agreementResponse.CdAgreement = acordo.id.ToString();
                                agreementResponse.DtInsert = acordo.data;
                                agreementResponse.Status = acordo.status_descricao;
                            }
                            else
                            {
                                agreementResponse.VlEntrace = acordo.total;
                                agreementResponse.DtEntrace = acordo.data;
                                agreementResponse.PcDiscount = acordo.desconto_principal + acordo.desconto_juros + acordo.desconto_multa + acordo.desconto_honorario;
                                agreementResponse.QtParcel = acordo.quantidade_parcelas;
                                if (acordo.quantidade_parcelas > 1 && acordo.parcelas_novas.Count > 1)
                                    agreementResponse.VlParcel = acordo.parcelas_novas.Where(p => p.parcela == "2").FirstOrDefault().valor;
                                agreementResponse.VlAgreement = acordo.total;
                                agreementResponse.CdAgreement = acordo.id.ToString();
                                agreementResponse.DtInsert = acordo.data;
                                agreementResponse.Status = acordo.status_descricao;
                            }
                            foreach (var parcel in agreementResponse.AgreementParcelResponse)
                            {
                                if (!parcel.BilletResponse.Any())
                                {
                                    var boleto = acordo.boletos.Where(p => p.vencimento.Date == parcel.DtParcel.Date).FirstOrDefault();
                                    if (boleto != null)
                                    {
                                        var newBillet = new BilletBLL().Add
                                            (
                                                new Billet()
                                                {
                                                    IdAgreementParcel = parcel.IdAgreementParcel,
                                                    CdAgreement = acordo.id.ToString(),
                                                    Barcode = boleto.codigo_barra,
                                                    Line = boleto.linha_digitavel,
                                                    DocumentNumber = boleto.nosso_numero,
                                                    DtBillet = boleto.vencimento,
                                                    IdProduct = idProduct,
                                                    CdBillet = boleto.id.ToString(),
                                                    URL = boleto.url,
                                                    VlBillet = boleto.valor,
                                                    DtInsert = DateTime.Now
                                                }
                                            );
                                        parcel.BilletResponse.Add
                                            (
                                                new BilletResponse()
                                                {
                                                    IdBillet = newBillet.IdBillet,
                                                    IdProduct = newBillet.IdProduct,
                                                    IdAgreementParcel = newBillet.IdAgreementParcel,
                                                    IdPromisse = newBillet.IdPromisse,
                                                    VlBillet = newBillet.VlBillet,
                                                    DtBillet = newBillet.DtBillet,
                                                    Barcode = newBillet.Barcode,
                                                    Line = newBillet.Line,
                                                    DocumentNumber = newBillet.DocumentNumber,
                                                    DtInsert = newBillet.DtInsert,
                                                    CdAgreement = newBillet.CdAgreement,
                                                    CdBillet = newBillet.CdBillet,
                                                    URL = newBillet.URL,
                                                    NrSendEmail = newBillet.BilletEmail.Count(),
                                                    NrSendSMS = newBillet.BilletSMS.Count(),
                                                    Parcel = newBillet.AgreementParcel != null ? newBillet.AgreementParcel.NrParcel : 0
                                                }
                                            );
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        var newStatusLead = AddAgreementAPI(cardResponse.IdLead, cardResponse.IdProduct, acordo);
                        cardResponse.StatusLeadResponse.Add(StatusLeadBLL.CreateStatusLeadResponse(newStatusLead));
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private Models.FIS.StatusLead AddAgreementAPI(long idLead, long idProduct, API.Models.Cobmais.Acordo acordo)
        {
            var statusLead = new StatusLead();
            statusLead.IdLead = idLead;
            statusLead.IdStatus = 32;
            statusLead.DsDescription = "ACORDO IMPORTADO COBMAIS PORTAL";
            statusLead.DtInsert = DateTime.Now;
            statusLead.IdUserLogin = 1;
            statusLead.Agreement.Add(
                new Agreement()
                {
                    VlEntrace = acordo.parcelas_novas.Where(p => p.parcela == "1").First().valor,
                    DtEntrace = acordo.parcelas_novas.Where(p => p.parcela == "1").First().vencimento,
                    PcDiscount = acordo.desconto_principal + acordo.desconto_honorario + acordo.desconto_juros + acordo.desconto_multa,
                    QtParcel = acordo.parcelas_novas.Count - 1,
                    VlParcel = acordo.parcelas_novas.Where(p => p.parcela == "2").Count() > 0 ? acordo.parcelas_novas.Where(p => p.parcela == "2").FirstOrDefault().valor : 0,
                    VlAgreement = acordo.total,
                    IdAgreementStatus = acordo.status_id,
                    CdPaymentOption = "Boleto",
                    CdParcelPlan = "API CREDZ",
                    CdAgreement = acordo.id.ToString(),
                    DtInsert = acordo.data,
                    AgreementParcel = acordo.parcelas_novas.Select(pn => new AgreementParcel()
                    {
                        NrParcel = Convert.ToInt32(pn.parcela) - 1,
                        DtParcel = pn.vencimento,
                        VlParcel = pn.valor,
                        Billet = acordo.boletos.Where(p => p.vencimento == pn.vencimento).Select
                        (
                            p =>
                            new Billet()
                            {
                                CdAgreement = acordo.id.ToString(),
                                Barcode = p.codigo_barra,
                                Line = p.linha_digitavel,
                                DocumentNumber = p.nosso_numero,
                                DtBillet = p.vencimento,
                                IdProduct = idProduct,
                                URL = p.url,
                                CdBillet = p.id.ToString(),
                                VlBillet = p.valor,
                                DtInsert = DateTime.Now
                            }
                        ).ToList()

                    }).ToList()
                }
            );
            return new StatusLeadBLL().Add(statusLead);
        }

        private bool AvailableBilling(Product product, bool agreementActive)
        {
            //if (product.Lead.Where(l => l.DtInsert >= DateTime.Today).Count() > 0)
            //{
            if (agreementActive == false)
            {
                foreach (var lead in product.Lead.ToList())
                {
                    foreach (var sl in lead.StatusLead.ToList())
                    {
                        if (sl.DtInsert.AddDays(sl.Status.DaysLift) > DateTime.Today)
                            return false;
                    }
                }
                return true;
            }
            //}
            //else
            return agreementActive;
        }

        /*
        private void FillPersonDataAfinz(string cpf, Models.Customer.PersonResponse personResponse)
        {
            var afinsClients = AfinzRecuperaAPI.ConsultarClientePorCPF(cpf);
            if (afinsClients != null && afinsClients.Count > 0)
            {
                personResponse.RG = afinsClients.FirstOrDefault().RG;
                personResponse.DtBirth = afinsClients.FirstOrDefault().DataNascimento;
                personResponse.RG = afinsClients.FirstOrDefault().RG;
                if (afinsClients.FirstOrDefault().Email.Trim().ToLower() != personResponse.Email.Trim().ToUpper())
                    personResponse.Email = afinsClients.FirstOrDefault().Email.ToLower();

                personResponse.Address = new AddressResponse()
                {
                    Address = afinsClients.FirstOrDefault().EnderecoResidencia.Trim(),
                    Cep = afinsClients.FirstOrDefault().CepResidencia.Trim(),
                    District = afinsClients.FirstOrDefault().BairroResidencia.Trim(),
                    NrAddress = afinsClients.FirstOrDefault().NumeroResidencia.Trim(),
                    City = afinsClients.FirstOrDefault().CidadeResidencia.Trim(),
                    Complement = afinsClients.FirstOrDefault().ComplementoResidencia.Trim(),
                    UF = afinsClients.FirstOrDefault().UFResidencia.Trim(),
                };

                //var listPhones = personResponse.Phones.Select(p => p.NrPhone.Trim()).ToList();
                foreach (var phone in afinsClients.FirstOrDefault().Telefones)
                {
                    var ph = personResponse.Phones.Where(p => p.NrPhone == phone.TelefoneMember).FirstOrDefault();

                    if (ph == null)
                    {
                        personResponse.Phones.Add
                            (
                                new PhoneResponse()
                                {
                                    IdPhone = 0,
                                    Blacklist = false,
                                    DtPhone = DateTime.Today,
                                    NrPhone = phone.TelefoneMember,
                                    PhoneType = phone.TipoTelefone,
                                    IdPhoneType = GetPhoneType(phone.TipoTelefone),
                                    IdPhoneStatus = phone.Preferencial ? 1 : phone.Ativo ? 3 : 4
                                }
                            );
                    }
                    else
                    {
                        ph.NrPhone = phone.TelefoneMember;
                        ph.PhoneType = phone.TipoTelefone;
                        ph.IdPhoneType = GetPhoneType(phone.TipoTelefone);
                        ph.IdPhoneStatus = phone.Preferencial ? 1 : phone.Ativo ? 3 : 4;
                    }
                }
            }
        }



        private void FillCardsAfinz(string cpf, Models.Customer.PersonResponse personResponse)
        {
            var afinsContracts = AfinzRecuperaAPI.ConsultarDividaEmAberto(cpf, DateTime.Today, 0);
            if (afinsContracts != null)
            {
                foreach (var contract in afinsContracts.Where(p => p.Prestacoes.Count > 0 && p.Prestacoes.Where(p => p.StatusPrestacao == RecuperaSoapApi.DominiosenmStatusDivida.EmAberto).Count() > 0).ToList())
                {
                    var card = personResponse.Cards.Where(p => p.Account == contract.CodigoContrato).FirstOrDefault();
                    if (card != null)
                    {
                        card.VlDue = Convert.ToDecimal(contract.ValorPrincipal);

                        if (card.VlFull <= 0 || card.VlFull < card.VlDue)
                            card.VlFull = Convert.ToDecimal(contract.ValorPagamento);

                        //card.VlMinimum = Convert.ToDecimal(contract.ValorPrincipal);
                        card.Age = DateTime.Today.Subtract(contract.Prestacoes.FirstOrDefault().DataVencimento).Days;
                        card.DtDue = contract.Prestacoes.FirstOrDefault().DataVencimento;
                    }
                }
            }
            else
            {
                personResponse.Cards.ToList().ForEach(p => p.AvailableBilling = false);
            }
        }

        private void FillAgreement(string cpf, DateTime dtAgreement, decimal pctDiscount, string product, AgreementResponse agreementResponse)
        {
            try
            {
                var consultarParcelamentosResult = AfinzRecuperaAPI.ConsultarParcelamentos(cpf, dtAgreement, Convert.ToDouble(pctDiscount));
                if (consultarParcelamentosResult != null)
                {
                    foreach (var parcel in consultarParcelamentosResult.Parcelamentos.ToList())
                    {
                        if (parcel.Contratos.Where(p => p.CodigoContrato == product).Count() > 0)
                        {
                            if (agreementResponse.CdAgreement == parcel.CodigoParcelamento)
                            {
                                agreementResponse.VlEntrace = Convert.ToDecimal(parcel.Parcelas.Where(p => Convert.ToInt32(p.NumeroParcela) == 0).FirstOrDefault().ValorPagamento);
                                agreementResponse.DtEntrace = parcel.Parcelas.Where(p => Convert.ToInt32(p.NumeroParcela) == 0).FirstOrDefault().DataVencimento;
                                agreementResponse.PcDiscount = Convert.ToDecimal(parcel.ValorDesconto);
                                agreementResponse.QtParcel = (parcel.Parcelas.Count() - 1);
                                if (parcel.Parcelas.Count() > 1)
                                    agreementResponse.VlParcel = Convert.ToDecimal(parcel.Parcelas.Where(p => Convert.ToInt32(p.NumeroParcela) == 1).FirstOrDefault().ValorPagamento);
                                agreementResponse.VlAgreement = Convert.ToDecimal(parcel.ValorPagamento);
                                agreementResponse.CdAgreement = parcel.CodigoParcelamento;
                                agreementResponse.DtInsert = parcel.DataParcelamento;
                            }

                            agreementResponse.Status = parcel.StatusParcelamento.ToString();


                            agreementResponse.AgreementParcelResponse = parcel.Parcelas.Select
                                (
                                    p => new AgreementParcelResponse()
                                    {
                                        NrParcel = Convert.ToInt32(p.NumeroParcela),
                                        DtParcel = p.DataVencimento,
                                        VlParcel = Convert.ToDecimal(p.ValorPagamento),
                                        Status = p.StatusParcela.ToString()
                                    }
                                ).ToList();
                        }
                    }
                }
                else
                {
                    agreementResponse.Status = "Cancelado";
                    if (agreementResponse.AgreementParcelResponse != null && agreementResponse.AgreementParcelResponse.Count > 0)
                        agreementResponse.AgreementParcelResponse.ToList().ForEach(p => p.Status = "Cancelado");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void UpdatePersonAfinz(string cpf, PersonUpdateRequest personUpdateRequest)
        {
            bool alterAddress = false;
            bool alterEmail = false;
            bool alterPhone = false;

            var client = AfinzRecuperaAPI.ConsultarClientePorCPF(cpf).FirstOrDefault();
            if (client != null)
            {
                alterEmail = client.Email.ToLower() != personUpdateRequest.Email.ToLower().Trim();

                client.Email = personUpdateRequest.Email.ToLower().Trim();

                alterAddress = !alterAddress ? client.CepResidencia != personUpdateRequest.AddressUpdateRequest.Cep.Trim().Replace("-", "").Replace(".", "") : true;
                //client.CepResidencia = personUpdateRequest.AddressUpdateRequest.Cep.Trim().Replace("-", "").Replace(".", "");

                alterAddress = !alterAddress ? client.EnderecoResidencia != personUpdateRequest.AddressUpdateRequest.Address.Trim() : true;
                //client.EnderecoResidencia = personUpdateRequest.AddressUpdateRequest.Address.Trim();

                alterAddress = !alterAddress ? client.NumeroResidencia != personUpdateRequest.AddressUpdateRequest.NrAddress.Trim() : true;
                //client.NumeroResidencia = personUpdateRequest.AddressUpdateRequest.NrAddress.Trim();

                alterAddress = !alterAddress ? client.ComplementoResidencia.Trim() != personUpdateRequest.AddressUpdateRequest.Complement.Trim() : true;
                //client.ComplementoResidencia = personUpdateRequest.AddressUpdateRequest.Complement.Trim();

                alterAddress = !alterAddress ? client.CidadeResidencia != personUpdateRequest.AddressUpdateRequest.City.Trim() : true;
                //client.CidadeResidencia = personUpdateRequest.AddressUpdateRequest.City.Trim();

                alterAddress = !alterAddress ? client.UFResidencia != personUpdateRequest.AddressUpdateRequest.UF.Trim() : true;
                //client.UFResidencia = personUpdateRequest.AddressUpdateRequest.UF.Trim();

                foreach (var phoneUpdate in personUpdateRequest.PhoneUpdateRequest.Where(p => p.NrPhone.Trim().Length >= 10).ToList())
                {
                    var phone = client.Telefones.Where(p => TextUtil.RemovePhoneMask(p.TelefoneMember) == TextUtil.RemovePhoneMask(phoneUpdate.NrPhone)).FirstOrDefault();

                    if (phone == null)
                    {
                        phone = new RecuperaSoapApi.Telefone()
                        {

                            Ativo = phoneUpdate.IdPhoneStatus < 4,
                            Preferencial = phoneUpdate.IdPhoneStatus == 1,
                            Ramal = "",
                            TelefoneMember = TextUtil.RemovePhoneMask(phoneUpdate.NrPhone),
                            TipoTelefone = phoneUpdate.PhoneType
                        };
                        client.Telefones.Add(phone);

                        alterPhone = true;
                    }
                    else
                    {
                        if (phoneUpdate.IdPhoneStatus == 4)
                        {
                            client.Telefones.Remove(phone);
                            alterPhone = true;
                        }
                        else
                        {
                            phone.Ativo = phoneUpdate.IdPhoneStatus < 4;
                            phone.Preferencial = phoneUpdate.IdPhoneStatus == 1;
                            if (phone.Preferencial)
                                phone.Ativo = true;
                            phone.TipoTelefone = phoneUpdate.PhoneType;
                        }
                    }
                }

                AfinzRecuperaAPI.AtualizarCliente(client);
                StatusLeadAlterPerson(client.CPF, personUpdateRequest.IdUserLogin, alterAddress, alterEmail, alterPhone);
            }
        }
        */

        private void StatusLeadAlterPerson(string cpf, int idUserLogin, bool alterAddress, bool alterEmail, bool alterPhone)
        {
            if (alterAddress)
            {
                //AfinzRecuperaAPI.IncluirOcorrencia(cpf, "ALTEND", DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss"), "", "");
                new StatusLeadBLL().Add(cpf, "ALTEND", idUserLogin, Constants.ProductType.AFINZ);
            }
            if (alterEmail)
            {
                //AfinzRecuperaAPI.IncluirOcorrencia(cpf, "ALTEMAIL", DateTime.Now.ToString("yyyy-MM-dd"), "", "");
                new StatusLeadBLL().Add(cpf, "ALTEMAIL", idUserLogin, Constants.ProductType.AFINZ);
            }

            if (alterPhone)
            {
                //AfinzRecuperaAPI.IncluirOcorrencia(cpf, "ALTFONE", DateTime.Now.ToString("yyyy-MM-dd"), "", "");
                new StatusLeadBLL().Add(cpf, "ALTFONE", idUserLogin, Constants.ProductType.AFINZ);
            }
        }
    }
}
