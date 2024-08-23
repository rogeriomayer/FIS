using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FMC.FIS.Business.Code.Api.Recupera
{
    /*
    public class AfinzRecuperaAPI
    {
        private static RecuperaSoapApi.ApiClient ApiClient = new RecuperaSoapApi.ApiClient(RecuperaSoapApi.ApiClient.EndpointConfiguration.BasicHttpBinding_IApi1);
        private static RecuperaSoapApi.TokenAcesso TokenRecuperaAfinz
        {
            get
            {
                var tokenAcesso = new RecuperaSoapApi.TokenAcesso();
                tokenAcesso.User = Constants.UserRecuperaAfinz;
                tokenAcesso.Password = Constants.PassRecuperaAfinz;
                tokenAcesso.Token = Constants.TokenRecuperaAfinz;

                return tokenAcesso;
            }
        }

        private static RecuperaSoapApi.ChaveCliente ChaveCliente(string cpf, string codCredor)
        {
            return new RecuperaSoapApi.ChaveCliente()
            {
                CodigoCliente = cpf,
                CodigoCredor = codCredor
            };
        }


        public static IList<RecuperaSoapApi.Cliente> ConsultarClientePorCPF(string cpf)
        {
            try
            {
                var consultarClienteResult = ApiClient.ConsultarClientePorCPF(AfinzRecuperaAPI.TokenRecuperaAfinz, cpf);
                if (consultarClienteResult != null && consultarClienteResult.StatusRetorno.CodigoRetorno == 0 && consultarClienteResult.Clientes.Count > 0)
                    return consultarClienteResult.Clientes;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IList<RecuperaSoapApi.Contrato> ConsultarDividaEmAberto(string cpf, DateTime dtCalculo, decimal pctDesconto)
        {
            try
            {
                var consultarDividaEmAbertoResponse = ApiClient.ConsultarDividaEmAberto(AfinzRecuperaAPI.TokenRecuperaAfinz, ChaveCliente(cpf, "1"), dtCalculo, Convert.ToDouble(pctDesconto));
                if (consultarDividaEmAbertoResponse != null && consultarDividaEmAbertoResponse.StatusRetorno.CodigoRetorno == 0 && consultarDividaEmAbertoResponse.Contratos.Count > 0)
                {
                    return consultarDividaEmAbertoResponse.Contratos;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static RecuperaSoapApi.ConsultarParcelamentosResult ConsultarParcelamentos(string cpf, DateTime dtAtualizacaoCalculo, double pctDesconto)
        {
            try
            {
                var consultarParcelamentosResult = ApiClient.ConsultarParcelamentos(AfinzRecuperaAPI.TokenRecuperaAfinz, ChaveCliente(cpf, "1"), dtAtualizacaoCalculo, pctDesconto);
                if (consultarParcelamentosResult != null && consultarParcelamentosResult.StatusRetorno.CodigoRetorno == 0 && consultarParcelamentosResult.Parcelamentos.Count > 0)
                {
                    return consultarParcelamentosResult;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void AtualizarCliente(RecuperaSoapApi.Cliente cliente)
        {
            try
            {
                var result = ApiClient.AtualizarCliente(AfinzRecuperaAPI.TokenRecuperaAfinz, cliente);

                if (result.StatusRetorno.CodigoRetorno != 0)
                    throw new Exception(result.StatusRetorno.MensagemRetorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void IncluirOcorrencia(string cpf, string ocorrencia, string dataAgenda, string complemento, string telefone)
        {
            try
            {
                var result = ApiClient.IncluirOcorrencia(AfinzRecuperaAPI.TokenRecuperaAfinz, ChaveCliente(cpf, "1"), ocorrencia, dataAgenda, complemento, telefone);

                if (result.StatusRetorno.CodigoRetorno != 0)
                    throw new Exception(result.StatusRetorno.MensagemRetorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ConfirmarOpcaoPagamento(string cpf, string codigoSolicitacaoOpcoesPagamento, string codigoPlano)
        {
            try
            {
                var result = ApiClient.ConfirmarOpcaoPagamento(AfinzRecuperaAPI.TokenRecuperaAfinz, ChaveCliente(cpf, "1"), codigoSolicitacaoOpcoesPagamento, codigoPlano);

                return result.Parcelamento.CodigoParcelamento;

                if (result.StatusRetorno.CodigoRetorno != 0)
                    throw new Exception(result.StatusRetorno.MensagemRetorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string SolicitarOpcoesPagamento(string cpf, DateTime dataPagamento, int percentualDesconto)
        {
            var result = ApiClient.SolicitarOpcoesPagamento(TokenRecuperaAfinz, ChaveCliente(cpf, "1"), dataPagamento, percentualDesconto);
            if (result.StatusRetorno.CodigoRetorno == 0)
                return result.CodigoSolicitacaoOpcoesPagamento;
            else
                throw new Exception(result.StatusRetorno.MensagemRetorno);
        }

        public static string SolicitarOpcoesPagamentoComEntrada(string cpf, DateTime dataPagamento, decimal valorEntrada, int percentualDesconto)
        {
            var result = ApiClient.SolicitarOpcoesPagamentoComEntrada(TokenRecuperaAfinz, ChaveCliente(cpf, "1"), dataPagamento, Convert.ToDouble(valorEntrada), percentualDesconto);
            if (result.StatusRetorno.CodigoRetorno == 0)
                return result.CodigoSolicitacaoOpcoesPagamento;
            else
                throw new Exception(result.StatusRetorno.MensagemRetorno);
        }

        public static RecuperaSoapApi.ConsultarOpcoesPagamentoResult ConsultarOpcoesPagamento(string cpf, string codigoSolicitacao)
        {
            var result = ApiClient.ConsultarOpcoesPagamento(TokenRecuperaAfinz, ChaveCliente(cpf, "1"), codigoSolicitacao);
            if (result.StatusRetorno.CodigoRetorno == 0)
            {
                return result;
            }
            else if (result.StatusRetorno.CodigoRetorno == 9 && result.StatusRetorno.MensagemRetorno.Contains("O cálculo desta Oferta está em processamento."))
            {
                Thread.Sleep(2000);
                return ConsultarOpcoesPagamento(cpf, codigoSolicitacao);
            }
            else
            {
                throw new Exception(result.StatusRetorno.MensagemRetorno);
            }
        }

        public static RecuperaSoapApi.EmitirBoletoResult EmitirBoleto(string cpf, string codigoParcelamento, DateTime dataVencimentoParcela, DateTime dataVencimentoBoleto)
        {
            try
            {
                return ApiClient.EmitirBoleto(TokenRecuperaAfinz, ChaveCliente(cpf, "1"), codigoParcelamento, dataVencimentoParcela, dataVencimentoBoleto);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static byte[] EmitirBoletoPDF(string cpf, string codigoParcelamento, DateTime dataVencimentoParcela, DateTime dataVencimentoBoleto)
        {
            var result = ApiClient.EmitirBoletoPDF(TokenRecuperaAfinz, ChaveCliente(cpf, "1"), codigoParcelamento, dataVencimentoParcela, dataVencimentoBoleto);
            if (result.StatusRetorno.CodigoRetorno == 0)
                return result.BoletoPDF;
            else
                throw new Exception(result.StatusRetorno.MensagemRetorno);
        }

        public static RecuperaSoapApi.Boleto EmitirBoletoSMS(string cpf, string codigoParcelamento, DateTime dataVencimentoParcela, DateTime dataVencimentoBoleto, string telefone)
        {
            var result = ApiClient.EmitirBoletoSMS(TokenRecuperaAfinz, ChaveCliente(cpf, "1"), codigoParcelamento, dataVencimentoParcela, dataVencimentoBoleto, telefone);
            if (result.StatusRetorno.CodigoRetorno == 0)
                return result.Boleto;
            else
                throw new Exception(result.StatusRetorno.MensagemRetorno);
        }

        public static RecuperaSoapApi.Boleto EmitirBoletoEmail(string cpf, string codigoParcelamento, DateTime dataVencimentoParcela, DateTime dataVencimentoBoleto, string email)
        {
            var result = ApiClient.EmitirBoletoEmail(TokenRecuperaAfinz, ChaveCliente(cpf, "1"), codigoParcelamento, dataVencimentoParcela, dataVencimentoBoleto, email);
            if (result.StatusRetorno.CodigoRetorno == 0)
                return result.Boleto;
            else
                throw new Exception(result.StatusRetorno.MensagemRetorno);
        }
    }
    */
}