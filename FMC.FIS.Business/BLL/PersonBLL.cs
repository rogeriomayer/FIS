using FMC.FIS.Business.Code.Api.Recupera;
using FMC.FIS.Business.Code.Api.OneB2K;
using FMC.FIS.Business.DAO;
using FMC.FIS.Business.Models.Customer;
using FMC.FIS.Business.Models.FIS;
using FMC.FIS.BLL;
using FMC.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using FMC.FIS.Business.Code.Api.Cobmais;

namespace FMC.FIS.Business.BLL
{
    public class PersonBLL : BLL<Person, PersonDAO>
    {
        public Models.Customer.PersonResponse GetByCPF(string cpf, Constants.ProductType productType)
        {
            try
            {
                var person = persistence.GetByCPF(cpf);

                var products = person.Product.Where(p => p.IdProductType == Convert.ToByte(productType)).Select(p => p.DsProduct).ToList();

                var personResponse = this.CreatePersonResponse(person, products);

                return CompletePersonResponse(cpf, products, person, personResponse, productType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Models.Customer.PersonResponse GetByCPF(string cpf, string cardToken, Constants.ProductType productType)
        {
            try
            {
                var person = persistence.GetByCPF(cpf);

                var portador = OneB2KApi.GetPortador(cpf, "", false);

                var card = portador.responseData.listaPortadores.Where(p => p.cartao != null && p.cartao.jwtCartao == cardToken).FirstOrDefault();
                string nrcard = card.cartao.nrCartao;

                var product = portador.responseData.listaPortadores.Where(p => p.nrConta.StartsWith(nrcard.Substring(0, 4)) && p.nrConta.Substring(15, 1) == nrcard.Substring(15, 1)).FirstOrDefault();

                List<String> products = new List<String>() { product.nrConta };

                var personResponse = this.CreatePersonResponse(person, products);

                return CompletePersonResponse(cpf, products, person, personResponse, productType);

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

        public ICollection<Person> GetByMailSend(int idProductType, DateTime dtLead, int count, int ageIni, int ageFim)
        {
            //var listPerson = new SendEmailBLL().GetEmail15Days().ToList();
            return persistence.GetByMailSend(idProductType, dtLead, count, ageIni, ageFim);
        }

        public ICollection<Person> GetByMailSendNews(int idProductType, DateTime dtLead, int count)
        {
            //var listPerson = new SendEmailBLL().GetEmail15Days().ToList();
            return persistence.GetByMailSendNews(idProductType, dtLead, count);
        }
        public ICollection<Person> GetPersonSentedSMS()
        {
            return persistence.GetPersonSentedSMS();
        }

        public ICollection<Person> GetPersonSendSMS(DateTime dtLead)
        {
            return persistence.GetPersonSendSMS(dtLead);
        }

        public ICollection<Person> GetPersonSendRCS(DateTime dtLead)
        {
            return persistence.GetPersonSendRCS(dtLead);
        }

        public ICollection<Person> GetPersonSendRCSNews()
        {
            return persistence.GetPersonSendRCSNews();
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

        private IList<CardResponse> CreateCardResponse(Person person, IList<string> dsProduct)
        {
            IList<CardResponse> cards = new List<CardResponse>();
            IList<Product> products;
            if (dsProduct != null && dsProduct.Count > 0)
                products = person.Product.Where(p => dsProduct.Contains(p.DsProduct)).ToList();
            else
                products = person.Product.ToList();

            foreach (var product in products)
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
                var productSpecification = product.IdProductSpecification.HasValue ? new ProductSpecificationBLL().GetBykey(product.IdProductSpecification) : null;
                card.CardImage = productSpecification != null ? productSpecification.UrlImage : "";

                card.CardName = productSpecification != null ? productSpecification.Description : "Cartão Credz";
                card.CardNumber = product.DsProduct.Substring(3, 6).PadRight(16, '*');
                card.AvailableBilling = false;

                cards.Add(card);
            }

            return cards;

        }

        private Models.Customer.PersonResponse CreatePersonResponse(Person person, IList<string> dsProduct)
        {
            var personResponse = new Models.Customer.PersonResponse();

            if (person != null)
            {
                //preenche dados da base/mailing
                personResponse.IdPerson = person.IdPerson;
                personResponse.CPF = person.NrCNPJCPF;
                personResponse.Name = person.DsName;
                personResponse.DtBirth = person.DtBirth.HasValue ? person.DtBirth.Value : new DateTime(1900, 01, 01);
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
                                                    PhoneType = p.PhoneType != null ? p.PhoneType.DsPhoneType : null,
                                                    Blacklist = p.Blacklist,
                                                    DtPhone = p.DtInsert,
                                                    IdPhoneType = p.IdPhoneType,
                                                    IdPhoneStatus = p.IdPhoneStatus
                                                }
                                        ).ToList();



                personResponse.Cards = CreateCardResponse(person, dsProduct);

            }

            return personResponse;
        }

        public Models.Customer.PersonResponse CompletePersonResponse(string cpf, IList<string> products, Person person, Models.Customer.PersonResponse personResponse, Constants.ProductType productType)
        {
            if (productType == Constants.ProductType.CREDZ)
            {
                try
                {
                    var personCobmais = CobmaisAPI.GetPessoa(cpf);
                    IList<Models.Cobmais.Contrato> cobmaisContracts = null;
                    if (personCobmais != null)
                    {
                        FillPersonDataCredz(personCobmais, personResponse);
                        cobmaisContracts = CobmaisAPI.GetContratos(cpf, "0", "0");
                    }

                    if (personCobmais == null && (cobmaisContracts == null || cobmaisContracts.Count == 0))
                        return null;

                    if (person == null)
                    {
                        person = CreatePersonCredz(personCobmais, cobmaisContracts);

                        products = person.Product.Where(p => p.IdProductType == Convert.ToByte(productType)).Select(p => p.DsProduct).ToList();

                        personResponse = CreatePersonResponse(person, products);
                    }



                    FillCardsCredz(products, cobmaisContracts, person, personResponse);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("não encontrado"))
                    {
                        if (personResponse.Cards != null)
                            personResponse.Cards.ToList().ForEach(p => p.AvailableBilling = false);
                    }
                }
                //FillAgreementCredz(person.NrCNPJCPF, personResponse.);
            }
            else if (productType == Constants.ProductType.AFINZ)
            {
                FillDataOneB2k(personResponse, person);

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
                    card.AvailableBilling = card.Age > 7 ? true : false;
                    if (string.IsNullOrEmpty(card.CardName))
                        card.CardName = "Cartão Teste";
                }
            }
        }

        private Person CreatePersonCredz(Models.Cobmais.Pessoa personApi, IList<Models.Cobmais.Contrato> cobmaisContracts)
        {
            var person = new Person();
            person.NrCNPJCPF = personApi.cliente_cpfcnpj;
            person.DsName = personApi.nome_pessoa.Trim();
            person.DtBirth = personApi.data_nascimento;
            person.DtInsert = DateTime.Now;

            foreach (var telefone in personApi.telefones.Where(p => p.ativo).ToList())
            {
                person.Phone.Add
                    (
                        new Phone()
                        {
                            IdPhoneStatus = telefone.contato ? 1 : 3,
                            Blacklist = false,
                            IdOrigem = 2,
                            IdPhoneType = telefone.id_tipo,
                            NrPhone = telefone.numero.Trim(),
                            DtInsert = DateTime.Now
                        }
                    );
            }

            foreach (var email in personApi.emails.Where(p => p.ativo).ToList())
            {
                person.Email.Add
                    (
                        new Email()
                        {
                            IdOrigem = 2,
                            DsEmail = email.email,
                            DtInsert = DateTime.Now
                        }
                    );
            }
            foreach (var endereco in personApi.enderecos.Where(p => p.ativo).ToList())
            {
                person.Address.Add
                    (
                    new Address()
                    {
                        DsAddress = endereco.logradouro.Trim(),
                        DsCep = endereco.cep.Trim(),
                        DsCity = endereco.cidade,
                        DsDistrict = endereco.bairro,
                        DsUF = endereco.uf,
                        DsComplement = endereco.complemento,
                        NrAddress = endereco.numero,
                        DtInsert = DateTime.Now
                    }
                    );
            }

            CreateProductCredz(ref person, cobmaisContracts);

            var newPerson = persistence.Add(person);

            return newPerson;
        }

        private void CreateProductCredz(ref Person person, IList<Models.Cobmais.Contrato> cobmaisContracts)
        {
            foreach (var card in cobmaisContracts)
            {
                var logo = card.dados_adicionais.Where(p => p.nome.Contains("digo da Logo")).FirstOrDefault();
                if (logo == null)
                    logo = card.dados_adicionais.Where(p => p.nome.Contains("digo da Filial")).FirstOrDefault();
                int idLogo = logo != null ? (Convert.ToInt32(logo.valor) > 100 ? Convert.ToInt32(logo.valor) : (Convert.ToInt32(logo.valor) + 100)) : 0;
                var productSpecification = new ProductSpecificationBLL().GetByLogo(3, idLogo);
                var parcela = card.parcelas.Where(p => p.vencimento <= DateTime.Today).OrderByDescending(p => p.vencimento).FirstOrDefault();
                if (parcela == null)
                    parcela = card.parcelas.OrderBy(p => p.vencimento).FirstOrDefault();
                if (!person.Product.Where(p => p.DsProduct == card.numero_contrato.Trim() && p.IdProductType == 3).Any())
                    person.Product.Add
                        (
                        new Product()
                        {
                            DsProduct = card.numero_contrato.Trim(),
                            IdProductType = 3,
                            DtInsert = DateTime.Now,
                            Lead = new List<Lead>()
                            {
                            new Lead()
                            {
                                Age = DateTime.Now.Subtract(parcela.vencimento).Days ,
                                DebitBalance =parcela.valor,
                                nmArquivo = "API CREDZ",
                                DtInsert = DateTime.Now
                            }
                            },
                            IdProductSpecification = productSpecification != null ? productSpecification.IdProductSpecification : null
                        }
                        );

                if (person.IdPerson > 0)
                {
                    Person newPerson = persistence.UpdateNoContext(person);
                    person = newPerson;
                }
            }
        }

        private void FillPersonDataCredz(Models.Cobmais.Pessoa personCobmais, Models.Customer.PersonResponse personResponse)
        {
            //var personApi = CobmaisAPI.GetPessoa(cpf);

            if (personCobmais != null)
            {
                if (personResponse.DtBirth <= Convert.ToDateTime("1930-01-01"))
                    personResponse.DtBirth = personCobmais.data_nascimento;
                //personResponse.RG = personApi.FirstOrDefault().RG;

                foreach (var email in personCobmais.emails)
                    if (!personResponse.Email.Contains(email.email))
                        personResponse.Email.Add(email.email);

                var endereco = personCobmais.enderecos.FirstOrDefault();

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

                foreach (var phone in personCobmais.telefones)
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

        private void FillCardsCredz(IList<string> products, IList<Models.Cobmais.Contrato> cobmaisContracts, Person person, Models.Customer.PersonResponse personResponse)
        {

            //var cobmaisContracts = CobmaisAPI.GetContratos(cpf, "0", "0");

            if (cobmaisContracts != null && cobmaisContracts.Count > 0)
            {
                foreach (var contract in cobmaisContracts)
                {
                    CardResponse card = personResponse.Cards.Where(p => p.Account == contract.numero_contrato).FirstOrDefault();
                    if (card == null)
                    {
                        CreateProductCredz(ref person, cobmaisContracts);
                        personResponse.Cards = CreateCardResponse(person, products);
                        card = personResponse.Cards.Where(p => p.Account == contract.numero_contrato).FirstOrDefault();
                    }
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

                            foreach (var parcela in parcelas)
                            {
                                card.ParcelaCredz.Add
                                    (
                                        new ParcelaCredz()
                                        {
                                            id_parcela_original = parcela.id,
                                            negociacao_id = contract.negociacao_id,
                                            numero_parcela_original = parcela.numero,
                                            vencimento = parcela.vencimento,
                                            valor = parcela.valor
                                        }
                                    );
                            }
                            /*
                            card.ComplementData.Add(new ComplementData() { Name = "id_parcela_original", Value = parcelas.OrderBy(p => p.vencimento).FirstOrDefault().id.ToString() });
                            card.ComplementData.Add(new ComplementData() { Name = "negociacao_id", Value = contract.negociacao_id.ToString() });
                            card.ComplementData.Add(new ComplementData() { Name = "numero_parcela_original", Value = parcelas.OrderBy(p => p.vencimento).FirstOrDefault().numero.ToString() });
                            */
                        }

                        string finalCartao = contract.dados_adicionais.Where(p => p.nome == "Final Número Cartão").Count() > 0 ? contract.dados_adicionais.Where(p => p.nome == "Final Número Cartão").FirstOrDefault().valor : "";
                        card.CardNumber = Convert.ToInt64(contract.numero_contrato).ToString().Substring(0, 6).PadRight(finalCartao.Length >= 3 ? 12 : 16, '*') + finalCartao;
                        card.CardName = String.IsNullOrEmpty(contract.filial_descricao) ? (string.IsNullOrEmpty(card.CardName) ? "CredZ" : card.CardName) : contract.filial_descricao;
                        var unvailableBilling = new UnvailableBillingBLL().GetByProduct(contract.numero_contrato);
                        card.AvailableBilling = unvailableBilling != null ? false : true;

                        ///validar acordos
                        FillAgreementCredz(personResponse.CPF, ref card, contract);
                    }
                }
            }
            else
            {
                personResponse.Cards.ToList().ForEach(p => p.AvailableBilling = false);
            }
        }

        private void FillAgreementCredz(string cpf, ref Models.Customer.CardResponse cardResponse, Models.Cobmais.Contrato contrato)
        {
            try
            {
                var acordos = CobmaisAPI.GetAcordos(cpf);
                var idProduct = cardResponse.IdProduct;

                IList<int> statusAtivos = new List<int>() { 1, 3, 6, 10 };

                if (acordos != null && acordos.Count > 0)
                {
                    // var parcelaOriginal = cardResponse.ComplementData.Where(p => p.Name == "id_parcela_original").FirstOrDefault();
                    var parcelaOriginal = cardResponse.ParcelaCredz.OrderByDescending(p => p.id_parcela_original).FirstOrDefault();
                    Models.Cobmais.Acordo acordo = null;
                    if (parcelaOriginal != null)
                    {
                        acordo = acordos.Where(p => p.id == contrato.parcelas.Select(p => p.acordo_id).FirstOrDefault()).FirstOrDefault();

                        if (acordo == null)
                            acordo = acordos.Where(p => p.parcelas_originais.Where(po => po.id == Convert.ToInt64(parcelaOriginal.id_parcela_original)).Any()).OrderByDescending(p => p.id).FirstOrDefault();
                        //acordo = acordos.Where(p => p.parcelas_originais.Where(po => po.id == Convert.ToInt64(parcelaOriginal.Value)).Any()).OrderByDescending(p => p.id).FirstOrDefault();


                        if (acordo == null)
                            acordo = acordos.Where(p => p.parcelas_originais.FirstOrDefault().id == contrato.parcelas.FirstOrDefault().id).FirstOrDefault();


                        if (acordo != null)
                        {

                            AgreementResponse agreementResponse = cardResponse.StatusLeadResponse.Where(p => p.AgreementResponse != null && p.AgreementResponse.CdAgreement == acordo.id.ToString()).OrderByDescending(p => p.IdStatusLead).Select(p => p.AgreementResponse).FirstOrDefault();

                            var unvailableBilling = new UnvailableBillingBLL().GetByProduct(contrato.numero_contrato);

                            cardResponse.AvailableBilling = unvailableBilling != null ? false : !statusAtivos.Contains(acordo.status_id.Value);

                            if (agreementResponse != null && agreementResponse.CdAgreement == acordo.id.ToString())
                            {
                                if (agreementResponse.IdAgreementStatus != acordo.status_id)
                                    new AgreementBLL().UpdateAgreementStatus(agreementResponse.IdAgreement, acordo.status_id.Value);

                                if (statusAtivos.Contains(acordo.status_id.Value))
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
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private Models.FIS.StatusLead AddAgreementAPI(long idLead, long idProduct, FMC.FIS.Business.Models.Cobmais.Acordo acordo)
        {
            var statusLead = new StatusLead();
            statusLead.IdLead = idLead;
            statusLead.IdStatus = 32;
            statusLead.DsDescription = "ACORDO IMPORTADO COBMAIS PORTAL";
            statusLead.DtInsert = DateTime.Now;
            statusLead.IdUserLogin = 1;
            if (acordo.parcelas_novas != null && acordo.parcelas_novas.Count > 0)
            {
                statusLead.Agreement.Add(
                    new Agreement()
                    {
                        VlEntrace = acordo.parcelas_novas.Where(p => p.parcela == "1").FirstOrDefault().valor,
                        DtEntrace = acordo.parcelas_novas.Where(p => p.parcela == "1").FirstOrDefault().vencimento,
                        PcDiscount = acordo.desconto_principal + acordo.desconto_honorario + acordo.desconto_juros + acordo.desconto_multa,
                        QtParcel = acordo.parcelas_novas.Count - 1,
                        VlParcel = acordo.parcelas_novas.Where(p => p.parcela == "2").Count() > 0 ? acordo.parcelas_novas.Where(p => p.parcela == "2").FirstOrDefault().valor : 0,
                        VlAgreement = acordo.total,
                        IdAgreementStatus = acordo.status_id.Value,
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
            }
            else
            {
                statusLead.Agreement.Add(
                    new Agreement()
                    {
                        VlEntrace = acordo.parcelas_originais.FirstOrDefault().total,
                        DtEntrace = acordo.data,
                        PcDiscount = acordo.desconto_principal + acordo.desconto_honorario + acordo.desconto_juros + acordo.desconto_multa,
                        QtParcel = acordo.quantidade_parcelas - 1,
                        VlParcel = acordo.parcelas_originais.FirstOrDefault().total,
                        VlAgreement = acordo.total,
                        IdAgreementStatus = acordo.status_id.Value,
                        CdPaymentOption = "Boleto",
                        CdParcelPlan = "API CREDZ",
                        CdAgreement = acordo.id.ToString(),
                        DtInsert = acordo.data,
                        AgreementParcel = acordo.parcelas_originais.Select(pn => new AgreementParcel()
                        {
                            NrParcel = 0,
                            DtParcel = acordo.data,
                            VlParcel = pn.total,
                            Billet = acordo.boletos.OrderByDescending(p => p.vencimento).Select
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
            }
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
