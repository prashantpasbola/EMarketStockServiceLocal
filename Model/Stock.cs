using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stockservice.Model
{
    public class Stock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StockId { get; set; }
        [Required]
        public double? StockPrice { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

        public string CompanyCode { get; set; }
    }

    public class StockDetails
    {
        public List<Stock> Stocks { get; set; }
        public double? Min { get; set; }
        public double? Max { get; set; }
        public double? Avg { get; set; }
    }
    public class CompanyCodeModel
    {
        public string CompanyCode { get; set; }
    }
}
