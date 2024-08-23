using Foolproof;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMC.WebSite.FIS.Models
{
    public class Curriculo
    {
        #region Informações Pessoais
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Sexo { get; set; }

        public string Observacoes { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Disponibilidade { get; set; }
        #endregion

        #region Documentos Pessoais
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Cpf { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Ctps { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Serie { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Pis { get; set; }
        #endregion

        #region Informações Educacionais
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string NivelAcademico { get; set; }

        public string NomeCurso { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? AnoConclusao { get; set; }

        public string HorarioCurso { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string ConhecimentoInformatica { get; set; }
        #endregion

        #region Dados de Endereço
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Cep { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Endereco { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Numero { get; set; }
        
        public string Complemento { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Estado { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Cidade { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Bairro { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string TelefoneResidencial { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string TelefoneCelular { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string TelefoneRecado { get; set; }
        #endregion

        #region Experiência Profissional
        public bool PrimeiroEmprego { get; set; }

        [RequiredIfFalse("PrimeiroEmprego", ErrorMessage = " ")]
        public string Empresa1 { get; set; }

        [RequiredIfFalse("PrimeiroEmprego", ErrorMessage = " ")]
        public string Funcao1 { get; set; }

        [RequiredIfFalse("PrimeiroEmprego", ErrorMessage = " ")]
        public DateTime? DataInicio1 { get; set; }

        [RequiredIfFalse("PrimeiroEmprego", ErrorMessage = " ")]
        public DateTime? DataDesligamento1 { get; set; }

        [RequiredIfFalse("PrimeiroEmprego", ErrorMessage = " ")]
        public string Resumo1 { get; set; }

        public string Empresa2 { get; set; }        
        public string Funcao2 { get; set; }
        public DateTime? DataInicio2 { get; set; }
        public DateTime? DataDesligamento2 { get; set; }
        public string Resumo2 { get; set; }
        #endregion

        #region Ficou Sabendo
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string FicouSabendo { get; set; }
        #endregion

        #region Unidade
        public string Unidade { get; set; }
        #endregion
    }
}
