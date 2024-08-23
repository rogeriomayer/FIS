using System;
using System.Collections.Generic;
using FMC.FIS.API.Models;

namespace FMC.FIS.API.Code.Api.Cobmais
{
    public class CobmaisAPI
    {
        public static string GetToken()
        {
            string token;

            var tokenApiBLL = new BLL.TokenApiBLL();

            var tokenApi = tokenApiBLL.GetBykey("COBMAISCREDZ");

            if (tokenApi == null || DateTime.Now > tokenApi.DtExpiration)
            {
                var param = new Models.Cobmais.Autenticacao() { usuario = Constants.UserCobmaisCredz, senha = Constants.PassCobmaisCredz };

                var headers = new Dictionary<string, string>();

                headers.Add("Content-Type", "application/json");

                token = RestApi.Post<string, Models.Cobmais.Autenticacao>(Constants.UrlApiCobmaisCredz, "usuario/token", param, headers, null, null);

                if (tokenApi == null)
                {
                    tokenApiBLL.Add(new TokenAPI() { API = "COBMAISCREDZ", Token = token, DtExpiration = DateTime.Now.AddMinutes(10) });
                }
                else
                {
                    tokenApi.DtExpiration = DateTime.Now.AddMinutes(10);
                    tokenApi.Token = token;
                    tokenApiBLL.Update(tokenApi);
                }
            }
            else
            {
                token = tokenApi.Token;
            }

            return token;

        }

        public static Models.Cobmais.Pessoa GetPessoa(string cpf)
        {
            var token = GetToken();
            var headers = new Dictionary<string, string>();

            headers.Add("Authorization", "Bearer " + token);

            return RestApi.Get<Models.Cobmais.Pessoa>(Constants.UrlApiCobmaisCredz, "clientes/dadosCadastrais/" + cpf, null, headers);
        }
        public static IList<Models.Cobmais.Contrato> GetContratos(string cpf, string credor, string numeroContrato)
        {
            var token = GetToken();
            var headers = new Dictionary<string, string>();

            headers.Add("Authorization", "Bearer " + token);
            ///incluem na url do contrato a possibilidade de buscar um 
            return RestApi.Get<IList<Models.Cobmais.Contrato>>(Constants.UrlApiCobmaisCredz, "contratos/" + cpf + "/" + credor + "/" + numeroContrato, null, headers);
        }

        public static IList<Models.Cobmais.Acordo> GetAcordos(string cpf)
        {
            var token = GetToken();
            var headers = new Dictionary<string, string>();

            headers.Add("Authorization", "Bearer " + token);
            ///incluem na url do contrato a possibilidade de buscar um 
            return RestApi.Get<IList<Models.Cobmais.Acordo>>(Constants.UrlApiCobmaisCredz, "acordos/cliente/" + cpf, null, headers);
        }

        public static Models.Cobmais.Acordo GetAcordo(long idAcordo)
        {
            var token = GetToken();
            var headers = new Dictionary<string, string>();

            headers.Add("Authorization", "Bearer " + token);
            ///incluem na url do contrato a possibilidade de buscar um 
            return RestApi.Get<Models.Cobmais.Acordo>(Constants.UrlApiCobmaisCredz, "acordos/" + idAcordo.ToString(), null, headers);
        }

        public static IList<Models.Cobmais.Acordo> GetAcordo(int idAcordo)
        {
            var token = GetToken();
            var headers = new Dictionary<string, string>();

            headers.Add("Authorization", "Bearer " + token);
            ///incluem na url do contrato a possibilidade de buscar um 
            return RestApi.Get<IList<Models.Cobmais.Acordo>>(Constants.UrlApiCobmaisCredz, "acordos/" + idAcordo, null, headers);
        }

        public static Models.Cobmais.SimulacaoAcordoResponse GetSimulacao(Models.Cobmais.SimulacaoAcordoRequest simulacaoAcordoRequest)
        {
            var token = GetToken();

            return RestApi.Post<Models.Cobmais.SimulacaoAcordoResponse, Models.Cobmais.SimulacaoAcordoRequest>(Constants.UrlApiCobmaisCredz, "acordos/simular", simulacaoAcordoRequest, token);

        }

        public static Models.Cobmais.BoletoCompleto GetBoleto(int idBoleto)
        {
            var token = GetToken();
            var headers = new Dictionary<string, string>();

            headers.Add("Authorization", "Bearer " + token);
            ///incluem na url do contrato a possibilidade de buscar um 
            return RestApi.Get<Models.Cobmais.BoletoCompleto>(Constants.UrlApiCobmaisCredz, "boletos/" + idBoleto.ToString(), null, headers);
        }

        public static Models.Cobmais.BoletoCompleto PostBoleto(long idAcordo, int parcela)
        {
            try
            {
                var token = GetToken();
                var headers = new Dictionary<string, string>();

                headers.Add("Authorization", "Bearer " + token);
                ///incluem na url do contrato a possibilidade de buscar um 
                //return RestApi.Get<Models.Cobmais.BoletoCompleto>(Constants.UrlApiCobmaisCredz, "boletos/26884504", null, headers);
                return RestApi.Post<Models.Cobmais.BoletoCompleto, object>(Constants.UrlApiCobmaisCredz, "boletos/" + idAcordo.ToString() + "/" + parcela.ToString(), null, token);
            }
            catch
            {
                return null;
            }
        }

        public static Models.Cobmais.Acordo SetAcordo(Models.Cobmais.SimulacaoAcordoRequest simulacaoAcordoRequest)
        {
            var token = GetToken();

            return RestApi.Post<Models.Cobmais.Acordo, Models.Cobmais.SimulacaoAcordoRequest>(Constants.UrlApiCobmaisCredz, "acordos", simulacaoAcordoRequest, token);
        }

    }
}
