using Newtonsoft.Json;
using System.Text.Json;

namespace Roost.ItemSync.ETL.Models.Target
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public string PartitionKey {  get; set; }

        public string Upc { set; get; }

        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        public double UnitQuantity { set; get; }

        public ItemAttributes ItemAttributes { get; set; }

        public string Category { get; set; }

        public string CategoryDescription { get; set; }

        public string ParentCategory { get; set; }

        public string ParentCategoryDescription { get; set; }

        public UnitOfMeasure UnitOfMeasure { get; set; }

        public string ImageUrl { get; set; }
    }
}
