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
using Swashbuckle.AspNetCore.Annotations;

namespace FMC.FIS.API.Controllers
{
    [Route("api/status")]
    public class StatusController : ControllerBase
    {
        public StatusController()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("{productType}")]
        public IActionResult GetStatus([FromRoute] int productType)
        {
            try
            {
                var listStatus = new StatusBLL().GetByProductType(productType);

                if (listStatus != null)
                {
                    return Ok(listStatus);
                }
                else
                    throw new Exception("Lista de Status não encontrado para este produto!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [SwaggerOperation(Description = "Adiciona uma ação que foi realizada no CPF do cliente. ProductType -> FIS =1, AFINZ = 2,  Credz = 3")]
        [Produces("application/json", "application/xml", Type = typeof(StatusLeadResponse))]
        [HttpPost, Route("{cpf}/{telefone}/{productType}")]
        public IActionResult InsertStatusLad([FromBody] StatusLead statusLead, [FromRoute] string cpf, [FromRoute] string telefone, [FromRoute] int productType)
        {
            try
            {
                var pType = (Constants.ProductType)Enum.ToObject(typeof(Constants.ProductType), productType);
                var result = new StatusLeadBLL().Add(statusLead, cpf, telefone, pType);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("statuslead/{idUser}/{idProductType}/{dtIni}/{dtFim}")]
        public IActionResult GetStatusLead([FromRoute] int idUser, [FromRoute] int idProductType, [FromRoute] DateTime dtIni, [FromRoute] DateTime dtFim)
        {
            try
            {
                dtIni = Convert.ToDateTime(dtIni.ToString("yyyy-MM-dd") + " 00:00:00");
                dtFim = Convert.ToDateTime(dtFim.ToString("yyyy-MM-dd") + " 23:59:59");
                var listStatus = new StatusLeadBLL().GetByIdUser(idUser, idProductType, dtIni, dtFim);

                if (listStatus != null)
                {
                    return Ok(listStatus);
                }
                else
                    throw new Exception("Lista de Status não encontrado para este produto!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, Route("statuslead/{idProductType}")]
        public IActionResult GetStatusLead([FromRoute] byte idProductType, [FromBody] StatusLeadRequest statusLeadRequest)
        {
            try
            {
                statusLeadRequest.dtIni = Convert.ToDateTime(statusLeadRequest.dtIni.ToString("yyyy-MM-dd") + " 00:00:00");
                statusLeadRequest.dtFim = Convert.ToDateTime(statusLeadRequest.dtFim.ToString("yyyy-MM-dd") + " 23:59:59");
                var listStatus = new StatusLeadBLL().GetbyIdsUser(statusLeadRequest, idProductType);

                if (listStatus != null)
                {
                    return Ok(listStatus);
                }
                else
                    throw new Exception("Lista de Status não encontrado para este produto!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("promisseType/{idProductType}")]
        public IActionResult GetPromisseType(int idProductType)
        {
            try
            {
                var listPromisseType = new PromisseTypeBLL().GetAll(idProductType);

                if (listPromisseType != null)
                {
                    return Ok(listPromisseType);
                }
                else
                    throw new Exception("Lista de tipo de promessa não encontrado para este produto!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}