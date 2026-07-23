using FMC.FIS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMC.FIS.Model;
using FMC.FIS.Business.Models;
using FMC.Generic;
using FMC.FIS.Business.Models.Boleto;
using FMC.FIS.Business.DAO;


namespace FMC.FIS.BLL
{
    public class BilletP2BLL : BLL<BilletP2, BilletP2DAO>
    {

        public BilletResponse GetBillet(BilletRequest billetRequest)
        {
            var url = "http://10.40.0.110/ibi/ura.svc";
            //var url = "http://localhost:34072/URA.svc";

            if (!billetRequest.Account.StartsWith("000"))
                billetRequest.Account = "000" + billetRequest.Account;

            BilletResponse billet = null;
            if (billetRequest.Account.Length == 19)
            {
                var billetParameter = new BilletParameterP2()
                {
                    Carteira = "FISP2",
                    NomeSacado = billetRequest.Name,
                    CPFSacado = billetRequest.CPF,
                    NumeroDocumento = billetRequest.Account,
                    CepSacado = billetRequest.CEP,
                    EnderecoSacado = billetRequest.Address,
                    NumeroSacado = billetRequest.Number,
                    ComplementoSacado = billetRequest.Complement,
                    BairroSacado = billetRequest.District,
                    CidadeSacado = billetRequest.City,
                    EstadoSacado = billetRequest.UF,
                    ValorDocumento = billetRequest.Value,
                    DataVencimento = Convert.ToDateTime(billetRequest.Date),
                    CodEmpresa = "FISCE"
                };

                billet = RestApi.Post<BilletResponse, BilletRequest>(url, "GetBilletFISP2", billetRequest, "");
                //billet = GetBilletFISP2(billetParameter, "FISP2", true);
            }
            if (billet != null)
            {
                try
                {
                    Add
                      (
                          new BilletP2()
                          {
                              CPF = billetRequest.CPF,
                              Account = billetRequest.Account,
                              Age = 0,
                              CodeBar = billet.CodeBar,
                              VlBillet = billetRequest.Value,
                              Email = null,
                              DtInsert = DateTime.Now
                          }
                      );
                }
                finally { }
            }

            return billet;
        }


        public BilletResponse GetBilletFISP2(BilletParameterP2 billetParameter, string carteira, bool salvarPDF)
        {
            try
            {
                salvarPDF = true;
                BilletResponse billet1 = null;
                billet1 = this.GetBilletP2(billetParameter.CPFSacado, billetParameter.ValorDocumento, billetParameter.DataVencimento, billetParameter.NumeroDocumento, "FISP2");

                if (billet1 != null)
                {
                    //if (billet1.PDF == null)
                    //billet1.PDF = GetPDFP2(billet1.IdBoleto, billetParameter.Instrucao1, billetParameter.Instrucao2, billetParameter.ComplementoInstrucao);

                    return billet1;
                }


                BilletResponse billet2 = null;
                //billet2 = this.GetBilletBradescoP2FIS(billetParameter, salvarPDF);
                return billet2;
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                string message = billetParameter.IdBoleto.ToString() + " | " + billetParameter.CodigoAcordo + " | " + billetParameter.Parcela + " | " + carteira + exception.StackTrace + Environment.NewLine + exception.Message;
                for (; exception.InnerException != null; exception = exception.InnerException)
                    message = message + exception.Message + Environment.NewLine + exception.StackTrace;

                throw new Exception(message);
            }
        }

