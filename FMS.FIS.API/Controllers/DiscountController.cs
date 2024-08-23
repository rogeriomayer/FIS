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
    [Route("api/discount")]
    public class DiscountController : ControllerBase
    {
        public DiscountController()
        {
        }

        [SwaggerOperation(Description = "Retorna uma lista de todos os descontos e faixa do cliente. ProductType -> FIS =1, AFINZ = 2,  Credz = 3")]
        [Produces("application/json", "application/xml", Type = typeof(ICollection<Discount>))]
        [HttpGet, Route("{productType}")]
        public IActionResult GetDiscount([FromRoute] int productType)
        {
            try
            {
                var listStatus = new DiscountBLL().GetByProductType(productType);

                if (listStatus != null)
                {
                    return Ok(listStatus);
                }
                else
                    throw new Exception("Lista de descontos não encontrado para este produto!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}