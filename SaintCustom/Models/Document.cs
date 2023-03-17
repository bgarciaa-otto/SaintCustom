using System;
using System.Collections.Generic;

namespace SaintCustom.Models
{
    internal class Document
    {
        public string DocumentNumber { get; set; }
        public string ProviderCode { get; set; }
        public string WarehouseCode { get; set; }
        public DateTime? DocumentDate { get; set; }
        public int? DueDay { get; set; }
        public List<Product> Products { get; set; }
    }
}
