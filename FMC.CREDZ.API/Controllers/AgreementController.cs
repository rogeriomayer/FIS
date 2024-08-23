using FMC.CREDZ.API.Code.Business.BLL;
using FMC.CREDZ.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FMC.CREDZ.API.Controllers
{
    [Route("api/agreement")]
    public class AgreementController : ControllerBase
    {
        public AgreementController()
        {
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var list = new AgreementBLL().GetAll();

                if (list != null)
                {
                    return Ok(list);
                }
                else
                    throw new Exception("Lista de acordo não encontrado para este produto!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("{card}")]
        public IActionResult GetByCard(string card)
        {
            try
            {
                var result = new AgreementBLL().GetLastAgreement(card);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                    throw new Exception("Acordo não encontrado para este produto!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] Agreement agreement)
        {
            try
            {
                var result = new AgreementBLL().Add(agreement);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}