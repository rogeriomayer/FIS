using System;
using System.Collections.Generic;

public class PersonResponse
{
    /// <summary>
    /// Codigo da pessoa
    /// </summary>
    public long IdPerson { get; set; }
    /// <summary>
    /// Nome do cliente
    /// </summary>
    /// 
    public string Name { get; set; }
    /// <summary>
    /// Data de nascimento
    /// </summary>
    public DateTime DtBirth { get; set; }

    /// <summary>
    /// CPF
    /// </summary>
    public string CPF { get; set; }

    /// <summary>
    /// RG
    /// </summary>
    public string RG { get; set; }

    /// <summary>
    /// Nome da mãe
    /// </summary>
    public string MotherName { get; set; }

    /// <summary>
    /// Email do cliente
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Cartões/Contas do cliente
    /// </summary>
    public virtual ICollection<CardResponse> Cards { get; set; }
    public virtual AddressResponse Address { get; set; }
    public virtual ICollection<PhoneResponse> Phones { get; set; }
}

public class CardResponse
{
    public CardResponse()
    {
        StatusLeadResponse = new HashSet<StatusLeadResponse>();
    }

    /// <summary>
    /// Código do produto/cartão
    /// </summary>
    public long IdProduct { get; set; }

    /// <summary>
    /// Id da carteira
    /// </summary>
    public byte IdProductType { get; set; }

    /// <summary>
    /// Cartão disponivel para cobrança
    /// </summary>
    public bool AvailableBilling { get; set; }

    /// <summary>
    /// Numero da Conta
    /// </summary>
    public string Account { get; set; }
    /// <summary>
    /// Nome do Cartão
    /// </summary>
    public string CardName { get; set; }
    /// <summary>
    /// Numero do cartão
    /// </summary>
    public string CardNumber { get; set; }
    /// <summary>
    /// Dias em atraso
    /// </summary>
    public int Age { get; set; }
    /// <summary>
    /// Saldo Atualizado
    /// </summary>
    public decimal VlFull { get; set; }
    /// <summary>
    /// Saldo em atraso
    /// </summary>
    public decimal VlDue { get; set; }
    /// <summary>
    /// Pagemento minimo
    /// </summary>
    public decimal VlMinimum { get; set; }
    /// <summary>
    /// Data do atraso
    /// </summary>
    public DateTime? DtDue { get; set; }

    /// <summary>
    /// Código da ultima Lead
    /// </summary>
    public long IdLead { get; set; }

    /// <summary>
    /// Data de importação da ultima Lead
    /// </summary>
    public DateTime? DtLead { get; set; }

    /// <summary>
    /// Ocorrência nesse cartão/lead
    /// </summary>
    public ICollection<StatusLeadResponse> StatusLeadResponse { get; set; }
}

public class AddressResponse
{
    /// <summary>
    /// Id do endereço
    /// </summary>
    public long IdAddress { get; set; }
    /// <summary>
    /// CEP
    /// </summary>
    public string Cep { get; set; }

    /// <summary>
    /// Logradouro
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Numero
    /// </summary>
    public string NrAddress { get; set; }

    /// <summary>
    /// Bairro
    /// </summary>
    public string District { get; set; }

    /// <summary>
    /// Complemento
    /// </summary>
    public string Complement { get; set; }

    /// <summary>
    /// Cidade
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Estado
    /// </summary>
    public string UF { get; set; }

    /// <summary>
    /// Data de atualização
    /// </summary>
    public System.DateTime DtAddress { get; set; }
}

public class PhoneResponse
{
    /// <summary>
    /// Id do telefone
    /// </summary>
    public long IdPhone { get; set; }

    /// <summary>
    /// Id do tipo de telefone
    /// </summary>
    public int IdPhoneType { get; set; }

    /// <summary>
    /// Status do telefone
    /// </summary>
    public int IdPhoneStatus { get; set; }

    /// <summary>
    /// Tipo do telefone
    /// </summary>
    public string PhoneType { get; set; }

    /// <summary>
    /// Numero do telefone
    /// </summary>
    public string NrPhone { get; set; }

    /// <summary>
    /// Data de atualização
    /// </summary>
    public DateTime DtPhone { get; set; }

    /// <summary>
    /// Está em black list
    /// </summary>
    public bool Blacklist { get; set; }
}

public class StatusLeadResponse
{
    public long IdStatusLead { get; set; }
    public int IdStatus { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public DateTime DtStatusLead { get; set; }
    public bool FlEfective { get; set; }

    public int IdUserLogin { get; set; }
    public string UserLogin { get; set; }
    public string DsProduct { get; set; }

    public PromisseResponse PromisseResponse { get; set; }

    public AgreementResponse AgreementResponse { get; set; }

}

public class PromisseResponse
{
    public long IdPromisse { get; set; }
    public long IdStatusLead { get; set; }
    public decimal VlPromisse { get; set; }
    public DateTime DtPromisse { get; set; }
    public DateTime DtInsert { get; set; }
}

public class AgreementResponse
{
    public long IdAgreement { get; set; }

    public long IdStatusLead { get; set; }

    public decimal VlEntrace { get; set; }

    public DateTime DtEntrace { get; set; }

    public decimal PcDiscount { get; set; }

    public decimal VlInterest { get; set; }

    public int QtParcel { get; set; }

    public decimal VlParcel { get; set; }

    public decimal VlAgreement { get; set; }

    public string CdPaymentOption { get; set; }

    public string CdParcelPlan { get; set; }

    public string CdAgreementRecupera { get; set; }

    public DateTime DtInsert { get; set; }

    public string Status { get; set; }


    public ICollection<AgreementParcelResponse> AgreementParcelResponse { get; set; }
}

public class AgreementParcelResponse
{
    public long IdAgreementParcel { get; set; }
    public int NrParcel { get; set; }
    public DateTime DtParcel { get; set; }
    public decimal VlParcel { get; set; }
    public string Status { get; set; }
}

