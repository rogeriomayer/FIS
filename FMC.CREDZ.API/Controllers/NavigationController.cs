using FMC.CREDZ.API.Code.Business.BLL;
using FMC.CREDZ.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FMC.CREDZ.API.Controllers
{
    [Route("api/navigation")]
    public class NavigationController : ControllerBase
    {
        public NavigationController()
        {
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var list = new NavigationBLL().GetAll();

                if (list != null)
                {
                    return Ok(list);
                }
                else
                    throw new Exception("Lista de Navigation não encontrado para este produto!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPost]
        public IActionResult Insert([FromBody] Navigation navigation)
        {
            try
            {
                var result = new NavigationBLL().Add(navigation);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}