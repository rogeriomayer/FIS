using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections;
using FMC.FIS.Model.Pessoa;
using System.Collections.Generic;
using Newtonsoft.Json;
using FMC.FIS.Business.Code.Api.TWW;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Models.Customer;
using FMC.FIS.Business.Models.BvTelecom;

namespace FMC.FIS.API.Controllers
{
    //[ApiController]
    [Route("api/sms")]
    public class SMSController : ControllerBase
    {
        public SMSController()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, Route("{productType}")]
        public IActionResult SendSMS([FromBody] SMSRequest smsRequest, [FromRoute] int productType)
        {
            try
            {
                var pType = (Constants.ProductType)Enum.ToObject(typeof(Constants.ProductType), productType);
                string ret = "";
                if (pType == Constants.ProductType.FIS)
                {
                    ret = TwwApi.SendSms(smsRequest);
                }
                else if (pType == Constants.ProductType.CREDZ)
                {
                    ret = new BvSmsBLL().SmsSingle
                           (
                               new SingleRequest()
                               {
                                   celular = smsRequest.phone,
                                   mensagem = smsRequest.message,
                                   carteiraId = 1064,
                                   parceiroId = "credZ" + DateTime.Now.ToString("ddMMyyyyHHmmss")
                               }
                           );
                }
                if (ret.ToUpper() == "OK")
                    return Ok("Ok");
                else
                    return BadRequest("SMS não enviado!" + ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("{phone}/{message}")]
        public IActionResult getSendSMS([FromRoute] string phone, [FromRoute] string message)
        {
            try
            {
                string ret = TwwApi.SendSms(new SMSRequest() { phone = phone, message = message });
                if (ret.ToUpper() == "OK")
                    return Ok("Ok");
                else
                    return BadRequest("SMS não enviado! " + ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}