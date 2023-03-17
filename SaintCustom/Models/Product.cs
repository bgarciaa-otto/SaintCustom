namespace SaintCustom.Models
{
    internal class Product
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Stock { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public string ClassificationCode { get; set; }
        public bool PrintZPL { get; set; }
        public int LineNumber { get; set; }
    }
}
