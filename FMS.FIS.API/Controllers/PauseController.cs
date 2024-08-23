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
    [Route("api/pause")]
    public class PauseController : ControllerBase
    {
        public PauseController()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("active")]
        public IActionResult GetActive()
        {
            try
            {
                var pauses = new PauseBLL().GetActive();

                if (pauses != null)
                {
                    return Ok(pauses);
                }
                else
                    throw new Exception("Pausas ativas não encontrado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}