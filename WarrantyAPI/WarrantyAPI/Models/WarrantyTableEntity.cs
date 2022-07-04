using Microsoft.Azure.Cosmos.Table;

namespace WarrantyAPI.Models
{
    public class WarrantyTableEntity : TableEntity
    {

        public WarrantyTableEntity()
        {

        }
        public WarrantyTableEntity(string PartKey, string RowKey)
        {
            this.RowKey = RowKey.ToString();
            this.PartitionKey = PartKey;
        }
        

        public string WarrantyID { get; set; }    
        public string AssetID { get; set; }
        public double PriceOfWarranty { get; set; }
        public DateTime Duration { get; set; }
        public string ContactMail { get; set; }
        public bool PossibilityToExtend { get; set; }
    }
}