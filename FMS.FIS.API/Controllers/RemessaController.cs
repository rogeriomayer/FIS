using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Models;
using FMC.FIS.Business.Models.Boleto;
using FMC.FIS.BLL;
using FMC.FIS.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.API.Controllers
{
    //[ApiController]
    [Route("api/remessa")]
    public class RemessaController : ControllerBase
    {
        public RemessaController()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("{dtInicio}/{dtFim}")]
        public IActionResult GetRemessas(string dtInicio, string dtFim)
        {

            var remessas = new RemessaBLL().GetRemessas(dtInicio, dtFim);
            return Ok(remessas);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("{idRemessa}")]
        public IActionResult GetRemessa(long idRemessa)
        {
            try
            {
                var remessa = new RemessaBLL().GetRemessa(idRemessa);
                return Ok(remessa);
            }
            catch (Exception ex)
            {
                return new UtilException().Error(ex);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("SetDownloadRemessa/{idRemessa}/{idUserLogin}")]
        public IActionResult SetDownloadRemessa(long idRemessa, int idUserLogin)
        {
            try
            {
                new DownloadCNABBLL().Add
                (
                    new DownloadCNAB()
                    {
                        IdRemessa = idRemessa,
                        IdUserLogin = idUserLogin,
                        DtDownload = DateTime.Now
                    }
                );

                return Ok(true);
            }
            catch (Exception ex)
            {
                return new UtilException().Error(ex);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, Route("retorno")]
        public IActionResult SetRetorno([FromBody] RetornoRequest retornoRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(retornoRequest.NomeArquivo) || !retornoRequest.NomeArquivo.ToUpper().EndsWith(".RET"))
                    throw new Exception("Erro:Nome do arquivo inválido! O nome do arquivo deve ter a extensão .RET");
                if (retornoRequest.Arquivo == null)
                    throw new Exception("Erro: Arquivo inválido!");

                var ret = new RemessaBLL().SetRetorno(retornoRequest.NomeArquivo, retornoRequest.Arquivo, retornoRequest.IdUserLogin);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("retorno/{dias}")]
        public IActionResult GetRetorno([FromRoute] int dias)
        {
            try
            {
                var listUpload = new UploadCNABBLL().GetLasts(dias);

                ICollection<RetornoResponse> listRetorno = new HashSet<RetornoResponse>();

                listUpload.OrderByDescending(p => p.DtUpload).ToList().ForEach(p =>
                     listRetorno.Add
                     (
                         new RetornoResponse()
                         {
                             NomeArquivo = p.DsName,
                             DataUpload = p.DtUpload.ToString("dd/MM/yyyy HH:mm"),
                             UserLogin = new FisLoginBLL().GetBykey(p.IdUserLogin).DsName
                         }
                     )
                );

                return Ok(listRetorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
