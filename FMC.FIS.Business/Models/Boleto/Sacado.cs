namespace FMC.FIS.Business.Models.Boleto
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Sacado")]
    public class Sacado
    {
        public Sacado()
        {
            this.Boleto = new HashSet<Boleto>();
            this.SacadoEndereco = new HashSet<SacadoEndereco>();
        }


        [Column("CpfCnpj", TypeName = "varchar")]
        public string CpfCnpj { get; set; }

        [Column("CpfCnpjAvalista", TypeName = "varchar")]
        public string CpfCnpjAvalista { get; set; }

        [Key, Column("IdSacado", TypeName = "bigint")]
        public long IdSacado { get; set; }

        [Column("Nome", TypeName = "varchar")]
        public string Nome { get; set; }

        [Column("NomeAvalista", TypeName = "varchar")]
        public string NomeAvalista { get; set; }

        public virtual ICollection<Boleto> Boleto { get; set; }
        public virtual ICollection<SacadoEndereco> SacadoEndereco { get; set; }


    }
    public class SacadoEndereco
    {
        [Column("DtCriacao", TypeName = "datetime")]
        public DateTime DtCriacao { get; set; }

        [ForeignKey("IdEndereco")]
        public virtual Endereco Endereco { get; set; }

        [Column("IdEndereco", TypeName = "bigint")]
        public long IdEndereco { get; set; }

        [Column("IdSacado", TypeName = "bigint")]
        public long IdSacado { get; set; }

        [Key, Column("IdSacadoEndereco", TypeName = "bigint")]
        public long IdSacadoEndereco { get; set; }

        [ForeignKey("IdSacado")]
        public virtual Sacado Sacado { get; set; }
    }

    [Table("Endereco")]
    public class Endereco
    {
        public Endereco()
        {
            this.SacadoEndereco = new HashSet<SacadoEndereco>();
        }

        [Column("Bairro", TypeName = "varchar")]
        public string Bairro { get; set; }


        [Column("CEP", TypeName = "varchar")]
        public string CEP { get; set; }

        [Column("Cidade", TypeName = "varchar")]
        public string Cidade { get; set; }

        [Column("Complemento", TypeName = "varchar")]
        public string Complemento { get; set; }

        [Key, Column("IdEndereco", TypeName = "bigint")]
        public long IdEndereco { get; set; }

        [Column("Logradouro", TypeName = "varchar")]
        public string Logradouro { get; set; }

        [Column("Numero", TypeName = "varchar")]
        public string Numero { get; set; }

        public virtual ICollection<SacadoEndereco> SacadoEndereco { get; set; }

        [Column("UF", TypeName = "varchar")]
        public string UF { get; set; }
    }
}
