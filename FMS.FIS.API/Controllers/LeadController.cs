using FMC.FIS.Business.Models.FIS;
using FMC.FIS.BLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections;
using FMC.FIS.Model.Pessoa;
using System.Collections.Generic;
using FMC.FIS.Business.BLL;
using Newtonsoft.Json;
using FMC.FIS.Business.Models.Customer;

namespace FMC.FIS.API.Controllers
{
    //[ApiController]
    [Route("api/lead")]
    public class LeadController : ControllerBase
    {
        public LeadController()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("{cpf}/{productType}")]
        public IActionResult Lead([FromRoute] string cpf, int productType)
        {
            try
            {
                var pType = (Constants.ProductType)Enum.ToObject(typeof(Constants.ProductType), productType);
                var person = new PersonBLL().GetByCPF(cpf, pType);

                if (person != null)
                {
                    return Ok(person);
                }
                else
                    throw new Exception("CPF não encontrado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("{idLead}/{productType}")]
        public IActionResult SetAction(long idLead, int productType)
        {
            var pType = (Constants.ProductType)Enum.ToObject(typeof(Constants.ProductType), productType);

            return Ok("Ok");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("SimulacaoParcelamentoFatura/{idProduct}/{cpf}/{vlEntrada}/{saldo}/{minimo}/{dtVencimento}/{age}")]
        public IActionResult GetSimulacaoParcelamentoFatura([FromRoute] long idProduct, [FromRoute] string cpf, [FromRoute] decimal vlEntrada, [FromRoute] decimal saldo, [FromRoute] decimal minimo, [FromRoute] DateTime dtVencimento, [FromRoute] int age)
        {
            try
            {
                var result = new LeadBLL().SimularParcelamentoFatura(cpf, vlEntrada, saldo, minimo, dtVencimento, age);

                if (result != null)
                    return Ok(result);
                else
                    throw new Exception("Simulação de parcelamento de fatura não encontrado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}