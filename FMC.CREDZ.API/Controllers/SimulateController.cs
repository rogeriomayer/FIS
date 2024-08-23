using FMC.CREDZ.API.Code.Business.BLL;
using FMC.CREDZ.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FMC.CREDZ.API.Controllers
{
    [Route("api/simulate")]
    public class SimulateController : ControllerBase
    {
        public SimulateController()
        {
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var list = new SimulateBLL().GetAll();

                if (list != null)
                {
                    return Ok(list);
                }
                else
                    throw new Exception("Lista de Product não encontrado para este produto!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] Simulate simulate)
        {
            try
            {
                var result = new SimulateBLL().Add(simulate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}