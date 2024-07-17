namespace Roost.ItemSync.ETL.Models.Source
{
    public class ItemAttribute
    {
        public string AttributeName { get; set; }

        public string AttributeDescription { set; get; }

        public ItemAttributeValue AttributeValue { get; set; }

    }
}
