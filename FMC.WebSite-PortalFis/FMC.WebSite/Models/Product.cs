using FMC.WebSite.FIS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

public class Product
{
    public long IdProduct { get; set; }
    public long IdNavigation { get; set; }
    public string Account { get; set; }

    public int Age { get; set; }

    public decimal VlFull { get; set; }

    public decimal VlMinimum { get; set; }

    public DateTime DtInsert { get; set; }

    public string ProductType { get; set; }

    public virtual Navigation Navigation { get; set; }

}