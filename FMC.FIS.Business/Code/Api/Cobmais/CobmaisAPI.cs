using System;
using System.Collections.Generic;
using System.Linq;
using FMC.FIS.Business.Models;

namespace FMC.FIS.Business.Code.Api.Cobmais
{
    public class CobmaisAPI
    {
        public static string GetToken()
        {
            string token;

            var tokenApiBLL = new FMC.FIS.BLL.TokenApiBLL();

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

        public static Models.Cobmais.EnderecoRequest PutEndereco(Models.Cobmais.EnderecoRequest enderecoRequest)
        {
            try
            {
                var token = GetToken();
                var headers = new Dictionary<string, string>();

                headers.Add("Authorization", "Bearer " + token);

                return RestApi.Put<Models.Cobmais.EnderecoRequest, Models.Cobmais.EnderecoRequest>(Constants.UrlApiCobmaisCredz, "clientes/enderecos", enderecoRequest, headers);
            }
            catch
            {
                return null;
            }
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

        public static Models.Cobmais.Acordo Acordo(long idAcordo)
        {
            var headers = new Dictionary<string, string>();

            headers.Add("Ocp-Apim-Subscription-key", "1353eca287624ab68a5bfbf533f3e7d9 ");
            headers.Add("id_credor", "97612309 ");
            ///incluem na url do contrato a possibilidade de buscar um 
            return RestApi.Get<Models.Cobmais.Acordo>("https://api.cobmais.com.br/cobranca", "acordos/" + idAcordo.ToString(), null, headers);
        }

        public static IList<Models.Cobmais.PagamentoResponse> Pagamento(long idPagamento)
        {
            var headers = new Dictionary<string, string>();

            headers.Add("Ocp-Apim-Subscription-key", "1353eca287624ab68a5bfbf533f3e7d9 ");
            headers.Add("id_credor", "97612309 ");
            ///incluem na url do contrato a possibilidade de buscar um 
            return RestApi.Get<IList<Models.Cobmais.PagamentoResponse>>("https://api.cobmais.com.br/cobranca", "pagamento/" + idPagamento.ToString(), null, headers);
        }

        public static Models.Cobmais.SimulacaoAcordoResponse GetSimulacao(Models.Cobmais.SimulacaoAcordoRequest simulacaoAcordoRequest)
        {
            var token = GetToken();

            return RestApi.Post<Models.Cobmais.SimulacaoAcordoResponse, Models.Cobmais.SimulacaoAcordoRequest>(Constants.UrlApiCobmaisCredz, "acordos/simular", simulacaoAcordoRequest, token);

        }

        public static Models.Cobmais.BoletoCompleto GetBoleto(long idBoleto)
        {
            var token = GetToken();
            var headers = new Dictionary<string, string>();

            headers.Add("Authorization", "Bearer " + token);
            ///incluem na url do contrato a possibilidade de buscar um 
            return RestApi.Get<Models.Cobmais.BoletoCompleto>(Constants.UrlApiCobmaisCredz, "boletos/" + idBoleto.ToString(), null, headers);
        }

        public static Models.Cobmais.BoletoCompleto PostBoleto(long idAcordo, string cpf, int parcela, DateTime dtVencimento)
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
            catch (Exception ex)
            {
                if (ex.Message.Contains("Essa parcela já contem um boleto"))
                {
                    var acordo = GetAcordo(idAcordo);
                    if (acordo != null)
                    {
                        var bol = acordo.boletos.Where(p => p.vencimento.Date == dtVencimento.Date).FirstOrDefault();
                        if (bol != null)
                            return GetBoleto(bol.id);
                    }
                    return null;
                }
                else if (ex.Message.Contains("Não é possível gerar o boleto sem"))
                {
                    return TrataEndereco(idAcordo, cpf, parcela, dtVencimento);
                }
                else
                    return null;
            }
        }

        private static Models.Cobmais.BoletoCompleto TrataEndereco(long idAcordo, string cpf, int parcela, DateTime dtVencimento)
        {
            var pessoa = GetPessoa(cpf);
            string cidade = "";
            string uf = "";
            string cep = "";
            var endereco = pessoa.enderecos.Where(p => p.ativo && p.correspondencia && p.id_tipo == 1).FirstOrDefault();
            if (endereco == null)
                endereco = pessoa.enderecos.Where(p => p.ativo).FirstOrDefault();
            if (endereco != null)
            {
                cidade = endereco.cidade;
                uf = endereco.uf;
                cep = endereco.cep;
            }

            int i = pessoa.enderecos.Count - 1;

            while (string.IsNullOrEmpty(cidade) && i >= 0)
            {
                cidade = pessoa.enderecos[i].cidade;
                i--;
            }
            if (string.IsNullOrEmpty(cidade)) cidade = "São Paulo";

            i = pessoa.enderecos.Count - 1;
            while (string.IsNullOrEmpty(uf) && i >= 0)
            {
                uf = pessoa.enderecos[i].uf;
                i--;
            }
            if (string.IsNullOrEmpty(uf)) uf = "SP";

            i = pessoa.enderecos.Count - 1;

            while (string.IsNullOrEmpty(cep) && i >= 0)
            {
                cep = pessoa.enderecos[i].cep;
                i--;
            }
            if (string.IsNullOrEmpty(cep)) cep = "01452002";

            if (endereco == null)
            {
                endereco = pessoa.enderecos.FirstOrDefault();
            }
            if (endereco != null)
            {
                var enderecoRequest = new Models.Cobmais.EnderecoRequest()
                {
                    id_pessoa = pessoa.id_pessoa,
                    endereco = new Models.Cobmais.Endereco()
                    {
                        id = endereco.id,
                        id_tipo = endereco.id_tipo > 0 ? endereco.id_tipo : 1,
                        logradouro = string.IsNullOrEmpty(endereco.logradouro) ? "N/A" : endereco.logradouro,
                        numero = string.IsNullOrEmpty(endereco.numero) ? "1" : endereco.numero,
                        complemento = endereco.complemento,
                        cep = cep,
                        bairro = string.IsNullOrEmpty(endereco.bairro) ? "N/A" : endereco.bairro,
                        cidade = cidade,
                        uf = uf,
                        ativo = true,
                        correspondencia = true,
                        id_pessoa_referencia = endereco.id_pessoa_referencia
                    }
                };
                if (PutEndereco(enderecoRequest) != null)
                    return PostBoleto(idAcordo, cpf, parcela, dtVencimento);
                else
                    return null;
            }
            else
                return null;
        }

        public static Models.Cobmais.Acordo SetAcordo(Models.Cobmais.SimulacaoAcordoRequest simulacaoAcordoRequest)
        {
            var token = GetToken();

            return RestApi.Post<Models.Cobmais.Acordo, Models.Cobmais.SimulacaoAcordoRequest>(Constants.UrlApiCobmaisCredz, "acordos", simulacaoAcordoRequest, token);
        }

        public static Models.Cobmais.PagamentoResponse[] GetPagamentos(Models.Cobmais.PagamentoRequest pagamentoRequest)
        {
            try
            {
                IDictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Ocp-Apim-Subscription-Key", "1353eca287624ab68a5bfbf533f3e7d9");
                headers.Add("id_credor", "97612309");

                return RestApi.Post<Models.Cobmais.PagamentoResponse[], Models.Cobmais.PagamentoRequest>("https://api.cobmais.com.br", "consulta/pagamentos", pagamentoRequest, headers, "", "");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Nenhum Pagamento encontrado para os parâmetros informados"))
                    return null;
                else
                    throw ex;
            }

        }



    }
}
