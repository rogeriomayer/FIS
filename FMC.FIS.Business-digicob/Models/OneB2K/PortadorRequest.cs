using System;

namespace FMC.FIS.Business.Models.OneB2K
{
    public class PortadorRequest
    {
        public string nrCPF4 { get; set; }
        public string nrCPF3 { get; set; }
        public FoneComercial foneComercial { get; set; }
        public string nrCPF2 { get; set; }
        public FoneResidencial foneResidencial { get; set; }
        public string nrCPF1 { get; set; }
        public string idEstadoCivil1 { get; set; }
        public string percLimiteParc { get; set; }
        public Celular celular { get; set; }
        public EnderecoFatura enderecoFatura { get; set; }
        public DateTime dtNascimento2 { get; set; }
        public string idSexo4 { get; set; }
        public Endereco endereco { get; set; }
        public DateTime dtNascimento1 { get; set; }
        public DateTime dtNascimento4 { get; set; }
        public DateTime dtNascimento3 { get; set; }
        public string idEstadoCivil4 { get; set; }
        public string idEstadoCivil2 { get; set; }
        public string idEstadoCivil3 { get; set; }
        public string nome3 { get; set; }
        public string idSexo1 { get; set; }
        public string nome4 { get; set; }
        public string nome1 { get; set; }
        public string idSexo2 { get; set; }
        public string documentoRg { get; set; }
        public string nome2 { get; set; }
        public string idSexo3 { get; set; }
    }

    public class Celular
    {
        public string numero { get; set; }
        public string ddd { get; set; }
    }

    public class Endereco
    {
        public string uf { get; set; }
        public string nrEndereco { get; set; }
        public string cidade { get; set; }
        public string tipo { get; set; }
        public string complemento { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
    }

    public class EnderecoFatura
    {
        public string uf { get; set; }
        public string cidade { get; set; }
        public string complemento { get; set; }
        public string numero { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
    }

    public class FoneComercial
    {
        public string numero { get; set; }
        public string ddd { get; set; }
        public string ramal { get; set; }
    }

    public class FoneResidencial
    {
        public string numero { get; set; }
        public string ddd { get; set; }
        public string ramal { get; set; }
    }


}
