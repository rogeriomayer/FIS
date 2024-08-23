using FMC.CREDZ.API.Code.Business.BLL;
using FMC.CREDZ.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FMC.CREDZ.API.Controllers
{
    [Route("api/pid")]
    public class PIDController : ControllerBase
    {
        public PIDController()
        {
        }

        [HttpGet, Route("{name}/{count}")]
        public IActionResult GetAll([FromRoute] string name, [FromRoute] int count)
        {
            try
            {
                var list = new PIDNamesBLL().GetNames(name, count);

                if (list != null)
                    return Ok(list);

                else
                    throw new Exception("Lista de Navigation não encontrado para este produto!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}