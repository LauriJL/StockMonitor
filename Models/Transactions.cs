//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockMonitor_2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    public partial class Transactions
    {
        public int ID { get; set; }
        public string Kayttaja { get; set; }
        public string OstoMyynti { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d.M.yyyy}")]
        public System.DateTime Pvm { get; set; }
        public string Yritys { get; set; }
        public int Maara { get; set; }
        public Nullable<int> MaaraForPortfolio { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public decimal aHinta { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> TotalForPortfolio { get; set; }
        public string Valuutta { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public Nullable<decimal> Kurssi { get; set; }
        public Nullable<decimal> TotalEuros { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public decimal Kulut { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public Nullable<decimal> Grandtotal { get; set; }
    
        public virtual Currency Currency { get; set; }
        public virtual Users Users { get; set; }
        public virtual TransactionTypes TransactionTypes { get; set; }
    }
}
