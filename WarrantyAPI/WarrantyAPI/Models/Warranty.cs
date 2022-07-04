namespace WarrantyAPI.Models
{
    public class Warranty
    {
        public string WarrantyID { get; set; }
        public string AssetID { get; set; }
        public double PriceOfWarranty { get; set; }
        public DateTime Duration { get; set; }
        public string ContactMail { get; set; }
        public bool PossibilityToExtend { get; set; }
    }
}
