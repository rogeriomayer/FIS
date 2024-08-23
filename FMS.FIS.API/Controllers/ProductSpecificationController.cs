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
    [Route("api/productspecification")]
    public class ProductSpecificationController : ControllerBase
    {
        public ProductSpecificationController()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("{productType}")]
        public IActionResult GetAll([FromRoute] byte productType)
        {
            try
            {
                var list = new ProductSpecificationBLL().GetByProductType(productType);

                if (list != null)
                {
                    return Ok(list);
                }
                else
                    throw new Exception("Lista de produtos não encontrado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}