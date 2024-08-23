using FMC.CREDZ.API.Code.Business.BLL;
using FMC.CREDZ.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FMC.CREDZ.API.Controllers
{
    [Route("api/billet")]
    public class BilletController : ControllerBase
    {
        public BilletController()
        {
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var list = new BilletBLL().GetAll();

                if (list != null)
                {
                    return Ok(list);
                }
                else
                    throw new Exception("Lista de boletos não encontrado para este produto!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] Billet billet)
        {
            try
            {
                var result = new BilletBLL().Add(billet);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}