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
    [Route("api/parameter")]
    public class ParameterController : ControllerBase
    {
        public ParameterController()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("{productType}")]
        public IActionResult GetParameter([FromRoute] byte productType)
        {
            try
            {
                var listParameters = new ParameterBLL().GetByProductType(productType);

                if (listParameters != null)
                {
                    return Ok(listParameters);
                }
                else
                    throw new Exception("Lista de parametros não encontrado para este produto!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}