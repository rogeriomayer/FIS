using FMC.FIS.Business.Models.Boleto;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FMC.FIS.Business.DAO
{
    public class BoletoDAO : AbstractRepositoryPersistence<Boleto>
    {
        public BoletoDAO() : base("CNN_BOLETO") { }

        public Boleto GetByNumeroDocumentoP2(string numeroDocumento, string carteira)
        {
            return Context
                .Where<Boleto>(p => p.Carteira.StartsWith(carteira) && p.IdentificadorInternoBoleto.StartsWith(numeroDocumento))
                .OrderByDescending(p => p.IdBoleto)
                .FirstOrDefault<Boleto>();
        }

        public Boleto GetBoleto(string cpf, decimal valor, DateTime dtVencimento, string documento, string carteira)
        {

            return this.Context.AsQueryable<Boleto>()
                   .Where(p => p.Sacado.CpfCnpj == cpf)
                   .Where(p => p.ValorBoleto == valor)
                   .Where(p => p.DataVencimento == dtVencimento)
                   //.Where(p => p.RetornoCNAB.Where(r => r.IdentificacaoOcorrencia == "03").Count() == 0)
                   .Where(p => p.Carteira.StartsWith(carteira))
                   .Where(p => p.NossoNumero.StartsWith(documento))
                   .OrderByDescending(p => p.IdBoleto)
                   .FirstOrDefault<Boleto>();
        }
    }
}
