using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Billet
{
    public long IdBillet { get; set; }
    public string CPF { get; set; }
    public decimal VlBillet { get; set; }
    public string Account { get; set; }
    public string CodeBar { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public bool FlPrint { get; set; }
    public DateTime DtInsert { get; set; }
}