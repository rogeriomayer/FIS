using FMC.FIS.BLL;
using FMC.FIS.Business.Code.Api.OneB2K;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Models;
using FMC.FIS.Business.Models.Customer;
using FMC.FIS.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Annotations;

namespace FMC.FIS.API.Controllers
{
    //[ApiController]
    [Route("api/billet")]
    public class BilletController : ControllerBase
    {
        public BilletController()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, Route("BilletP2")]
        public IActionResult BilletP2([FromBody] BilletRequest billetRequest)
        {
            try
            {
                if (!TextUtil.IsCpf(billetRequest.CPF))
                    throw new Exception("Erro:CPF inválido!");
                // if (TextUtil.IsDate(billetRequest.Date))
                //     throw new Exception("Erro:Data inválida!");

                if (billetRequest.Account.Length != 19 && (!billetRequest.Account.StartsWith("000")) && (!billetRequest.Account.EndsWith("000")))
                    throw new Exception("Erro: Não foi informado um número de conta válido! Número de conta deve ter 19 caracteres, iniciar e terminar com 000");

                var billet = new BilletP2BLL().GetBillet(billetRequest);

                return Ok(billet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, Route("BilletP1")]
        public IActionResult BilletP1([FromBody] BilletRequest billetRequest)
        {
            try
            {
                if (!TextUtil.IsCpf(billetRequest.CPF))
                    throw new Exception("Erro:CPF inválido!");
                //if (TextUtil.IsDate(billetRequest.Date))
                //    throw new Exception("Erro:Data inválida!");
                if (billetRequest.Account.Length != 16)
                    throw new Exception("Erro: Não foi informado um número de cartão válido!");

                var billet = new BilletP1BLL().GetBillet(billetRequest);

                return Ok(billet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("{idProduct}")]
        public IActionResult GetBillet([FromRoute] long idProduct)
        {
            try
            {
                var billet = new BilletBLL().GetByIdProduct(idProduct);
                return Ok(billet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("pdf/{cpf}/{codBillet}/{dtPayment}/{productType}")]
        public IActionResult GetPDF([FromRoute] string cpf, [FromRoute] string codBillet, [FromRoute] DateTime dtPayment, [FromRoute] int productType)
        {
            try
            {
                var pType = (Constants.ProductType)Enum.ToObject(typeof(Constants.ProductType), productType);
                //var billet = new BilletBLL().GetPDF(cpf, codBillet, dtPayment, pType);
                //return Ok(billet);
                return null;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Description = "Envia o boleto para o cliente. ProductType -> FIS =1, AFINZ = 2,  Credz = 3")]
        [Produces("application/json", "application/xml", Type = typeof(Business.Models.Customer.BilletResponse))]
        [HttpPost, Route("sendEmail/{productType}")]
        public IActionResult SendEmail([FromBody] SendEmailRequest sendEmailRequest, [FromRoute] int productType)
        {
            try
            {
                var pType = (Constants.ProductType)Enum.ToObject(typeof(Constants.ProductType), productType);
                var billet = new BilletBLL().SendEmail(sendEmailRequest, pType);
                return Ok(billet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, Route("sendSMS/{productType}")]
        public IActionResult SendSMS([FromBody] SendSMSRequest sendSMSRequest, [FromRoute] int productType)
        {
            try
            {
                var pType = (Constants.ProductType)Enum.ToObject(typeof(Constants.ProductType), productType);
                /*
                var billet = new BilletBLL().SendSMS(sendSMSRequest.idProduct, sendSMSRequest.cpf, sendSMSRequest.codBillet,
                                                       sendSMSRequest.parcel, sendSMSRequest.dtPayment,
                                                       sendSMSRequest.phone, sendSMSRequest.idUserLogin, pType);
                return Ok(billet);
                */

                return null;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Description = "Cria um novo boleto para o cliente. ProductType -> FIS =1, AFINZ = 2,  Credz = 3")]
        [Produces("application/json", "application/xml", Type = typeof(Business.Models.Customer.BilletResponse))]
        [HttpPost, Route("{productType}")]
        public IActionResult AddBillet([FromBody] NewBilletRequest newBilletRequest, [FromRoute] int productType)
        {
            try
            {
                var pType = (Constants.ProductType)Enum.ToObject(typeof(Constants.ProductType), productType);

                var billet = new BilletBLL().AddNewBillet(newBilletRequest, pType);
                return Ok(billet);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("FaturaPDF/{cpf}/{nrConta}/{tpCliente}/{dtIni}/{dtFim}")]
        public IActionResult GetFaturaPDF([FromRoute] string cpf, [FromRoute] string nrConta, [FromRoute] string tpCliente, [FromRoute] string dtIni, [FromRoute] string dtFim)
        {
            try
            {
                var idPdf = OneB2KApi.GetIdFaturaPDF(cpf, nrConta, tpCliente, Convert.ToDateTime(dtIni), Convert.ToDateTime(dtFim));
                if (idPdf != null && idPdf.responseData != null && idPdf.responseData.listaPDF != null && idPdf.responseData.listaPDF.Count > 0)
                {
                    var ultimaFatura = idPdf.responseData.listaPDF.OrderByDescending(p => p.dtVencimento).FirstOrDefault();
                    return Ok(OneB2KApi.GetFaturaPDF(ultimaFatura.idPDF));
                }
                else
                {
                    return BadRequest("Fatura não encontrada!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("LinhaDigitavelFatura/{nrConta}")]
        public IActionResult GetLinhaDigitavelFatura([FromRoute] string nrConta)
        {
            try
            {
                return Ok(OneB2KApi.GetLinhaDigitavelFatura(nrConta));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("LinhaDigitavelFaturaSMS/{nrConta}")]
        public IActionResult LinhaDigitavelFaturaSMS([FromRoute] string nrConta)
        {
            try
            {
                return Ok(OneB2KApi.EnvioLinhaDigitavelSMS(nrConta));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
