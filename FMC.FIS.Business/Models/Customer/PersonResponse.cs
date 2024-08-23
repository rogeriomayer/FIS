using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace FMC.FIS.Business.Models.Customer
{
    /// <summary>
    /// Dados do cliente
    /// </summary>
    [SwaggerSchema(Description = "Dados do cliente")]
    public class PersonResponse
    {
        public PersonResponse()
        {
            Cards = new HashSet<CardResponse>();
            Email = new HashSet<string>();
            Phones = new HashSet<PhoneResponse>();
        }
        /// <summary>
        /// Id do cliente
        /// </summary>
        [SwaggerSchema(Description = "Id do cliente")]
        public long IdPerson { get; set; }
        /// <summary>
        /// Nome do cliente
        /// </summary>
        [SwaggerSchema(Description = "Nome do cliente")]
        public string Name { get; set; }
        /// <summary>
        /// Data de nascimento
        /// </summary>
        [SwaggerSchema(Description = "Data de nascimento")]
        public DateTime DtBirth { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [SwaggerSchema(Description = "CPF do cliente")]
        public string CPF { get; set; }

        /// <summary>
        /// RG
        /// </summary>
        [SwaggerSchema(Description = "RG do cliente")]
        public string RG { get; set; }

        /// <summary>
        /// Nome da mãe do cliente
        /// </summary>
        [SwaggerSchema(Description = "Nome da mãe do cliente")]
        public string MotherName { get; set; }

        /// <summary>
        /// E-mails do cliente
        /// </summary>
        [SwaggerSchema(Description = "E-mails do cliente")]
        public ICollection<string> Email { get; set; }

        /// <summary>
        /// Cartões/Contas do cliente
        /// </summary>
        [SwaggerSchema(Description = "Cartões/Contas do cliente")]
        public virtual ICollection<CardResponse> Cards { get; set; }

        /// <summary>
        /// Endereços do cliente
        /// </summary>
        [SwaggerSchema(Description = "Endereços do cliente")]
        public virtual AddressResponse Address { get; set; }

        /// <summary>
        /// Telefones do cliente
        /// </summary>
        [SwaggerSchema(Description = "Telefones do cliente")]
        public virtual ICollection<PhoneResponse> Phones { get; set; }
    }

    /// <summary>
    /// Dados dos cartão/conta/contrato do cliente
    /// </summary>
    [SwaggerSchema(Description = "Dados dos cartão/conta/contrato do cliente")]
    public class CardResponse
    {
        public CardResponse()
        {
            StatusLeadResponse = new HashSet<StatusLeadResponse>();
            //ComplementData = new HashSet<ComplementData>();
            ParcelaCredz = new HashSet<ParcelaCredz>();
        }

        /// <summary>
        /// True: Conta disponivel para cobrança | False: Conta não disponível para cobrança
        /// </summary>
        [SwaggerSchema(Description = "True: Conta disponivel para cobrança | False: Conta não disponível para cobrança")]
        public bool AvailableBilling { get; set; }

        /// <summary>
        /// Id do produto/cartão/conta/contrato
        /// </summary>
        [SwaggerSchema(Description = "Id do produto/cartão/conta/contrato")]
        public long IdProduct { get; set; }

        /// <summary>
        /// Id da carteira/parceiro :  FIS = 1, AFINZ = 2,  Credz = 3
        /// </summary>
        [SwaggerSchema(Description = "Id da carteira/parceiro :  FIS = 1, AFINZ = 2,  Credz = 3")]
        public byte IdProductType { get; set; }

        /// <summary>
        /// Numero da Conta/Contrato
        /// </summary>
        [SwaggerSchema(Description = "Numero da Conta/Contrato")]
        public string Account { get; set; }
        /// <summary>
        /// Nome do Cartão/Contrato
        /// </summary>
        [SwaggerSchema(Description = "Nome do Cartão/Contrato")]
        public string CardName { get; set; }

        /// <summary>
        /// URL Imagem do cartão/contrato
        /// </summary>
        [SwaggerSchema(Description = "URL da imagem do Cartão/Contrato")]
        public string CardImage { get; set; }

        /// <summary>
        /// Numero do cartão mascarado
        /// </summary>
        [SwaggerSchema(Description = "Numero do cartão mascarado")]
        public string CardNumber { get; set; }

        /// <summary>
        /// Numero da conta no sistema do cliente/parceiro
        /// </summary>
        [SwaggerSchema(Description = "Numero da conta no sistema do cliente/parceiro")]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Dias em atraso
        /// </summary>
        [SwaggerSchema(Description = "Dias em atraso")]
        public int Age { get; set; }
        /// <summary>
        /// Saldo Atualizado
        /// </summary>
        [SwaggerSchema(Description = "Saldo Atualizado")]
        public decimal VlFull { get; set; }
        /// <summary>
        /// Saldo em atraso
        /// </summary>
        [SwaggerSchema(Description = "Saldo em atraso")]
        public decimal VlDue { get; set; }
        /// <summary>
        /// Valor pagamento minimo
        /// </summary>
        [SwaggerSchema(Description = "Valor pagamento minimo")]
        public decimal VlMinimum { get; set; }
        /// <summary>
        /// Data do atraso
        /// </summary>
        [SwaggerSchema(Description = "Data do atraso")]
        public DateTime? DtDue { get; set; }

        /// <summary>
        /// Id da ultima Lead
        /// </summary>
        [SwaggerSchema(Description = "Id da ultima Lead")]
        public long IdLead { get; set; }

        /// <summary>
        /// Data de importação da ultima Lead
        /// </summary>
        [SwaggerSchema(Description = "Data de importação da ultima Lead")]
        public DateTime? DtLead { get; set; }

        /// <summary>
        /// Dados genericos complementares
        /// </summary>
        [SwaggerSchema(Description = "Dados genericos complementares")]
        //public ICollection<ComplementData> ComplementData { get; set; }
        public ICollection<ParcelaCredz> ParcelaCredz { get; set; }


        /// <summary>
        /// Ocorrências da conta/lead
        /// </summary>
        [SwaggerSchema(Description = "Ocorrências da conta/lead")]
        public ICollection<StatusLeadResponse> StatusLeadResponse { get; set; }


    }

    /*
    /// <summary>
    /// Dados complementares
    /// </summary>
    [SwaggerSchema(Description = "Dados complementares")]
    public class ComplementData
    {
        /// <summary>
        /// Propriedade
        /// </summary>
        [SwaggerSchema(Description = "Propriedade")] 
        public string Name { get; set; }

        /// <summary>
        /// Valor
        /// </summary>
        [SwaggerSchema(Description = "Valor da propriedade")] 
        public string Value { get; set; }
    }
    */

    public class ParcelaCredz
    {
        public long id_parcela_original { get; set; }
        public long negociacao_id { get; set; }
        public string numero_parcela_original { get; set; }
        public DateTime vencimento { get; set; }
        public decimal valor { get; set; }
    }

    /// <summary>
    /// Endereço
    /// </summary>
    [SwaggerSchema(Description = "Endereço")]
    public class AddressResponse
    {
        /// <summary>
        /// Id do endereço
        /// </summary>
        [SwaggerSchema(Description = "ID do Endereço")]
        public long IdAddress { get; set; }
        /// <summary>
        /// CEP
        /// </summary>
        [SwaggerSchema(Description = "CEP")]
        public string Cep { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        [SwaggerSchema(Description = "Logradouro")]
        public string Address { get; set; }

        /// <summary>
        /// Numero do logradouro
        /// </summary>
        [SwaggerSchema(Description = "Numero do logradouro")]
        public string NrAddress { get; set; }

        /// <summary>
        /// Bairro
        /// </summary>
        [SwaggerSchema(Description = "Bairro")]
        public string District { get; set; }

        /// <summary>
        /// Complemento do endereço
        /// </summary>
        [SwaggerSchema(Description = "Complemento do endereço")]
        public string Complement { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        [SwaggerSchema(Description = "Cidade")]
        public string City { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [SwaggerSchema(Description = "UF")]
        public string UF { get; set; }

        /// <summary>
        /// Data de atualização
        /// </summary>
        [SwaggerSchema(Description = "Data de atualização do endereço")]
        public System.DateTime DtAddress { get; set; }
    }

    /// <summary>
    /// Dados Telefone
    /// </summary>
    [SwaggerSchema(Description = "Dados Telefone")]
    public class PhoneResponse
    {
        /// <summary>
        /// Id do telefone
        /// </summary>
        [SwaggerSchema(Description = "Id do telefone")]
        public long IdPhone { get; set; }

        /// <summary>
        /// Id do tipo de telefone
        /// </summary>
        [SwaggerSchema(Description = "Id do tipo de telefone : 1:Residencial | 2:Comercial | 3:Celular | 4:Outros | 5:Referência")]
        public int IdPhoneType { get; set; }

        /// <summary>
        /// Status do telefone
        /// </summary>
        [SwaggerSchema(Description = "Id do Status do telefone : 1:Hot | 2:Localizador | 3:Ativo | 4:Inativo")]
        public int IdPhoneStatus { get; set; }

        /// <summary>
        /// Tipo do telefone
        /// </summary>
        [SwaggerSchema(Description = "Tipo do telefone : 1:Residencial | 2:Comercial | 3:Celular | 4:Outros | 5:Referência")]
        public string PhoneType { get; set; }

        /// <summary>
        /// Numero do telefone
        /// </summary>
        [SwaggerSchema(Description = "Numero do telefone")]
        public string NrPhone { get; set; }

        /// <summary>
        /// Data de atualização
        /// </summary>
        [SwaggerSchema(Description = "Data de atualização")]
        public DateTime DtPhone { get; set; }

        /// <summary>
        /// Está em black list
        /// </summary>
        [SwaggerSchema(Description = "True: Black list")]
        public bool Blacklist { get; set; }
    }

    /// <summary>
    /// Ocorrência da Lead/Conta
    /// </summary>
    [SwaggerSchema(Description = "Ocorrências da Lead/Conta")]
    public class StatusLeadResponse
    {
        /// <summary>
        /// Id da ocorrência
        /// </summary>
        [SwaggerSchema(Description = "Id da ocorrência")]
        public long IdStatusLead { get; set; }

        /// <summary>
        /// Id status ocorrência
        /// </summary>
        [SwaggerSchema(Description = "Id status ocorrência")]
        public int IdStatus { get; set; }

        /// <summary>
        /// Descrição status ocorrência
        /// </summary>
        [SwaggerSchema(Description = "Descrição status ocorrência")]
        public string Status { get; set; }

        /// <summary>
        /// Descrição da ocorrência
        /// </summary>
        [SwaggerSchema(Description = "Descrição da ocorrência")]
        public string Description { get; set; }

        /// <summary>
        /// Data da ocorrência
        /// </summary>
        [SwaggerSchema(Description = "Data da ocorrência")]
        public DateTime DtStatusLead { get; set; }

        /// <summary>
        /// Flag contato efetivo
        /// </summary>
        [SwaggerSchema(Description = "Flag contato efetivo")]
        public bool FlEfective { get; set; }

        /// <summary>
        /// Id usuário da ocorrencia
        /// </summary>
        [SwaggerSchema(Description = "Id usuário da ocorrencia")]
        public int IdUserLogin { get; set; }

        /// <summary>
        /// Usuário da ocorrencia
        /// </summary>
        [SwaggerSchema(Description = "Usuário da ocorrencia")]
        public string UserLogin { get; set; }

        /// <summary>
        /// Numero conta/contrato
        /// </summary>
        [SwaggerSchema(Description = "Numero conta/contrato")]
        public string DsProduct { get; set; }

        /// <summary>
        /// Caso ocorrencia seja promessa de pagamento será preenchida
        /// </summary>
        [SwaggerSchema(Description = "Caso ocorrencia seja promessa de pagamento será preenchida")]
        public PromisseResponse PromisseResponse { get; set; }

        /// <summary>
        /// Caso ocorrencia seja acordo será preenchido
        /// </summary>
        [SwaggerSchema(Description = "Caso ocorrencia seja acordo será preenchido")]
        public AgreementResponse AgreementResponse { get; set; }

    }

    /// <summary>
    /// Dados promessa de pagamento
    /// </summary>
    [SwaggerSchema(Description = "Dados promessa de pagamento")]
    public class PromisseResponse
    {

        /// <summary>
        /// Id da promessa de pagamento
        /// </summary>
        [SwaggerSchema(Description = "Id da promessa de pagamento")]
        public long IdPromisse { get; set; }

        /// <summary>
        /// Id da ocorrência
        /// </summary>
        [SwaggerSchema(Description = "Id da ocorrência")]
        public long IdStatusLead { get; set; }

        /// <summary>
        /// Valor da promessa
        /// </summary>
        [SwaggerSchema(Description = "Valor da promessa")]
        public decimal VlPromisse { get; set; }

        /// <summary>
        /// Data pagamento da promessa
        /// </summary>
        [SwaggerSchema(Description = "Data pagamento da promessa")]
        public DateTime DtPromisse { get; set; }

        /// <summary>
        /// Data criação da promessa
        /// </summary>
        [SwaggerSchema(Description = "Data criação da promessa")]
        public DateTime DtInsert { get; set; }
    }


    /// <summary>
    /// Dados do acordo
    /// </summary>
    [SwaggerSchema(Description = "Dados do acordo")]
    public class AgreementResponse
    {
        public AgreementResponse()
        {
            AgreementParcelResponse = new HashSet<AgreementParcelResponse>();
        }

        /// <summary>
        /// Id do acordo
        /// </summary>
        [SwaggerSchema(Description = "Id do acordo")]
        public long IdAgreement { get; set; }

        /// <summary>
        /// Id da ocorrência
        /// </summary>
        [SwaggerSchema(Description = "Id da ocorrência")]
        public long IdStatusLead { get; set; }

        /// <summary>
        /// Valor da entrada
        /// </summary>
        [SwaggerSchema(Description = "Valor da entrada")]
        public decimal VlEntrace { get; set; }

        /// <summary>
        /// Data da entrada
        /// </summary>
        [SwaggerSchema(Description = "Data da entrada")]
        public DateTime DtEntrace { get; set; }

        /// <summary>
        /// Valor de desconto
        /// </summary>
        [SwaggerSchema(Description = "Valor de desconto")]
        public decimal PcDiscount { get; set; }


        /// <summary>
        /// Quantidade de parcelas do acordo
        /// </summary>
        [SwaggerSchema(Description = "Quantidade de parcelas do acordo")]
        public int QtParcel { get; set; }

        /// <summary>
        /// Valor das parcelas do acordo
        /// </summary>
        [SwaggerSchema(Description = "Valor das parcelas do acordo")]
        public decimal VlParcel { get; set; }

        /// <summary>
        /// Valor total do acordo
        /// </summary>
        [SwaggerSchema(Description = "Valor total do acordo")]
        public decimal VlAgreement { get; set; }

        /// <summary>
        /// Código do acordo no sistema do parceiro
        /// </summary>
        [SwaggerSchema(Description = "Código do acordo no sistema do parceiro")]
        public string CdPaymentOption { get; set; }

        /// <summary>
        /// Caso preenchido significa que o acordo foi formalizado pelo parceiro
        /// </summary>
        [SwaggerSchema(Description = "Caso preenchido significa que o acordo foi formalizado pelo parceiro")]
        public string CdParcelPlan { get; set; }

        /// <summary>
        /// Código do acordo no sistema do parceiro
        /// </summary>
        [SwaggerSchema(Description = "Código do acordo no sistema do parceiro")]
        public string CdAgreement { get; set; }

        /// <summary>
        /// Data de criação do acordo
        /// </summary>
        [SwaggerSchema(Description = "Data de criação do acordo")]
        public DateTime DtInsert { get; set; }


        /// <summary>
        /// Status do acordo Em Aberto | Quebrado | Aguardando Aprovação | Reprovado | Pago | Em Andamento | Reprovado | Aguardando Contra Proposta 
        /// </summary>
        [SwaggerSchema(Description = "Status do acordo Em Aberto | Quebrado | Aguardando Aprovação | Reprovado | Pago | Em Andamento | Reprovado | Aguardando Contra Proposta ")]
        public string Status { get; set; }

        /// <summary>
        /// Id Status do acordo 1:Em Aberto | 2:Quebrado | 3:Aguardando Aprovação | 4:Reprovado | 5:Pago | 6:Em Andamento | 7:Reprovado | 8:Aguardando Contra Proposta
        /// </summary>
        [SwaggerSchema(Description = "Id Status do acordo 1:Em Aberto | 2:Quebrado | 3:Aguardando Aprovação | 4:Reprovado | 5:Pago | 6:Em Andamento | 7:Reprovado | 8:Aguardando Contra Proposta")]
        public long IdAgreementStatus { get; set; }

        /// <summary>
        /// Parcelas do acordo
        /// </summary>
        [SwaggerSchema(Description = "Parcelas do acordo")]
        public ICollection<AgreementParcelResponse> AgreementParcelResponse { get; set; }
    }


    /// <summary>
    /// Dados da parcela do acordo
    /// </summary>
    [SwaggerSchema(Description = "Dados da parcela do acordo")]
    public class AgreementParcelResponse
    {
        public AgreementParcelResponse()
        {
            BilletResponse = new HashSet<BilletResponse>();
        }

        /// <summary>
        /// Id da parcela do acordo
        /// </summary>
        [SwaggerSchema(Description = "Id da parcela")]
        public long IdAgreementParcel { get; set; }

        /// <summary>
        /// Numero da parcela do acordo
        /// </summary>
        [SwaggerSchema(Description = "Numero da parcela ")]
        public int NrParcel { get; set; }

        /// <summary>
        /// Data de vencimento 
        /// </summary>
        [SwaggerSchema(Description = "Data de vencimento")]
        public DateTime DtParcel { get; set; }

        /// <summary>
        /// Valor da parcela
        /// </summary>
        [SwaggerSchema(Description = "Valor da parcela")]
        public decimal VlParcel { get; set; }

        /// <summary>
        /// Status da parcela
        /// </summary>
        [SwaggerSchema(Description = "Status da parcela")]
        public string Status { get; set; }

        /// <summary>
        /// Boleto da parcela
        /// </summary>
        [SwaggerSchema(Description = "Boleto da parcela")]
        public ICollection<BilletResponse> BilletResponse { get; set; }
    }

    public class CardOneB2k
    {
        public string NrCard { get; set; }
        public string Account { get; set; }
        public decimal VlFull { get; set; }
        public decimal VlMinimum { get; set; }
        public int Age { get; set; }
    }
}



