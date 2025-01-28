using FMC.FIS.Business.Models;
using FMC.FIS.Business.Models.Boleto;
using FMC.FIS.Business.Models.CREDZ;
using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.DAO
{
    public class EnderecoDAO : AbstractRepositoryPersistence<Endereco>
    {
        public EnderecoDAO() : base("CNN_BOLETO") { }

        public Endereco GetEndereco(string logradouro, string numero, string complemento, string bairro, string cidade, string UF, string cep)
        {
            return this.Context.AsQueryable<Endereco>()
                      .Where<Endereco>(p => p.Logradouro == logradouro)
                      .Where<Endereco>(p => p.Numero == numero)
                      .Where<Endereco>(p => p.Complemento == complemento)
                      .Where<Endereco>(p => p.Bairro == bairro)
                      .Where<Endereco>(p => p.Cidade == cidade)
                      .Where<Endereco>(p => p.UF == UF)
                      .Where<Endereco>(p => p.CEP == cep)
                      .FirstOrDefault<Endereco>();
        }

    }

}
