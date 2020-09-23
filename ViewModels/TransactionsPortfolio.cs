namespace StockMonitor_2.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class TransactionsPortfolio
    {

        public int ID { get; set; }
        public string Kayttaja { get; set; }
        public string OstoMyynti { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d.M.yyyy}")]
        public Nullable<System.DateTime> Pvm { get; set; }
        public string Yritys { get; set; }
        public int Maara { get; set; }
        public int MaaraForPortfolio { get; set; }
        public decimal aHinta { get; set; }
        public decimal Total { get; set; }
        public decimal TotalForPortfolio { get; set; }
        public string Valuutta { get; set; }
        public decimal? Kurssi { get; set; }
        public decimal TotalEuros { get; set; }
        public decimal Kulut { get; set; }
        public decimal Grandtotal { get; set; }
        public int? MaaraYht { get; set; }
        public decimal HankintaArvo { get; set; }
        public decimal aHintaNyt { get; set; }
        public decimal ArvoNytAll { get; set; }
        public decimal VoittoTappioE { get; set; }
        public decimal VoittoTappio_	{ get; set; }
    }
}