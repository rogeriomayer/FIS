using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections;
using FMC.FIS.Model.Pessoa;
using System.Collections.Generic;
using Newtonsoft.Json;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Models.Customer;
using Swashbuckle.AspNetCore.Annotations;

namespace FMC.FIS.API.Controllers
{
    //[ApiController]
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        public PersonController()
        {
        }

        [SwaggerOperation(Description = "Retorna dados pessoais e produtos do cliente pelo cpf. ProductType -> FIS =1, AFINZ = 2,  Credz = 3")]
        [Produces("application/json", "application/xml", Type = typeof(PersonResponse))]
        [HttpGet, Route("{cpf}/{productType}")]
        public IActionResult Person([FromRoute] string cpf, int productType)
        {
            try
            {
                var pType = (Constants.ProductType)Enum.ToObject(typeof(Constants.ProductType), productType);
                var person = new PersonBLL().GetByCPF(cpf, pType);

                if (person != null)
                {
                    return Ok(person);
                }
                else
                    throw new Exception("CPF não encontrado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Description = "Retorna dados pessoais e produtos do cliente pelo cpf. ProductType -> FIS =1, AFINZ = 2,  Credz = 3")]
        [Produces("application/json", "application/xml", Type = typeof(PersonResponse))]
        [HttpGet, Route("{cpf}/{cardToken}/{productType}")]
        public IActionResult PersonByCard([FromRoute] string cpf, string cardToken, int productType)
        {
            try
            {
                var pType = (Constants.ProductType)Enum.ToObject(typeof(Constants.ProductType), productType);
                var person = new PersonBLL().GetByCPF(cpf, cardToken, pType);

                if (person != null)
                {
                    return Ok(person);
                }
                else
                    throw new Exception("CPF não encontrado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Description = "Atualiza dados pessoais do cliente. Product Type -> FIS =1, AFINZ = 2,  Credz = 3")]
        [Produces("application/json", "application/xml", Type = typeof(string))]
        [HttpPost, Route("{productType}")]
        public IActionResult Person([FromBody] PersonUpdateRequest personUpdateRequest, [FromRoute] int productType)
        {
            try
            {
                var pType = (Constants.ProductType)Enum.ToObject(typeof(Constants.ProductType), productType);
                new PersonBLL().UpdatePerson(personUpdateRequest, pType);

                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}