using FMC.FIS.Business.Models.OneB2K;
using System;
using System.Linq;
using System.Collections.Generic;

namespace FMC.FIS.Business.Code.Api.OneB2K
{
    public class OneB2KApi
    {

        public static Token GetToken()
        {
            var param = new Dictionary<string, string>();

            var headers = new Dictionary<string, string>();

            headers.Add("Content-Type", "application/x-www-form-urlencoded");

            param.Add("grant_type", "password");
            param.Add("username", Constants.USER_CODE_CONNECT);
            param.Add("password", Constants.PASS_CODE_CONNECT);

            //return new Token() { access_token = "b352a01d-be27-3676-9ad6-1622ba17efd3", token_type = "Bearer" };
            return RestApi.Post<Token, Dictionary<string, string>>(Constants.URL_API_CODE_CONNECT, "token", param, headers, Constants.TOKEN_CODE_CONNECT, "Basic");



        }


        public static Portador GetPortador(string cpf, string chaveRestart, bool ativo)
        {
            var token = GetToken();

            var headers = new Dictionary<string, string>();
            headers.Add("nrCPF", cpf);
            headers.Add("idContaAtiva", ativo ? "S" : "N");
            headers.Add("chaveRestart", chaveRestart);
            headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

            return RestApi.Get<Portador>(Constants.URL_API_CODE_CONNECT, "cadastral/1.0.0/oneb2k/portador/porCPF", null, headers);

        }

        public static string GetNrCard(string cpf)
        {
            var portador = GetPortador(cpf, string.Empty, false);
            if (portador != null && portador.responseData.listaPortadores != null && portador.responseData.listaPortadores.Count > 0)
            {
                return portador.responseData.listaPortadores.Where(p => p.cartao != null).FirstOrDefault().cartao.nrCartao;
            }
            else
                return String.Empty;
        }

        public static Status SetPortador(string nrConta, PortadorRequest portadorRequest)
        {
            var token = GetToken();

            var headers = new Dictionary<string, string>();
            headers.Add("nrConta", nrConta);
            headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

            return RestApi.Put<Status, PortadorRequest>(Constants.URL_API_CODE_CONNECT, "cadastral/1.0.0/oneb2k/conta/dadosCadastrais", portadorRequest);
        }

        public static ResumoFatura GetResumoFatura(string jwtCartao, string nrConta, DateTime dtVencimentoFatura)
        {
            var token = GetToken();

            var headers = new Dictionary<string, string>();
            headers.Add("jwt-cartao", jwtCartao);
            headers.Add("dtVencimentoFatura", dtVencimentoFatura.ToString("yyyyMMdd"));
            headers.Add("nrConta", nrConta);
            headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

            return RestApi.Get<ResumoFatura>(Constants.URL_API_CODE_CONNECT, "fatura/1.0.0/oneb2k/cartao/resumoFatura", null, headers);
        }

        public static LinhaDigitavelResponse GetLinhaDigitavelFatura(string nrConta)
        {
            var token = GetToken();

            var headers = new Dictionary<string, string>();
            headers.Add("nrConta", nrConta);
            headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

            return RestApi.Get<LinhaDigitavelResponse>(Constants.URL_API_CODE_CONNECT, "fatura/1.0.0/oneb2k/conta/linhaDigitavel", null, headers);
        }

