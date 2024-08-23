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
using FMC.FIS.Business.Models.Boleto;

namespace FMC.FIS.API.Controllers
{
    //[ApiController]
    [Route("api/p1")]
    public class P1Controller : ControllerBase
    {
        public P1Controller()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("cards/{cpf}")]
        public IActionResult GetCards([FromRoute] string cpf)
        {
            try
            {
                var result = new P1BLL().GetCards(cpf);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, Route("card")]
        public IActionResult GetCard([FromBody] CardP1Request cardP1Request)
        {
            try
            {
                var result = new P1BLL().GetCard(cardP1Request.nrToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}