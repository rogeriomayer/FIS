using System;
using System.Collections.Generic;

public class AcordoCredz
{
    public long id { get; set; }
    public string status_descricao { get; set; }
    public int status_id { get; set; }
    public string numero { get; set; }
    public DateTime data { get; set; }
    public int quantidade_parcelas { get; set; }
    public decimal principal { get; set; }
    public decimal multa { get; set; }
    public decimal juros { get; set; }
    public decimal honorarios { get; set; }
    public decimal despesas { get; set; }
    public decimal taxa_parcelamento { get; set; }
    public decimal desconto_principal { get; set; }
    public decimal desconto_multa { get; set; }
    public decimal desconto_juros { get; set; }
    public decimal desconto_honorario { get; set; }
    public decimal total { get; set; }
    public decimal repasse_principal { get; set; }
    public decimal repasse_multa { get; set; }
    public decimal repasse_juros { get; set; }
    public decimal repasse_despesas { get; set; }
    public decimal repasse_taxa_parcelamento { get; set; }
    public decimal total_repasse { get; set; }
    public string operador_vinculado { get; set; }
    public List<ParcelasOriginalCredz> parcelas_originais { get; set; }
    public List<ParcelasNovaCredz> parcelas_novas { get; set; }
    public List<BoletoSimplesCredz> boletos { get; set; }
}
public class BoletoSimplesCredz
{
    public long id { get; set; }
    public long acordo_id { get; set; }
    public string nosso_numero { get; set; }
    public DateTime vencimento { get; set; }
    public decimal valor { get; set; }
    public string codigo_barra { get; set; }
    public string linha_digitavel { get; set; }
    public string url { get; set; }
}

public class ParcelasNovaCredz
{
    public long id { get; set; }
    public string parcela { get; set; }
    public DateTime vencimento { get; set; }
    public decimal valor { get; set; }
}

public class ParcelasOriginalCredz
{
    public long id { get; set; }
    public int atraso { get; set; }
    public decimal principal { get; set; }
    public decimal juros { get; set; }
    public decimal multa { get; set; }
    public decimal honorarios { get; set; }
    public decimal desconto_principal { get; set; }
    public decimal desconto_multa { get; set; }
    public decimal desconto_juros { get; set; }
    public decimal desconto_honorarios { get; set; }
    public decimal despesas { get; set; }
    public decimal total { get; set; }
    public decimal repasse_principal { get; set; }
    public decimal repasse_multa { get; set; }
    public decimal repasse_juros { get; set; }
    public decimal repasse_despesa { get; set; }
    public decimal total_repasse { get; set; }
}

public class BoletoCompletoCredz
{
    public long id { get; set; }
    public long acordo_id { get; set; }
    public bool status { get; set; }
    public DateTime data_documento { get; set; }
    public string numero_documento { get; set; }
    public string numero_parcela { get; set; }
    public DateTime vencimento { get; set; }
    public double valor { get; set; }
    public string nosso_numero { get; set; }
    public string codigo_barra { get; set; }
    public string linha_digitavel { get; set; }
    public string instrucoes { get; set; }
    public string mensagem { get; set; }
    public string codigo_banco { get; set; }
    public string carteira { get; set; }
    public string cedente { get; set; }
    public string digito_cedente { get; set; }
    public string agencia { get; set; }
    public string digito_agencia { get; set; }
    public string conta_corrente { get; set; }
    public string digito_conta_corrente { get; set; }
    public string especie { get; set; }
    public string moeda { get; set; }
    public string aceite { get; set; }
    public string url { get; set; }
}