        public static bool EnvioLinhaDigitavelSMS(string nrConta)
        {
            try
            {
                var token = GetToken();

                var headers = new Dictionary<string, string>();
                headers.Add("nrConta", nrConta);
                headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

                RestApi.Post<object, object>(Constants.URL_API_CODE_CONNECT, "fatura/1.0.0/oneb2k/conta/solicitar/envioLinhaDigitavelSms", "", headers);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public static IdFaturaPDFResponse GetIdFaturaPDF(string cpf, string nrConta, string tpCliente, DateTime dInicio, DateTime dtFim)
        {
            var token = GetToken();

            var headers = new Dictionary<string, string>();
            headers.Add("nrCPF", cpf);
            headers.Add("nrConta", nrConta);
            headers.Add("tpCliente", tpCliente);
            headers.Add("dtInicio", dInicio.ToString("yyyy-MM-dd"));
            headers.Add("dtFim", dtFim.ToString("yyyy-MM-dd"));
            headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

            return RestApi.Get<IdFaturaPDFResponse>(Constants.URL_API_CODE_CONNECT, "fatura/1.0.0/oneb2k/conta/idFaturaPDF", null, headers);
        }

        public static byte[] GetFaturaPDF(string IdPDF)
        {
            var token = GetToken();

            var headers = new Dictionary<string, string>();
            headers.Add("docID", IdPDF);
            headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

            return RestApi.Download(Constants.URL_API_CODE_CONNECT, "fatura/1.0.0/oneb2k/conta/downloadFaturaPDF", null, headers);
        }


        public static ResumoSaldoAtualizadoResponse GetResumoSaldoAtualizado(string nrConta, DateTime dtAtualizacao)
        {
            var token = GetToken();

            var headers = new Dictionary<string, string>();
            headers.Add("nrConta", nrConta);
            headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

            return RestApi.Put<ResumoSaldoAtualizadoResponse, SaldoAtualizadoRequest>(Constants.URL_API_CODE_CONNECT, "fatura/1.0.0/oneb2k/conta/apos/saldo", new SaldoAtualizadoRequest { renegParcelado = "N", dtPayOff = dtAtualizacao.ToString("yyyyMMdd") }, headers);
        }

        public static DevedorResponse GetDevedor(string nrConta, DateTime dtAtualizacao)
        {
            var token = GetToken();

            var headers = new Dictionary<string, string>();
            headers.Add("nrConta", nrConta);
            headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

            return RestApi.Put<DevedorResponse, SaldoAtualizadoRequest>(Constants.URL_API_CODE_CONNECT, "fatura/1.0.0/oneb2k/conta/devedor", new SaldoAtualizadoRequest { renegParcelado = "N", dtPayOff = dtAtualizacao.ToString("yyyyMMdd") }, headers);
        }
        public static DadosCadastraisRespose GetDadosCadastrais(string nrConta)
        {
            var token = GetToken();

            var headers = new Dictionary<string, string>();
            headers.Add("nrConta", nrConta);
            headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

            return RestApi.Get<DadosCadastraisRespose>(Constants.URL_API_CODE_CONNECT, "cadastral/1.0.0/oneb2k/conta/dadosCadastrais", null, headers);
        }

        public static DetalhesSaldoDevedorResponse GetDetalhesSaldoDevedorResponse(string nrConta, DateTime dtAtualizacao)
        {
            var token = GetToken();

            var headers = new Dictionary<string, string>();
            headers.Add("nrConta", nrConta);
            headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

            return RestApi.Put<DetalhesSaldoDevedorResponse, SaldoAtualizadoRequest>(Constants.URL_API_CODE_CONNECT, "fatura/1.0.0/oneb2k/conta/devedor", new SaldoAtualizadoRequest { renegParcelado = "N", dtPayOff = dtAtualizacao.ToString("yyyyMMdd") }, headers);
        }

        public static ParcelamentoFaturaResponse GetSimulacaoParcelamentoFatura(string nrConta, SimulacaoParcelamentoFaturaRequest parcelamentoFaturaRequest)
        {
            var token = GetToken();

            var headers = new Dictionary<string, string>();
            headers.Add("nrConta", nrConta);
            headers.Add("funcao", "00");
            headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

            return RestApi.Post<ParcelamentoFaturaResponse, SimulacaoParcelamentoFaturaRequest>(Constants.URL_API_CODE_CONNECT, "fatura/1.0.0/oneb2k/conta/parcelamentoFatura/simulacao", parcelamentoFaturaRequest, headers, "", "");
        }

        public static ParcelamentoFaturaResponse SetParcelamentoFatura(string nrConta, ParcelamentoFaturaRequest parcelamentoFaturaRequest)
        {
            var token = GetToken();

            var headers = new Dictionary<string, string>();
            headers.Add("nrConta", nrConta);
            headers.Add("Authorization", string.Format("{0} {1}", token.token_type, token.access_token));

            return RestApi.Post<ParcelamentoFaturaResponse, ParcelamentoFaturaRequest>(Constants.URL_API_CODE_CONNECT, "fatura/1.0.0/oneb2k/conta/parcelamentoFatura", parcelamentoFaturaRequest, headers, "", "");
        }

    }
}
