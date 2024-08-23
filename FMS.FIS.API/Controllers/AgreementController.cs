using FMC.FIS.Business.Models.FIS;
using FMC.FIS.BLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using FMC.FIS.Business.BLL;
using Newtonsoft.Json;
using FMC.FIS.Business.Models.Customer;
using Swashbuckle.AspNetCore.Annotations;
using FMC.FIS.Business.Models.Cobmais;

namespace FMC.FIS.API.Controllers
{
    //[ApiController]
    [Route("api/agreement")]
    public class AgreementController : ControllerBase
    {
        public AgreementController()
        {
        }

        [SwaggerOperation(Description = "Retorna a simulação com as opções de acordo para do cliente. ProductType -> FIS =1, AFINZ = 2,  Credz = 3")]
        [Produces("application/json", "application/xml", Type = typeof(AgreementSimulateResponse))]
        [HttpPost, Route("simulate/{productType}")]
        public IActionResult GetAgreementSimulate([FromBody] AgreementSimulateRequest agreementSimulateRequest, [FromRoute] int productType)
        {
            try
            {
                var pType = (Constants.ProductType)Enum.ToObject(typeof(Constants.ProductType), productType);
                var result = new AgreementBLL().GetAgreementSimulate(agreementSimulateRequest, pType);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Description = "Adicionar um acordo na plataforma CredZ Cobmais.")]
        [Produces("application/json", "application/xml", Type = typeof(Acordo))]
        [HttpPost, Route("addCredz")]
        public IActionResult AddAgreementCredz([FromBody] AgreementSimulateRequest agreementSimulateRequest)
        {
            var result = new AgreementBLL().AddAgreementCredz(agreementSimulateRequest);
            return Ok(result);
        }


    }
}