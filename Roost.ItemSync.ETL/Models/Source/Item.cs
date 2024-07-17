namespace Roost.ItemSync.ETL.Models.Source
{
    public class Item
    {
        public int ItemID { get; set; }

        public string Upc { set; get; }

        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        public double UnitQuantity { set; get; }

        public List<ItemAttribute> Attributes { get; set; }

        public Category Category { get; set; }

        public UnitOfMeasure UnitOfMeasure { get; set; }

        public List<ItemImage> Images { get; set; }
    }
}