        private BilletResponse GetBilletP2(string cpf, decimal valor, DateTime dtVencimento, string numeroDocumento, string carteira)
        {
            try
            {
                string documento = GetNossoNumeroP2(numeroDocumento, carteira);

                if (!numeroDocumento.StartsWith("000"))
                    numeroDocumento = "000" + numeroDocumento;

                var boleto = new BoletoDAO().GetBoleto(cpf, valor, dtVencimento, documento, carteira);
                if (boleto != null)
                {
                    return new BilletResponse()
                    {
                        //CodeBar = boleto.CodigoBarraBoleto,
                        CodeBar = boleto.LinhaDigitavelBoleto,
                        Number = boleto.NossoNumero,
                        PDF = boleto.PDF,
                        IdBillet = boleto.IdBoleto,
                        Registered = boleto.FlRegistrado
                    };
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetNossoNumeroP2(string nrAccount, string carteira)
        {
            if (!nrAccount.StartsWith("000"))
                nrAccount = "000" + nrAccount;
            string identificadorInterno = string.Empty;

            var identificadorBin = new IdentificadorBinDAO().GetByBinRange(nrAccount.Substring(3, 8));
            if (identificadorBin != null && identificadorBin.DigBin == 8)
            {
                identificadorInterno = identificadorBin.Identificador.Substring(identificadorBin.Identificador.Length - 3, 3);
                identificadorInterno = identificadorInterno + nrAccount.Trim().Substring(11, 7);
            }
            else
            {
                identificadorBin = new IdentificadorBinDAO().GetByBinRange(nrAccount.Substring(3, 6));
                if (identificadorBin.Identificador.StartsWith("8") || identificadorBin.Identificador.StartsWith("9"))
                {
                    identificadorInterno = identificadorBin.Identificador;
                    if (identificadorBin.DigBin == 6)
                        identificadorInterno = identificadorInterno + nrAccount.Trim().Substring(9, 6);
                    else
                        identificadorInterno = identificadorInterno + nrAccount.Trim().Substring(11, 7);
                }
                else
                {
                    identificadorInterno = identificadorBin.Identificador.Substring(identificadorBin.Identificador.Length - 3, 3);
                    if (identificadorBin.DigBin == 6)
                        identificadorInterno = identificadorInterno + nrAccount.Trim().Substring(9, 7);
                    else
                        identificadorInterno = identificadorInterno + nrAccount.Trim().Substring(11, 7);
                }
            }
            var maxNossoNumero = new BoletoDAO().GetByNumeroDocumentoP2(identificadorInterno, carteira);
            if (maxNossoNumero == null)
            {
                return (identificadorInterno + "1").PadRight(11, '1');
            }
            else
            {
                if (maxNossoNumero.IdentificadorInternoBoleto.EndsWith("9"))
                    return maxNossoNumero.IdentificadorInternoBoleto.Substring(0, maxNossoNumero.IdentificadorInternoBoleto.Length - 1) + "0";
                else
                    return (Convert.ToInt64(maxNossoNumero.IdentificadorInternoBoleto) + 1).ToString().PadLeft(11, '0');
            }
        }

        private Business.Models.Boleto.Endereco GetEndereco(string logradouro, string numero, string complemento, string bairro, string cidade, string UF, string cep)
        {
            EnderecoDAO enderecoBll = new EnderecoDAO();
            Business.Models.Boleto.Endereco endereco = enderecoBll.GetEndereco(logradouro, numero, complemento, bairro, cidade, UF, cep);
            if (endereco != null)
                return endereco;
            return enderecoBll.Add(new Business.Models.Boleto.Endereco()
            {
                Logradouro = logradouro,
                Numero = numero,
                Complemento = complemento,
                Bairro = bairro,
                Cidade = cidade,
                UF = UF,
                CEP = cep
            });
        }

        /*
        
        private BilletResponse GetBilletBradescoP2FIS(BilletParameterP2 billetParameter, bool salvarPDF)
        {
            try
            {
                IBanco _banco;

                string numeroDocumento = billetParameter.NumeroDocumento;
                long idAcordo = Convert.ToInt64(billetParameter.CodigoAcordo);
                int parcela = Convert.ToInt32(billetParameter.Parcela);
                DateTime dtVencimento = Convert.ToDateTime(billetParameter.DataVencimento);
                double valorBoleto = Convert.ToDouble(billetParameter.ValorDocumento);
                Convert.ToInt32(billetParameter.TotaldeParcelas);

                
                billetParameter.Agencia = "4150";
                billetParameter.ContaCorrente = "9999997";
                billetParameter.AgenciaBeneficiario = "4150";
                billetParameter.ContaBeneficiario = "9999997";
                billetParameter.ContaBeneficiarioDV = "7";
                billetParameter.CNPJBeneficiario = "04184779000101";
                billetParameter.Carteira = "FISP2";
                billetParameter.NomeBeneficiario = "BANCO BRADESCARD CNPJ: 004.184.779/0001-01 <br />ALAMEDA RIO NEGRO, 585 ALPHAVILLE BARUERI - SP CEP: 06454-000";
                billetParameter.Instrucao1 = "*** AO PAGADOR(A),  VALORES EXPRESSOS EM REAIS ***";
                billetParameter.Instrucao2 = "<br /><br />SR(A) CAIXA, NÃO RECEBER APÓS O VENCIMENTO ";
                billetParameter.ComplementoInstrucao += "<br /><br /><br />Assessoria  FIS - Fidelity National Serviços e Contact Center Ltda - CNPJ 19.581.571/0001-95, prestadora de serviço do Banco Bradesco";
                //Fim dados conta FMC Boleto Online

                var contaBancaria = new ContaBancaria
                {
                    Agencia = billetParameter.Agencia,
                    DigitoAgencia = "",
                    Conta = billetParameter.ContaBeneficiario,
                    DigitoConta = billetParameter.ContaBeneficiarioDV,
                    CarteiraPadrao = "16",
                    TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples,
                    TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro,
                    TipoImpressaoBoleto = TipoImpressaoBoleto.Empresa
                };

                _banco = BoletoNetCore.Banco.Instancia(Bancos.Bradesco);
                _banco.Beneficiario = new BoletoNetCore.Beneficiario
                {
                    CPFCNPJ = billetParameter.CNPJBeneficiario,
                    Nome = billetParameter.NomeBeneficiario,
                    Endereco = new BoletoNetCore.Endereco(),
                    ContaBancaria = contaBancaria,
                    MostrarCNPJnoBoleto = true
                };
                _banco.FormataBeneficiario();

                string nossoNumero = GetNossoNumeroP2(billetParameter.NumeroDocumento, "FISP2");
                string controleParticipante = Convert.ToInt64(billetParameter.NumeroDocumento).ToString().PadLeft(16, '0') + " " + billetParameter.CodEmpresa;

                var boleto = new BoletoNetCore.Boleto(_banco)
                {
                    DataVencimento = billetParameter.DataVencimento,
                    ValorTitulo = billetParameter.ValorDocumento,
                    NossoNumero = nossoNumero,
                    NumeroDocumento = nossoNumero.Substring(0, 10),//
                    EspecieDocumento = TipoEspecieDocumento.DM,
                    Pagador = new BoletoNetCore.Pagador
                    {
                        CPFCNPJ = billetParameter.CPFSacado,
                        Endereco = new BoletoNetCore.Endereco
                        {
                            CEP = billetParameter.CepSacado,
                            LogradouroEndereco = billetParameter.EnderecoSacado,
                            LogradouroNumero = billetParameter.NumeroSacado,
                            Bairro = billetParameter.BairroSacado,
                            Cidade = billetParameter.CidadeSacado,
                            UF = billetParameter.EstadoSacado
                        },
                        Nome = billetParameter.NomeSacado
                    },
                    MensagemInstrucoesCaixa = billetParameter.Instrucao1,
                    ComplementoInstrucao1 = billetParameter.Instrucao2,
                    ComplementoInstrucao2 = billetParameter.ComplementoInstrucao,
                };

                boleto.ValidarDados();

                //gerar pdf
                var boletoBancarioPdf = new BoletoNetCore.Pdf.BoletoImpressao.BoletoBancarioPdf();
                boletoBancarioPdf.Boleto = boleto;

                var pdf = boletoBancarioPdf.MontaBytesPDF();


                var idBoleto = this.AddNewBillet
                    (
                        "FISP2",
                        billetParameter.CNPJBeneficiario,
                        billetParameter.AgenciaBeneficiario,
                        billetParameter.ContaBeneficiario,
                        17,//cedente.IdCedente,
                        billetParameter.CPFSacado,
                        billetParameter.NomeSacado,
                        billetParameter.EnderecoSacado,
                        string.IsNullOrEmpty(billetParameter.NumeroSacado) ? "-" : billetParameter.NumeroSacado,
                        billetParameter.ComplementoSacado,
                        billetParameter.BairroSacado,
                        billetParameter.CidadeSacado,
                        billetParameter.EstadoSacado,
                        billetParameter.CepSacado,
                        boleto.Carteira,
                        boleto.Banco.Codigo,
                        boleto.NossoNumeroFormatado.Replace("016/", ""),
                        boleto.NumeroDocumento,
                        billetParameter.DataVencimento,
                        DateTime.Now,
                        DateTime.Now,
                        Convert.ToInt32(billetParameter.TotaldeParcelas),
                        billetParameter.ValorDocumento,
                        boleto.Aceite,
                        billetParameter.IdLead,
                        billetParameter.CodigoAcordo,
                        billetParameter.Parcela,
                        Convert.ToInt32(billetParameter.Parcela),
                        boleto.CodigoBarra.CodigoDeBarras,
                        boleto.CodigoBarra.LinhaDigitavel,
                        controleParticipante,
                        pdf,
                        false,
                        null,
                        salvarPDF
                    );

                return new BilletResponse()
                {
                    IdBillet = idBoleto,
                    //CodeBar = boleto.CodigoBarra.CodigoDeBarras,
                    CodeBar = boleto.CodigoBarra.LinhaDigitavel,
                    Number = boleto.NossoNumeroFormatado,
                    PDF = pdf,
                    Registered = false
                };

            }
            catch (Exception ex)
            {
                string erro = ex.Message + Environment.NewLine + ex.StackTrace;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
                }

                throw ex;
            }
        }

        */

        private long AddNewBillet(string carteira, string cpfCnpjBeneficiario, string agenciaBeneficiario, string contaBeneficiario, long idCedente, string cpfCnpjSacado, string nomeSacado, string enderecoSacado, string numeroSacado, string complementoSacado, string bairroSacado, string cidadeSacado, string ufSacado, string cepSacado, string codigoCarteira, int codigoBanco, string nossoNumero, string documento, DateTime dtVencimento, DateTime dtDocumento, DateTime dtProcessamento, int qtdParcelas, Decimal valor, string aceite, long idLead, string idAcordo, string idParcela, int nrParcela, string codigoBarras, string linhaDigitavel, string controleParticipante, byte[] pdf, bool registradoOnline, KeyValuePair<string, string>? registroOnline, bool salvarPDF)
        {
            try
            {
                if (string.IsNullOrEmpty(cepSacado))
                    cepSacado = "00000000";
                cepSacado = cepSacado.Replace("-", "").Replace(" ", "");
                Business.Models.Boleto.Boleto model = new Business.Models.Boleto.Boleto();
                model.RowGuidBoleto = Guid.NewGuid();
                model.IdLead = idLead;
                model.IdAcordo = !string.IsNullOrEmpty(idAcordo) ? new long?(Convert.ToInt64(idAcordo)) : new long?();
                model.IdParcela = !string.IsNullOrEmpty(idParcela) ? new long?(Convert.ToInt64(idParcela)) : new long?();
                string agencia;
                if (agenciaBeneficiario.Contains("-") && agenciaBeneficiario.Length == 5)
                    agencia = agenciaBeneficiario.Replace("-", "");
                else
                    agencia = ((IEnumerable<string>)agenciaBeneficiario.Split('-')).FirstOrDefault<string>();

                model.IdCedente = idCedente;
                model.IdSacado = this.AddSacado(cpfCnpjSacado, nomeSacado, enderecoSacado, numeroSacado, complementoSacado, bairroSacado, cidadeSacado, ufSacado, cepSacado).IdSacado;
                //FMC.Billet.DAO.Model.CarteiraCobranca byCarteira = new CarteiraCobrancaBLL().GetByCarteira(codigoCarteira);
                //model.IdCarteiraCobranca = byCarteira.IdCarteiraCobranca;
                //FMC.Billet.DAO.Model.Banco byCodigo = new BancoBLL().GetByCodigo(codigoBanco.ToString().Substring(0, 3));
                //model.IdBanco = byCodigo.IdBanco;
                model.Carteira = carteira;
                model.IdCarteiraCobranca = 5;
                model.IdBanco = 1;

                if (nossoNumero.Contains("-"))
                    model.DigitoNossoNumero = nossoNumero.Split('-')[1];
                else
                    model.DigitoNossoNumero = nossoNumero.Substring(nossoNumero.Length - 1, 1);
                model.IdentificadorInternoBoleto = nossoNumero.Replace("-", "").PadLeft(11, '0').Substring(0, 11);
                model.NossoNumero = nossoNumero;
                model.NumeroDocumento = documento;
                model.DigitoNumeroDocumento = "";
                model.DataVencimento = dtVencimento;
                model.DataDocumento = dtDocumento;
                model.DataProcessamento = dtProcessamento;
                model.QtdParcelas = qtdParcelas;
                model.NumeroParcela = nrParcela;
                model.ValorBoleto = valor;
                model.ValorCobrado = new Decimal?();
                model.LocalPagamento = "";
                model.QuantidadeMoeda = new Decimal?();
                model.ValorMoeda = new Decimal?();
                model.Aceite = aceite;
                model.IdEspecieDocumento = new int?();
                model.Moeda = "Reais";
                model.ValorAbatimento = new Decimal?();
                model.ValorDesconto = new Decimal?();
                model.ValorDescontoDia = new Decimal?();
                model.JurosPermanente = false;
                model.PercentualJurosMora = new Decimal?();
                model.JurosMora = new Decimal?();
                model.Iof = new Decimal?();
                model.PercentualMulta = new Decimal?();
                model.ValorMulta = new Decimal?();
                model.OutrosAcrescimos = new Decimal?();
                model.OutrosDescontos = new Decimal?();
                model.DataJurosMora = new DateTime?();
                model.DataMulta = new DateTime?();
                model.DataDesconto = new DateTime?();
                model.DataOutrosAcrescimos = new DateTime?();
                model.DataOutrosDescontos = new DateTime?();
                model.TipoModalidade = controleParticipante;
                model.CodigoBarraBoleto = codigoBarras;
                model.LinhaDigitavelBoleto = linhaDigitavel;
                model.TipoArquivo = 1;
                model.CodigoDoProduto = "1";
                model.QtdDias = new int?();
                model.IdCodigoOcorrenciaRemessa = new long?();
                model.IdCodigoOcorrenciaRetorno = new long?();

                if (salvarPDF)
                    model.PDF = pdf;
                if (registradoOnline)
                {
                    model.DataEnvioRegistro = new DateTime?(DateTime.Now);
                    model.FlRegistrado = true;
                    model.DataRegistro = new DateTime?(DateTime.Now);
                }
                else
                    model.FlRegistrado = false;
                //model.PDF = this.GetPDF(html, carteira != "UAM");
                Business.Models.Boleto.Boleto boleto = new BoletoDAO().Add(model);


                return boleto.IdBoleto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Sacado AddSacado(string cpfCnpjSacao, string nomeSacado, string enderecoSacado, string numeroSacado, string complementoSacado, string bairroSacado, string cidadeSacado, string ufSacado, string cepSacado)
        {
            try
            {
                SacadoDAO sacadoBll = new SacadoDAO();
                Sacado model = sacadoBll.GetByCPFCNPJ(cpfCnpjSacao);
                if (model == null)
                {
                    model = new Sacado();
                    model.Nome = nomeSacado;
                    model.CpfCnpj = cpfCnpjSacao;
                }
                if (!string.IsNullOrEmpty(enderecoSacado))
                {
                    if (ufSacado.Length > 2) ufSacado = ufSacado.Trim();

                    if (ufSacado.Length > 2) ufSacado = "SP";

                    Business.Models.Boleto.Endereco endereco = this.GetEndereco(enderecoSacado, numeroSacado, complementoSacado, bairroSacado, cidadeSacado, ufSacado, cepSacado);
                    if (model.SacadoEndereco.Where<SacadoEndereco>((Func<SacadoEndereco, bool>)(p => p.IdEndereco == endereco.IdEndereco)).Count<SacadoEndereco>() == 0)
                        model.SacadoEndereco.Add(new SacadoEndereco()
                        {
                            IdEndereco = endereco.IdEndereco,
                            DtCriacao = DateTime.Now
                        });
                }
                if (model.IdSacado == 0L)
                    return sacadoBll.Add(model);
                sacadoBll.Update(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}


