namespace FMC.FIS.API.Models
{
    public class RetornoRequest
    {
        public string NomeArquivo { get; set; }
        public byte[] Arquivo { get; set; }
        public int IdUserLogin { get; set; }
    }
}
