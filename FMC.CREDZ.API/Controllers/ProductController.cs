using FMC.CREDZ.API.Code.Business.BLL;
using FMC.CREDZ.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FMC.CREDZ.API.Controllers
{
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var list = new ProductBLL().GetAll();

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
        public IActionResult Insert([FromBody] Product product)
        {
            try
            {
                
                var result = new ProductBLL().Add(product);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}