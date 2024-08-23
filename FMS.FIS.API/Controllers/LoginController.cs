using FMC.FIS.BLL;
using Microsoft.AspNetCore.Mvc;
using System;
using FMC.FIS.Business.Code.Helpers;
using FMC.FIS.Business.Models;
using Microsoft.AspNetCore.Authorization;
using FMC.FIS.Business.BLL;
using System.Linq;
using Swashbuckle.AspNetCore.Annotations;

namespace FMC.FIS.API.Controllers
{
    //[ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        public LoginController()
        {
        }

        /*
        [HttpGet, AllowAnonymous]
        public IActionResult GetLogin()
        {
            try
            {
                var value = Code.Api.OneB2K.OneB2KApi.GetPortador("06886336681", "", false);

                var cartao = value.responseData.listaPortadores.Where(p => p.cartao != null).FirstOrDefault().cartao;
                string conta = value.responseData.listaPortadores.Where(p => p.nrConta.StartsWith(cartao.nrCartao.Substring(0, 4))).FirstOrDefault().nrConta;

                var fatura = Code.Api.OneB2K.OneB2KApi.GetResumoFatura(cartao.jwtCartao, conta, DateTime.Today.AddDays(2));
                var resumoSaldoAtualizado = Code.Api.OneB2K.OneB2KApi.GetResumoSaldoAtualizado(conta, DateTime.Today);
                var detalhesSaldoDevedorResponse = Code.Api.OneB2K.OneB2KApi.GetDetalhesSaldoDevedorResponse(conta, DateTime.Today);
                var idPdf = Code.Api.OneB2K.OneB2KApi.GetIdFaturaPDF("06886336681", conta, "PF", DateTime.Today.AddMonths(-10), DateTime.Today);
                var pdf = Code.Api.OneB2K.OneB2KApi.GetFaturaPDF(idPdf.responseData.listaPDF.LastOrDefault().idPDF);
                //
                var fisLogin = new FisLoginBLL().Login("rogerio.mayer", "Fis@1234");
                if (value != null)
                    return Ok(fatura);
                else
                    throw new Exception("Usuário ou senha inválidos");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        */

        [HttpPost, AllowAnonymous]
        public IActionResult Login([FromBody] Login _user)
        {
            try
            {
                var fisLogin = new FisLoginBLL().Login(_user.User, _user.Password);
                if (fisLogin != null)
                    return Ok(fisLogin);
                else
                    throw new Exception("Usuário ou senha inválidos");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, Route("idp"), AllowAnonymous]
        public IActionResult LoginFromIDP([FromBody] LoginIDP login)
        {
            try
            {
                var fisLogin = new FisLoginBLL().Login(login);
                if (fisLogin != null)
                    return Ok(fisLogin);
                else
                    throw new Exception("Usuário ou senha inválidos");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, Route("ChangePassword")]
        [Authorize]
        public IActionResult ChangePassword([FromBody] Login _user)
        {
            try
            {
                if (PasswordUtil.GetPassPoint(_user.Password) < 80)
                    throw new Exception("Weak Password");
                else
                {
                    new FisLoginBLL().ChangePassword(_user.User, _user.Password);
                    return Ok(true);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("UserProfile/{usersProfile}/{idProductType}")]
        //[Authorize]
        public IActionResult GetUserProfile([FromRoute] string usersProfile, [FromRoute] byte idProductType)
        {
            try
            {
                var listUserProfile = usersProfile.Split(',').ToList();
                var userProfile = new UserProfileBLL().GetByName(listUserProfile, idProductType);

                if (userProfile != null)
                    return Ok(userProfile);
                else
                    throw new Exception("UserProfile não encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("all/{idProductType}")]
        //[Authorize]
        public IActionResult GetUserLogin([FromRoute] byte idProductType)
        {
            try
            {
                var users = new FisLoginBLL().GetByProductType(idProductType);
                if (users != null)
                    return Ok(users);
                else
                    throw new Exception("Lista de usuários não encontrada!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("manager/{idManager}")]
        //[Authorize]
        public IActionResult GetUserByManager([FromRoute] int idManager)
        {
            try
            {
                var users = new FisLoginBLL().GetUserIdManager(idManager);
                if (users != null)
                    return Ok(users);
                else
                    throw new Exception("Lista de usuários não encontrada!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

    /// <summary>
    /// Login para geração de token de acesso
    /// </summary>
    [SwaggerSchema(Description = "Login para geração de token de acesso")]
    public class Login
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}
