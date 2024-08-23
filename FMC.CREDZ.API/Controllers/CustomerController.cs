using FMC.CREDZ.API.Models.FIS;
using FMC.FIS.BLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections;
using FMC.FIS.Model.Pessoa;
using System.Collections.Generic;

namespace FMC.CREDZ.API.Controllers
{
    //[ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        public CustomerController()
        {
        }

        [HttpGet, Route("{cpf}")]
        public IActionResult Person(string cpf)
        {
            try
            {
                var person = new RestApi().Get<Pessoa>("http://10.40.0.109/ibi/ura.svc", "person/" + cpf);
                if (person != null)
                {

                    var contas = new List<Conta>();
                    string contaAdd = "";

                    foreach (var conta in person.Contas.OrderByDescending(p => p.NrConta).ThenByDescending(p => p.NrCartao).ToList())
                    {
                        if (contaAdd != conta.NrConta)
                        {
                            contaAdd = conta.NrConta;
                            contas.Add(conta);
                        }
                    }

                    var personResponse = new PersonResponse()
                    {
                        IdPerson = person.IdPessoa,
                        Name = person.Nome,
                        Cpf = person.Cpf,
                        Address = new Addresses()
                        {
                            Cep = person.Endereco != null ? person.Endereco.Cep : "",
                            Street = person.Endereco != null ? person.Endereco.Logradouro : "",
                            Number = person.Endereco != null ? person.Endereco.Numero : "",
                            Complement = person.Endereco != null ? person.Endereco.Complemento : "",
                            District = person.Endereco != null ? person.Endereco.Bairro : "",
                            City = person.Endereco != null ? person.Endereco.Cidade : "",
                            UF = person.Endereco != null ? person.Endereco.Estado : "",
                        },
                        Phones = person.Telefone.Select(p => new Phones()
                        {
                            IdPhone = p.IdTelefone,
                            PhoneNumber = p.NrTelefone,
                            PhoneType = p.TipoTelefone
                        }).ToList(),
                        Emails = person.Emails.Select(p => new Models.FIS.Emails()
                        {
                            Email = p.Email,
                            Date = p.Data
                        }).ToList(),
                        Accounts = contas.Select(p => new Account()
                        {
                            AccountNumber = p.NrConta,
                            CardNumber = Convert.ToInt64(p.NrCartao).ToString(),
                            Product = p.Produto,
                            DueDate = p.DtAtraso,
                            Age = p.DiasAtraso,
                            Balance = p.VlSaldoAtraso,
                            MinimumPayment = p.VlPagamentoMinimo,
                            CardImage = p.ImagemCartao,
                            AccountType = p.TipoConta,
                            ORG = p.ORG,
                            LOGO = p.LOGO,
                            DayPayment = p.DiasVencimento
                        }).ToList()
                    };

                    return Ok(personResponse);
                }
                else
                    throw new Exception("CPF não encontrado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("agreement/{card}")]
        public IActionResult Agreement(string card)
        {
            try
            {
                var ur83 = new RestApi().Get<UR83>("http://10.40.0.109/ibi/ura.svc", "UR83/" + card);
                if (ur83.ReturnCode != "666")
                {
                    var agreement = new Agreement()
                    {
                        StatusCode = ur83.StatusAcordo.Trim(),
                        Status = ur83.DescricaoStatus.Trim(),
                        StatusDate = ur83.DataAlteracaoStatus,
                        Agent = ur83.IdentificacaoOperador,
                        Partner = ur83.Codigoassessoria,
                        AgreementDate = ur83.DataAtivacaoAcordo,
                        Balance = ur83.SaldoNegociado,
                        Discount = ur83.ValorDesconto,
                        EntraceDate = ur83.DataEntradaAcordo,
                        EntraceValue = ur83.ValorEntrada,
                        FirstParcelDate = ur83.DataVencimentoParc1,
                        ParcelValue = ur83.ValorParcela,
                        Parcels = ur83.NumeroParcelasAcordo,
                        PaymentDay = ur83.DiaVencimento,
                        DelayedParcel = ur83.NumeroParcelaAtraso,
                        DelayedParcelDate = ur83.DtVctoParcelaAtraso,
                        DelayedParcelValue = ur83.ValorParcelaAtraso,
                        DaysDelayedParcel = ur83.QuantidadeDiasAtraso,
                        DaysDelayedEntrace = ur83.QuantidadeDiasAtrasoEntrada,
                        IndDelayedEntrance = ur83.IndicadorEntradaAtraso,
                        BalancePayment = ur83.SaldoQuitacao
                    };
                    return Ok(agreement);
                }
                else
                    return Ok(new Agreement());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}