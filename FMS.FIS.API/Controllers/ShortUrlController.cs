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
    [Route("api/shorturl")]
    public class ShortUrlController : ControllerBase
    {
        public ShortUrlController()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("{code}/{ip}")]
        public IActionResult Get([FromRoute] string code, string ip)
        {
            try
            {
                var result = new ShortURLBLL().GetByCode(code, ip);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                    throw new Exception("Codigo não encontrado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}