using FMC.FIS.API.Models.Boleto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FMC.FIS.API.Code.Business.BLL
{
    public class RemessaBLL
    {
        public ICollection<RemessasResponse> GetRemessas(string dtIni, string dtFim)
        {
            string cedentes = "17,18";

            var url = "http://10.40.0.109/ibi/ura.svc";
            //var url = "http://localhost:34072/URA.svc";
            var remessas = RestApi.Get<ICollection<Remessa>>(url, "GetRemessas/" + cedentes + "/" + dtIni + "/" + dtFim, null);

            IList<RemessasResponse> remessasResponse = new List<RemessasResponse>();

            remessas.ToList().ForEach(
                    p => remessasResponse.Add
                        (
                            new RemessasResponse()
                            {
                                TipoProduto = p.Carteira.ToUpper().Contains("P1") ? "P1" : "P2",
                                DtGeracaoRemessa = p.DtGeracaoRemessa,
                                DtRetornoRemessa = p.DtRetornoRemessa,
                                IdRemessa = p.IdRemessa,
                                StatusRemessa = GetStatusRemessa(p.IdRemessa),
                                NomeArquivo = p.NomeArquivo,
                                NrRemessa = p.NrRemessa
                            }
                        )
                );

            return remessasResponse;

        }

        private string GetStatusRemessa(long idRemessa)
        {
            var downloads = new DownloadCNABBLL().GetByIdRemessa(idRemessa);
            if (downloads.Count() > 0)
            {
                return "Baixado";
            }
            else
            {
                return "Não Baixado";
            }
        }

        public RemessaResponse GetRemessa(long idRemessa)
        {
            var url = "http://10.40.0.109/ibi/ura.svc";
            //var url = "http://localhost:34072/URA.svc";
            var remessa = RestApi.Get<Remessa>(url, "GetRemessa/" + idRemessa, null);

            return new RemessaResponse()
            {
                IdRemessa = remessa.IdRemessa,
                NomeArquivo = remessa.NomeArquivo,
                File = Util.ReadFile(remessa.CaminhoArquivo + @"\" + remessa.NomeArquivo).ToArray()
            };
        }

        public bool SetRetorno(string nomeArquivo, byte[] arquivo, int idUserLogin)
        {
            string filePath = @"\\10.40.0.10\estrategia\BRADESCARD\REGISTRO_BOLETO\RETORNO\PENDENTES";
            string fileDuplicados = @"\\10.40.0.10\estrategia\BRADESCARD\REGISTRO_BOLETO\RETORNO\PENDENTES\DUPLICADOS";
            string file = filePath + @"\FIS_" + nomeArquivo;
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            if (!Directory.Exists(filePath + @"\DUPLICADOS"))
                Directory.CreateDirectory(fileDuplicados);

            if (File.Exists(file))
                File.Move(file, fileDuplicados + @"\" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + "_" + nomeArquivo);

            File.WriteAllBytes(file, arquivo);

            new UploadCNABBLL().Add
                (
                    new Models.UploadCNAB()
                    {
                        DsName = nomeArquivo,
                        IdUserLogin = idUserLogin,
                        DtUpload = DateTime.Now
                    }
                );

            return true;
        }
    }
}
