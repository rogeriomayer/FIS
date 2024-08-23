using FMC.FIS.API.Code.Api.OneB2K;
using FMC.FIS.API.Code.Business.DAO;
using FMC.FIS.API.Models.FIS;
using FMC.FIS.API.Models.OneB2K;
using FMC.Generic;
using System;
using System.Linq;

namespace FMC.FIS.API.Code.Business.BLL
{
    public class LeadBLL : BLL<Lead, LeadDAO>
    {
        public Lead GetByCPF(string cpf, byte productType)
        {
            return persistence.GetByCPF(cpf, productType);
        }

        public ParcelamentoFaturaResponse SimularParcelamentoFatura(string cpf, decimal vlEntrada, decimal saldo, decimal minimo, DateTime dtVencimento, int age)
        {
            try
            {
                var portador = Code.Api.OneB2K.OneB2KApi.GetPortador(cpf, "", false);
                var cartao = portador.responseData.listaPortadores.Where(p => p.cartao != null).FirstOrDefault().cartao;
                string conta = portador.responseData.listaPortadores.Where(p => p.nrConta.StartsWith(cartao.nrCartao.Substring(0, 4))).FirstOrDefault().nrConta;
                string nmPortador = portador.responseData.listaPortadores.Where(p => p.nrConta.StartsWith(cartao.nrCartao.Substring(0, 4))).FirstOrDefault().nmPortador;

                var simulacaoParcelamento = new SimulacaoParcelamentoFaturaRequest()
                {
                    tipoParcelado = "04",
                    valorAdesao = vlEntrada.ToString("N2"),
                    valorParcelado = "",
                    subProduto = "",
                    categoria = "",
                    totalTransacao = saldo.ToString("N2"),
                    saldoFatura = saldo.ToString("N2"),
                    vencimento = dtVencimento.ToString("dd/MM/yyyy"),
                    utilizaCred = "N",
                    billingCode = "VIF00101",
                    parcelas = "10",
                    cliente = nmPortador,
                    produto = "VIF",
                    diasAtraso = age.ToString().PadLeft(3, '0'),
                    contratosAbertos = "00",
                    canal = "FAT",
                    valorMinimo = "000",//minimo.ToString("N2"),
                    status = "A",
                    blockReclass = "blockReclass"

                };

                return OneB2KApi.GetSimulacaoParcelamentoFatura(conta, simulacaoParcelamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
